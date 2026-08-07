using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    /// <summary>
    /// Reglas de negocio y validaciones para la gestión de ventas, incluyendo la creación
    /// transaccional de una venta junto con sus detalles y la actualización del stock de
    /// los productos involucrados. Trabaja con <see cref="VentaDTO"/> y <see cref="DetalleVentaDTO"/>,
    /// delegando el acceso a datos en los repositorios obtenidos mediante <see cref="RepositorioFactory"/>.
    /// </summary>
    public class VentaNegocio
    {
        private readonly IVentaRepositorio ventaRepositorio;
        private readonly IDetalleVentaRepositorio detalleVentaRepositorio;
        private readonly ClienteNegocio clienteNegocio = new ClienteNegocio();
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        /// <summary>
        /// Crea una nueva instancia de <see cref="VentaNegocio"/> y obtiene los repositorios
        /// de ventas y detalles de venta a través de la fábrica de repositorios.
        /// </summary>
        public VentaNegocio()
        {
            ventaRepositorio = RepositorioFactory.CrearVentaRepositorio();
            detalleVentaRepositorio = RepositorioFactory.CrearDetalleVentaRepositorio();
        }

        /// <summary>
        /// Obtiene la lista completa de ventas registradas, con sus detalles y datos de cliente
        /// resueltos.
        /// </summary>
        /// <returns>Lista de ventas en formato <see cref="VentaDTO"/>.</returns>
        public List<VentaDTO> ObtenerVentas()
        {
            List<Venta> ventas = ventaRepositorio.ObtenerVentas();
            return ventas.Select(MapearVentaADto).ToList();
        }

        /// <summary>
        /// Busca una venta por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la venta.</param>
        /// <returns>El <see cref="VentaDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        public VentaDTO? ObtenerVentaPorId(int id)
        {
            Venta? venta = ventaRepositorio.ObtenerVentaPorId(id);
            return venta == null ? null : MapearVentaADto(venta);
        }

        /// <summary>
        /// Función de mapeo: convierte una entidad <see cref="Venta"/> en su <see cref="VentaDTO"/>
        /// correspondiente, resolviendo el cliente y los detalles (incluyendo el nombre de cada
        /// producto) asociados a la venta.
        /// </summary>
        /// <param name="venta">Entidad de venta proveniente del repositorio.</param>
        /// <returns>El <see cref="VentaDTO"/> equivalente, con cliente y detalles completos.</returns>
        /// <exception cref="Exception">Se lanza si el cliente o algún producto referenciado no existe.</exception>
        private VentaDTO MapearVentaADto(Venta venta)
        {
            var cliente = clienteNegocio.ObtenerClientePorId(venta.ClienteId);
            if (cliente == null)
                throw new Exception($"Cliente no encontrado (Id={venta.ClienteId}).");

            List<DetalleVenta> detallesEntidad = detalleVentaRepositorio.ObtenerDetallesPorVenta(venta.Id);
            List<DetalleVentaDTO> detalles = new List<DetalleVentaDTO>();

            foreach (var detalle in detallesEntidad)
            {
                var producto = productoNegocio.ObtenerProductoPorId(detalle.ProductoId);
                if (producto == null)
                    throw new Exception($"Producto no encontrado (Id={detalle.ProductoId}).");

                detalles.Add(new DetalleVentaDTO
                {
                    Id = detalle.Id,
                    VentaId = detalle.VentaId,
                    ProductoId = detalle.ProductoId,
                    ProductoNombre = producto.Nombre,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = detalle.Subtotal
                });
            }

            return new VentaDTO
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                ClienteId = venta.ClienteId,
                ClienteNombre = cliente.Nombre,
                Total = venta.Total,
                Detalles = detalles
            };
        }

        /// <summary>
        /// Crea una nueva venta junto con todos sus detalles dentro de una única transacción MySQL.
        /// <para>
        /// Validaciones que realiza: que la venta y la lista de detalles no sean nulas y que existan
        /// detalles; que el cliente y cada producto tengan un identificador válido; y que cada
        /// cantidad sea mayor a cero. Si un producto aparece varias veces, acumula sus cantidades.
        /// Ya dentro de la transacción bloquea cada producto, comprueba el stock más reciente,
        /// recalcula precios, subtotales y total, y solo entonces guarda la venta.
        /// </para>
        /// <para>
        /// Garantías de la transacción: la validación y bloqueo del stock, la inserción de la venta,
        /// la inserción de cada uno de sus detalles y el descuento del inventario se ejecutan sobre la misma
        /// <see cref="MySqlConnection"/> y <see cref="MySqlTransaction"/>. Si cualquier paso falla
        /// (la venta, un detalle o una actualización de stock), se hace <c>Rollback</c> de toda la
        /// transacción y se relanza la excepción, de modo que nunca queda una venta guardada sin
        /// sus detalles, ni un detalle guardado sin el descuento correspondiente de stock. Solo se
        /// hace <c>Commit</c> si absolutamente todo el proceso se completó con éxito.
        /// </para>
        /// </summary>
        /// <param name="venta">Datos generales de la venta (cliente, fecha, total a calcular).</param>
        /// <param name="detalles">Lista de detalles (líneas de producto) que componen la venta.</param>
        /// <returns><c>true</c> si la venta y todos sus detalles se guardaron correctamente.</returns>
        /// <exception cref="ArgumentNullException">Se lanza si <paramref name="venta"/> es <c>null</c>.</exception>
        /// <exception cref="Exception">
        /// Se lanza cuando alguna validación de negocio falla o cuando ocurre un error al guardar
        /// la venta, un detalle o al actualizar el stock (en cuyo caso se revierte la transacción).
        /// </exception>
        public bool CrearVenta(VentaDTO venta, List<DetalleVentaDTO> detalles)
        {
            if (venta == null)
            {
                throw new ArgumentNullException(nameof(venta));
            }

            if (detalles == null || detalles.Count == 0)
            {
                throw new Exception("La venta debe contener al menos un producto.");
            }

            if (venta.ClienteId <= 0)
            {
                throw new Exception("El cliente es requerido.");
            }

            // Las validaciones básicas se hacen antes de abrir la transacción.
            foreach (DetalleVentaDTO detalle in detalles)
            {
                if (detalle.ProductoId <= 0)
                {
                    throw new Exception("Existe un producto inválido en la venta.");
                }

                if (detalle.Cantidad <= 0)
                {
                    throw new Exception("La cantidad debe ser mayor que cero.");
                }
            }

            // Si el mismo producto fue agregado varias veces, se convierte en una sola línea.
            // Esto hace que la cantidad total se compare contra el stock disponible.
            List<DetalleVentaDTO> detallesAgrupados = detalles
                .GroupBy(d => d.ProductoId)
                .Select(grupo => new DetalleVentaDTO
                {
                    ProductoId = grupo.Key,
                    Cantidad = grupo.Sum(d => d.Cantidad)
                })
                .OrderBy(d => d.ProductoId)
                .ToList();

            if (venta.Fecha == DateTime.MinValue)
            {
                venta.Fecha = DateTime.Now;
            }

            ConexionDB conexionDB = new ConexionDB();

            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        decimal totalVenta = 0;
                        Dictionary<int, ProductoDTO> productosBloqueados =
                            new Dictionary<int, ProductoDTO>();

                        // Todas las lecturas de stock se hacen dentro de esta misma transacción.
                        // FOR UPDATE mantiene cada producto bloqueado hasta Commit o Rollback.
                        foreach (DetalleVentaDTO detalle in detallesAgrupados)
                        {
                            ProductoDTO? producto = productoNegocio.ObtenerProductoPorId(
                                detalle.ProductoId,
                                conexion,
                                transaccion);

                            if (producto == null)
                            {
                                throw new Exception(
                                    $"No se encontró el producto con Id={detalle.ProductoId}.");
                            }

                            if (producto.Stock < detalle.Cantidad)
                            {
                                throw new Exception(
                                    $"Stock insuficiente para el producto {producto.Nombre}. " +
                                    $"Disponible: {producto.Stock}.");
                            }

                            detalle.ProductoNombre = producto.Nombre;
                            detalle.PrecioUnitario = producto.Precio;
                            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                            totalVenta += detalle.Subtotal;
                            productosBloqueados.Add(producto.Id, producto);
                        }

                        venta.Total = totalVenta;
                        venta.Detalles = detallesAgrupados;

                        Venta ventaEntidad = new Venta
                        {
                            ClienteId = venta.ClienteId,
                            Fecha = venta.Fecha,
                            Total = venta.Total
                        };

                        int ventaId =
                            ventaRepositorio.InsertarVenta(
                                ventaEntidad,
                                conexion,
                                transaccion);

                        if (ventaId <= 0)
                        {
                            throw new Exception("No fue posible guardar la venta.");
                        }

                        venta.Id = ventaId;

                        foreach (DetalleVentaDTO detalle in detallesAgrupados)
                        {
                            detalle.VentaId = ventaId;

                            DetalleVenta detalleEntidad = new DetalleVenta
                            {
                                VentaId = ventaId,
                                ProductoId = detalle.ProductoId,
                                Cantidad = detalle.Cantidad,
                                PrecioUnitario = detalle.PrecioUnitario,
                                Subtotal = detalle.Subtotal
                            };

                            bool detalleGuardado =
                                detalleVentaRepositorio.InsertarDetalleVenta(
                                    detalleEntidad,
                                    conexion,
                                    transaccion);

                            if (!detalleGuardado)
                            {
                                throw new Exception(
                                    $"No fue posible guardar el detalle del producto " +
                                    $"Id={detalle.ProductoId}.");
                            }

                            ProductoDTO producto = productosBloqueados[detalle.ProductoId];

                            int nuevoStock =
                                producto.Stock - detalle.Cantidad;

                            bool stockActualizado =
                                productoNegocio.ActualizarStock(
                                    detalle.ProductoId,
                                    nuevoStock,
                                    conexion,
                                    transaccion);

                            if (!stockActualizado)
                            {
                                throw new Exception(
                                    $"No fue posible actualizar el stock del producto " +
                                    $"{producto.Nombre}.");
                            }
                        }

                        transaccion.Commit();

                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Valida y actualiza los datos generales de una venta y recalcula los subtotales
        /// y el total a partir de sus detalles. No modifica el stock de los productos.
        /// </summary>
        /// <param name="venta">Datos actualizados de la venta, incluyendo sus detalles.</param>
        /// <returns><c>true</c> si la venta fue actualizada correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando el cliente indicado no es válido.</exception>
        public bool EditarVenta(VentaDTO venta)
        {
            if (venta.ClienteId <= 0)
                throw new Exception("El cliente es requerido.");

            decimal total = 0;
            foreach (var detalle in venta.Detalles)
            {
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                total += detalle.Subtotal;
            }

            venta.Total = total;

            Venta ventaEntidad = new Venta
            {
                Id = venta.Id,
                ClienteId = venta.ClienteId,
                Fecha = venta.Fecha,
                Total = venta.Total
            };

            return ventaRepositorio.EditarVenta(ventaEntidad);
        }

        /// <summary>
        /// Elimina una venta por su identificador, eliminando primero todos sus detalles asociados.
        /// </summary>
        /// <param name="id">Identificador de la venta a eliminar.</param>
        /// <returns><c>true</c> si la venta fue eliminada correctamente.</returns>
        public bool EliminarVenta(int id)
        {
            detalleVentaRepositorio.EliminarDetallesPorVenta(id);
            return ventaRepositorio.EliminarVenta(id);
        }
    }
}