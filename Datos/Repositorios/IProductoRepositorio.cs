using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Abstracción sobre el acceso a datos de Producto, usada por la capa de
    /// Negocio para no depender directamente del DAO concreto (<see cref="SistemaVentas.Datos.DAO.ProductoDAO"/>).
    /// </summary>
    public interface IProductoRepositorio
    {
        /// <summary>
        /// Obtiene todos los productos registrados.
        /// </summary>
        /// <returns>Lista de todos los productos encontrados (puede estar vacía).</returns>
        List<Producto> ObtenerProductos();

        /// <summary>
        /// Busca un producto por su identificador.
        /// </summary>
        /// <param name="id">Id del producto a buscar.</param>
        /// <returns>El producto encontrado, o <c>null</c> si no existe.</returns>
        Producto? ObtenerProductoPorId(int id);

        /// <summary>
        /// Busca y bloquea un producto dentro de una transacción existente para que su stock
        /// no pueda modificarse simultáneamente desde otra venta.
        /// </summary>
        /// <param name="id">Id del producto a buscar.</param>
        /// <param name="conexion">Conexión abierta de la transacción actual.</param>
        /// <param name="transaccion">Transacción MySQL actual.</param>
        /// <returns>El producto bloqueado, o <c>null</c> si no existe.</returns>
        Producto? ObtenerProductoPorId(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion);

        /// <summary>
        /// Inserta un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool InsertarProducto(Producto producto);

        /// <summary>
        /// Actualiza los datos de un producto existente.
        /// </summary>
        /// <param name="producto">Producto con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EditarProducto(Producto producto);

        /// <summary>
        /// Elimina un producto por su Id.
        /// </summary>
        /// <param name="id">Id del producto a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EliminarProducto(int id);


        bool TieneVentas(int id);

        /// <summary>
        /// Actualiza el stock de un producto.
        /// </summary>
        /// <param name="id">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool ActualizarStock(int id, int nuevoStock);

        /// <summary>
        /// Variante transaccional de <see cref="ActualizarStock(int, int)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="productoId">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion);

        /// <summary>
        /// Verifica si ya existe un producto con ese código.
        /// </summary>
        /// <param name="codigo">Código a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese código.</returns>
        bool ExisteCodigo(string codigo, int? excludeId = null);

        /// <summary>
        /// Verifica si ya existe un producto con ese nombre.
        /// </summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese nombre.</returns>
        bool ExisteNombre(string nombre, int? excludeId = null);
    }
}