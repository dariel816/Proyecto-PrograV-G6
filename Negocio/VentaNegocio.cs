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
    public class VentaNegocio
    {
        private readonly IVentaRepositorio ventaRepositorio;
        private readonly IDetalleVentaRepositorio detalleVentaRepositorio;
        private readonly ClienteNegocio clienteNegocio = new ClienteNegocio();
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        public VentaNegocio()
        {
            ventaRepositorio = RepositorioFactory.CrearVentaRepositorio();
            detalleVentaRepositorio = RepositorioFactory.CrearDetalleVentaRepositorio();
        }

        public List<VentaDTO> ObtenerVentas()
        {
            List<Venta> ventas = ventaRepositorio.ObtenerVentas();
            return ventas.Select(MapearVentaADto).ToList();
        }

        public VentaDTO? ObtenerVentaPorId(int id)
        {
            Venta? venta = ventaRepositorio.ObtenerVentaPorId(id);
            return venta == null ? null : MapearVentaADto(venta);
        }

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

            decimal totalVenta = 0;

            // Validar productos, cantidades, precios y stock
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

                ProductoDTO? producto =
                    productoNegocio.ObtenerProductoPorId(detalle.ProductoId);

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

                if (venta.Fecha == DateTime.MinValue)
                {
                    venta.Fecha = DateTime.Now;
                }

                detalle.ProductoNombre = producto.Nombre;
                detalle.PrecioUnitario = producto.Precio;
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

                totalVenta += detalle.Subtotal;
            }

            venta.Total = totalVenta;

            Venta ventaEntidad = new Venta
            {
                ClienteId = venta.ClienteId,
                Fecha = venta.Fecha,
                Total = venta.Total
            };

            ConexionDB conexionDB = new ConexionDB();

            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
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

                        foreach (DetalleVentaDTO detalle in detalles)
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

                            ProductoDTO? producto =
                                productoNegocio.ObtenerProductoPorId(
                                    detalle.ProductoId);

                            if (producto == null)
                            {
                                throw new Exception(
                                    $"No se encontró el producto con " +
                                    $"Id={detalle.ProductoId}.");
                            }

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

        public bool EliminarVenta(int id)
        {
            detalleVentaRepositorio.EliminarDetallesPorVenta(id);
            return ventaRepositorio.EliminarVenta(id);
        }
    }
}
