using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class ProductoNegocio
    {
        ProductoDAO productoDAO = new ProductoDAO();

        public List<Producto> ObtenerProductos()
        {
            return productoDAO.ObtenerProductos();
        }

        public Producto ObtenerProductoPorId(int id)
        {
            return productoDAO.ObtenerProductoPorId(id);
        }

        public bool InsertarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            return productoDAO.InsertarProducto(producto);
        }

        public bool EditarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            return productoDAO.EditarProducto(producto);
        }

        public bool EliminarProducto(int id)
        {
            return productoDAO.EliminarProducto(id);
        }
    }
}
