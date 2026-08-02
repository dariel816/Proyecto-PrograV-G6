using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Implementación de <see cref="IProductoRepositorio"/> que delega en <see cref="ProductoDAO"/>.
    /// </summary>
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly ProductoDAO productoDAO = new ProductoDAO();

        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de todos los productos encontrados (puede estar vacía).</returns>
        public List<Producto> ObtenerProductos() => productoDAO.ObtenerProductos();

        /// <summary>
        /// Busca un producto por su identificador.
        /// </summary>
        /// <param name="id">Id del producto a buscar.</param>
        /// <returns>El producto encontrado, o <c>null</c> si no existe.</returns>
        public Producto? ObtenerProductoPorId(int id) => productoDAO.ObtenerProductoPorId(id);

        /// <summary>
        /// Inserta un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool InsertarProducto(Producto producto) => productoDAO.InsertarProducto(producto);

        /// <summary>
        /// Actualiza los datos de un producto existente.
        /// </summary>
        /// <param name="producto">Producto con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EditarProducto(Producto producto) => productoDAO.EditarProducto(producto);

        /// <summary>
        /// Elimina un producto por su Id.
        /// </summary>
        /// <param name="id">Id del producto a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EliminarProducto(int id) => productoDAO.EliminarProducto(id);

        /// <summary>
        /// Actualiza el stock de un producto.
        /// </summary>
        /// <param name="id">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool ActualizarStock(int id, int nuevoStock) => productoDAO.ActualizarStock(id, nuevoStock);

        /// <summary>
        /// Variante transaccional de <see cref="ActualizarStock(int, int)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="productoId">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion)
            => productoDAO.ActualizarStock(productoId, nuevoStock, conexion, transaccion);

        /// <summary>
        /// Verifica si ya existe un producto con ese código.
        /// </summary>
        /// <param name="codigo">Código a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese código.</returns>
        public bool ExisteCodigo(string codigo, int? excludeId = null) => productoDAO.ExisteCodigo(codigo, excludeId);

        /// <summary>
        /// Verifica si ya existe un producto con ese nombre.
        /// </summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese nombre.</returns>
        public bool ExisteNombre(string nombre, int? excludeId = null) => productoDAO.ExisteNombre(nombre, excludeId);
    }
}
