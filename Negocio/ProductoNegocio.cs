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

        public Producto? ObtenerProductoPorId(int id)
        {
            return productoDAO.ObtenerProductoPorId(id);
        }

        public bool InsertarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            // Validaciones de unicidad
            if (productoDAO.ExisteCodigo(producto.Codigo, null))
                throw new Exception("El código ya está registrado.");

            if (productoDAO.ExisteNombre(producto.Nombre, null))
                throw new Exception("El nombre del producto ya está registrado.");

            return productoDAO.InsertarProducto(producto);
        }

        public bool EditarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            // Validaciones de unicidad (excluir el propio registro)
            if (productoDAO.ExisteCodigo(producto.Codigo, producto.Id))
                throw new Exception("El código ya está registrado por otro producto.");

            if (productoDAO.ExisteNombre(producto.Nombre, producto.Id))
                throw new Exception("El nombre del producto ya está registrado por otro producto.");

            return productoDAO.EditarProducto(producto);
        }

        public bool EliminarProducto(int id)
        {
            return productoDAO.EliminarProducto(id);
        }
    }
}
