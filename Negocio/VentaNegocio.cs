using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;

namespace SistemaVentas.Negocio
{
    public class VentaNegocio
    {
        VentaDAO ventaDAO = new VentaDAO();
        DetalleVentaDAO detalleVentaDAO = new DetalleVentaDAO();
        ClienteNegocio clienteNegocio = new ClienteNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();
        ProductoDAO productoDAO = new ProductoDAO();

        public List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = ventaDAO.ObtenerVentas();

            foreach (var venta in ventas)
            {
                var cliente = clienteNegocio.ObtenerClientePorId(venta.ClienteId);
                if (cliente == null)
                    throw new Exception($"Cliente no encontrado (Id={venta.ClienteId}).");
                venta.Cliente = cliente;

                venta.Detalles = detalleVentaDAO.ObtenerDetallesPorVenta(venta.Id);

                foreach (var detalle in venta.Detalles)
                {
                    var producto = productoNegocio.ObtenerProductoPorId(detalle.ProductoId);
                    if (producto == null)
                        throw new Exception($"Producto no encontrado (Id={detalle.ProductoId}).");

                    detalle.Producto = producto;
                }
            }

            return ventas;
        }

        public Venta? ObtenerVentaPorId(int id)
        {
            Venta? venta = ventaDAO.ObtenerVentaPorId(id);

            if (venta != null)
            {
                var cliente = clienteNegocio.ObtenerClientePorId(venta.ClienteId);
                if (cliente == null)
                    throw new Exception($"Cliente no encontrado (Id={venta.ClienteId}).");
                venta.Cliente = cliente;

                venta.Detalles = detalleVentaDAO.ObtenerDetallesPorVenta(venta.Id);

                foreach (var detalle in venta.Detalles)
                {
                    var producto = productoNegocio.ObtenerProductoPorId(detalle.ProductoId);
                    if (producto == null)
                        throw new Exception($"Producto no encontrado (Id={detalle.ProductoId}).");

                    detalle.Producto = producto;
                }
            }

            return venta;
        }

        public bool CrearVenta(Venta venta, List<DetalleVenta> detalles)
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
            foreach (DetalleVenta detalle in detalles)
            {
                if (detalle.ProductoId <= 0)
                {
                    throw new Exception("Existe un producto inválido en la venta.");
                }

                if (detalle.Cantidad <= 0)
                {
                    throw new Exception("La cantidad debe ser mayor que cero.");
                }

                Producto producto =
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

                detalle.PrecioUnitario = producto.Precio;
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

                totalVenta += detalle.Subtotal;
            }

            venta.Total = totalVenta;

            ConexionDB conexionDB = new ConexionDB();

            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        int ventaId =
                            ventaDAO.InsertarVenta(
                                venta,
                                conexion,
                                transaccion);

                        if (ventaId <= 0)
                        {
                            throw new Exception("No fue posible guardar la venta.");
                        }

                        foreach (DetalleVenta detalle in detalles)
                        {
                            detalle.VentaId = ventaId;

                            bool detalleGuardado =
                                detalleVentaDAO.InsertarDetalleVenta(
                                    detalle,
                                    conexion,
                                    transaccion);

                            if (!detalleGuardado)
                            {
                                throw new Exception(
                                    $"No fue posible guardar el detalle del producto " +
                                    $"Id={detalle.ProductoId}.");
                            }

                            Producto producto =
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
                                productoDAO.ActualizarStock(
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

        public bool EditarVenta(Venta venta)
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

            return ventaDAO.EditarVenta(venta);
        }

        public bool EliminarVenta(int id)
        {
            detalleVentaDAO.EliminarDetallesPorVenta(id);
            return ventaDAO.EliminarVenta(id);
        }
    }
}
