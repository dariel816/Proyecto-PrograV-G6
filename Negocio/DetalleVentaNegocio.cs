using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class DetalleVentaNegocio
    {
        DetalleVentaDAO detalleVentaDAO = new DetalleVentaDAO();
        ProductoDAO productoDAO = new ProductoDAO();

        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId)
        {
            List<DetalleVenta> detalles = detalleVentaDAO.ObtenerDetallesPorVenta(ventaId);

            foreach (var detalle in detalles)
            {
                var producto = productoDAO.ObtenerProductoPorId(detalle.ProductoId);
                if (producto == null)
                    throw new Exception($"Producto no encontrado (Id={detalle.ProductoId}).");

                detalle.Producto = producto;
            }

            return detalles;
        }

        public DetalleVenta? ObtenerDetallePorId(int id)
        {
            DetalleVenta? detalle = detalleVentaDAO.ObtenerDetallePorId(id);

            if (detalle != null)
            {
                var producto = productoDAO.ObtenerProductoPorId(detalle.ProductoId);
                if (producto == null)
                    throw new Exception($"Producto no encontrado (Id={detalle.ProductoId}).");

                detalle.Producto = producto;
            }

            return detalle;
        }

        public bool AgregarDetalle(DetalleVenta detalle)
        {
            if (detalle.ProductoId <= 0)
                throw new Exception("El producto es requerido.");

            if (detalle.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            if (detalle.PrecioUnitario <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            return detalleVentaDAO.InsertarDetalleVenta(detalle);
        }

        public bool EditarDetalle(DetalleVenta detalle)
        {
            if (detalle.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            if (detalle.PrecioUnitario <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            return detalleVentaDAO.EditarDetalleVenta(detalle);
        }

        public bool EliminarDetalle(int id)
        {
            return detalleVentaDAO.EliminarDetalleVenta(id);
        }
    }
}
