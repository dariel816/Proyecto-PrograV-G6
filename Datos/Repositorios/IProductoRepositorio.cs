using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public interface IProductoRepositorio
    {
        List<Producto> ObtenerProductos();

        Producto? ObtenerProductoPorId(int id);

        bool InsertarProducto(Producto producto);

        bool EditarProducto(Producto producto);

        bool EliminarProducto(int id);

        bool ActualizarStock(int id, int nuevoStock);

        bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion);

        bool ExisteCodigo(string codigo, int? excludeId = null);

        bool ExisteNombre(string nombre, int? excludeId = null);
    }
}
