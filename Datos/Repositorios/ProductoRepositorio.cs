using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly ProductoDAO productoDAO = new ProductoDAO();

        public List<Producto> ObtenerProductos() => productoDAO.ObtenerProductos();

        public Producto? ObtenerProductoPorId(int id) => productoDAO.ObtenerProductoPorId(id);

        public bool InsertarProducto(Producto producto) => productoDAO.InsertarProducto(producto);

        public bool EditarProducto(Producto producto) => productoDAO.EditarProducto(producto);

        public bool EliminarProducto(int id) => productoDAO.EliminarProducto(id);

        public bool ActualizarStock(int id, int nuevoStock) => productoDAO.ActualizarStock(id, nuevoStock);

        public bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion)
            => productoDAO.ActualizarStock(productoId, nuevoStock, conexion, transaccion);

        public bool ExisteCodigo(string codigo, int? excludeId = null) => productoDAO.ExisteCodigo(codigo, excludeId);

        public bool ExisteNombre(string nombre, int? excludeId = null) => productoDAO.ExisteNombre(nombre, excludeId);
    }
}
