using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class VentaNegocio
    {
        VentaDAO ventaDAO = new VentaDAO();
        DetalleVentaDAO detalleVentaDAO = new DetalleVentaDAO();
        ClienteNegocio clienteNegocio = new ClienteNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();

        public List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = ventaDAO.ObtenerVentas();

            foreach (var venta in ventas)
            {
                venta.Cliente = clienteNegocio.ObtenerClientePorId(venta.ClienteId);
                venta.Detalles = detalleVentaDAO.ObtenerDetallesPorVenta(venta.Id);

                foreach (var detalle in venta.Detalles)
                {
                    detalle.Producto = productoNegocio.ObtenerProductoPorId(detalle.ProductoId);
                }
            }

            return ventas;
        }

        public Venta ObtenerVentaPorId(int id)
        {
            Venta venta = ventaDAO.ObtenerVentaPorId(id);

            if (venta != null)
            {
                venta.Cliente = clienteNegocio.ObtenerClientePorId(venta.ClienteId);
                venta.Detalles = detalleVentaDAO.ObtenerDetallesPorVenta(venta.Id);

                foreach (var detalle in venta.Detalles)
                {
                    detalle.Producto = productoNegocio.ObtenerProductoPorId(detalle.ProductoId);
                }
            }

            return venta;
        }

        public int CrearVenta(Venta venta)
        {
            if (venta.ClienteId <= 0)
                throw new Exception("El cliente es requerido.");

            if (venta.Detalles == null || venta.Detalles.Count == 0)
                throw new Exception("La venta debe contener al menos un detalle.");

            decimal total = 0;
            foreach (var detalle in venta.Detalles)
            {
                if (detalle.Cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a 0.");

                if (detalle.PrecioUnitario <= 0)
                    throw new Exception("El precio debe ser mayor a 0.");

                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                total += detalle.Subtotal;
            }

            venta.Total = total;
            venta.Fecha = DateTime.Now;

            int ventaId = ventaDAO.InsertarVenta(venta);

            if (ventaId > 0)
            {
                foreach (var detalle in venta.Detalles)
                {
                    detalle.VentaId = ventaId;
                    detalleVentaDAO.InsertarDetalleVenta(detalle);
                }
            }

            return ventaId;
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
