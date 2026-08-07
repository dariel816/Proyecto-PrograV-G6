using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    /// <summary>
    /// Acceso a datos de la tabla <c>productos</c> en MySQL mediante ADO.NET
    /// (MySql.Data.MySqlClient).
    /// </summary>
    public class ProductoDAO
    {
        ConexionDB conexionDB = new ConexionDB();

        /// <summary>
        /// Obtiene todos los productos registrados en la tabla <c>productos</c>.
        /// </summary>
        /// <returns>Lista de todos los productos encontrados (puede estar vacía).</returns>
        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM productos";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto
                        {
                            Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : Convert.ToInt32(reader["id"]),
                            Codigo = reader.IsDBNull(reader.GetOrdinal("codigo")) ? string.Empty : reader.GetString(reader.GetOrdinal("codigo")),
                            Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("nombre")),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString(reader.GetOrdinal("descripcion")),
                            Precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0m : Convert.ToDecimal(reader["precio"]),
                            Stock = reader.IsDBNull(reader.GetOrdinal("stock")) ? 0 : Convert.ToInt32(reader["stock"])
                        };

                        lista.Add(producto);
                    }
                }
            }

            return lista;
        }

        /// <summary>
        /// Busca un producto por su identificador.
        /// </summary>
        /// <param name="id">Id del producto a buscar.</param>
        /// <returns>El producto encontrado, o <c>null</c> si no existe.</returns>
        public Producto? ObtenerProductoPorId(int id)
        {
            Producto? producto = null;

            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();
                string query = "SELECT * FROM productos WHERE id = @id";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : Convert.ToInt32(reader["id"]),
                                Codigo = reader.IsDBNull(reader.GetOrdinal("codigo")) ? string.Empty : reader.GetString(reader.GetOrdinal("codigo")),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? string.Empty : reader.GetString(reader.GetOrdinal("nombre")),
                                Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? string.Empty : reader.GetString(reader.GetOrdinal("descripcion")),
                                Precio = reader.IsDBNull(reader.GetOrdinal("precio")) ? 0m : Convert.ToDecimal(reader["precio"]),
                                Stock = reader.IsDBNull(reader.GetOrdinal("stock")) ? 0 : Convert.ToInt32(reader["stock"])
                            };
                        }
                    }
                }
            }

            return producto;
        }

        /// <summary>
        /// Busca un producto dentro de una transacción existente y bloquea su fila hasta que
        /// la transacción termine. Esto evita que dos ventas descuenten simultáneamente el mismo
        /// stock usando un valor desactualizado.
        /// </summary>
        /// <param name="id">Id del producto a consultar.</param>
        /// <param name="conexion">Conexión abierta de la transacción actual.</param>
        /// <param name="transaccion">Transacción MySQL actual.</param>
        /// <returns>El producto bloqueado, o <c>null</c> si no existe.</returns>
        public Producto? ObtenerProductoPorId(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            const string query = @"SELECT id, codigo, nombre, descripcion, precio, stock
                                   FROM productos
                                   WHERE id = @id
                                   FOR UPDATE";

            using (MySqlCommand comando = new MySqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new Producto
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Codigo = reader.IsDBNull(reader.GetOrdinal("codigo"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("codigo")),
                        Nombre = reader.IsDBNull(reader.GetOrdinal("nombre"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("nombre")),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                            ? string.Empty
                            : reader.GetString(reader.GetOrdinal("descripcion")),
                        Precio = reader.IsDBNull(reader.GetOrdinal("precio"))
                            ? 0m
                            : Convert.ToDecimal(reader["precio"]),
                        Stock = reader.IsDBNull(reader.GetOrdinal("stock"))
                            ? 0
                            : Convert.ToInt32(reader["stock"])
                    };
                }
            }
        }

        /// <summary>
        /// Inserta un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="producto">Datos del producto a insertar.</param>
        /// <returns><c>true</c> si se insertó al menos una fila; <c>false</c> si ocurrió un error o no se insertó ninguna fila.</returns>
        public bool InsertarProducto(Producto producto)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = @"INSERT INTO productos (codigo, nombre, descripcion, precio, stock) values (@codigo, @nombre, @descripcion, @precio , @stock)";

                try
                {
                    conexion.Open();

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@codigo", producto.Codigo ?? string.Empty);
                        comando.Parameters.AddWithValue("@nombre", producto.Nombre ?? string.Empty);
                        comando.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? string.Empty);
                        comando.Parameters.AddWithValue("@precio", producto.Precio);
                        comando.Parameters.AddWithValue("@stock", producto.Stock);

                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de un producto existente, identificado por su Id.
        /// </summary>
        /// <param name="producto">Producto con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si se actualizó al menos una fila; <c>false</c> si ocurrió un error o no se actualizó ninguna fila.</returns>
        public bool EditarProducto(Producto producto)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = @"UPDATE productos
                            SET codigo = @codigo,
                                nombre = @nombre,   
                                descripcion = @descripcion,
                                precio = @precio,
                                stock = @stock
                            WHERE id = @id";

                try
                {
                    conexion.Open();

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", producto.Id);
                        comando.Parameters.AddWithValue("@codigo", producto.Codigo ?? string.Empty);
                        comando.Parameters.AddWithValue("@nombre", producto.Nombre ?? string.Empty);
                        comando.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? string.Empty);
                        comando.Parameters.AddWithValue("@precio", producto.Precio);
                        comando.Parameters.AddWithValue("@stock", producto.Stock);

                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Elimina un producto de la base de datos por su Id.
        /// </summary>
        /// <param name="id">Id del producto a eliminar.</param>
        /// <returns><c>true</c> si se eliminó al menos una fila; <c>false</c> si ocurrió un error o no se eliminó ninguna fila.</returns>
        public bool EliminarProducto(int id)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = "DELETE FROM productos WHERE id = @id";

                try
                {
                    conexion.Open();

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Verifica si un producto tiene ventas asociadas en la tabla detalle_ventas.  
        /// </summary>
        /// <param name="id">Id del producto a verificar.</param>
        /// <returns><c>true</c> si el producto tiene ventas asociadas; <c>false</c> en caso contrario.</returns>
        public bool TieneVentas(int id)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                conexion.Open();

                const string query =
                    "SELECT EXISTS(SELECT 1 FROM detalle_ventas WHERE producto_id = @id)";

                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    return Convert.ToInt32(comando.ExecuteScalar()) == 1;
                }
            }
        }

        /// <summary>
        /// Actualiza el stock de un producto.
        /// </summary>
        /// <param name="id">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <returns><c>true</c> si se actualizó al menos una fila; <c>false</c> si ocurrió un error o no se actualizó ninguna fila.</returns>
        public bool ActualizarStock(int id, int nuevoStock)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = "UPDATE productos SET stock = @stock WHERE id = @id";
                try
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@stock", nuevoStock);
                        comando.Parameters.AddWithValue("@id", id);
                        return comando.ExecuteNonQuery() > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        // Overload útil cuando hay transacción abierta
        /// <summary>
        /// Variante transaccional de <see cref="ActualizarStock(int, int)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>),
        /// reutilizando la misma conexión y transacción en lugar de abrir una nueva.
        /// </summary>
        /// <param name="productoId">Id del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a asignar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si se actualizó al menos una fila.</returns>
        public bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion)
        {
            string query = @"UPDATE productos
          SET stock = @stock
          WHERE id = @id";

            using (MySqlCommand comando = new MySqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@stock", nuevoStock);
                comando.Parameters.AddWithValue("@id", productoId);
                return comando.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Verifica si ya existe un código en la tabla productos. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        /// </summary>
        /// <param name="codigo">Código a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese código; <c>false</c> si no existe o si ocurrió un error.</returns>
        public bool ExisteCodigo(string codigo, int? excludeId = null)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM productos WHERE codigo = @codigo AND id <> @id"
                    : "SELECT COUNT(1) FROM productos WHERE codigo = @codigo";

                try
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@codigo", codigo ?? string.Empty);
                        if (excludeId.HasValue)
                            comando.Parameters.AddWithValue("@id", excludeId.Value);

                        object result = comando.ExecuteScalar();
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Verifica si ya existe un nombre en la tabla productos. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        /// </summary>
        /// <param name="nombre">Nombre a verificar.</param>
        /// <param name="excludeId">Id de producto a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un producto con ese nombre; <c>false</c> si no existe o si ocurrió un error.</returns>
        public bool ExisteNombre(string nombre, int? excludeId = null)
        {
            using (MySqlConnection conexion = conexionDB.ObtenerConexion())
            {
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM productos WHERE nombre = @nombre AND id <> @id"
                    : "SELECT COUNT(1) FROM productos WHERE nombre = @nombre";

                try
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre ?? string.Empty);
                        if (excludeId.HasValue)
                            comando.Parameters.AddWithValue("@id", excludeId.Value);

                        object result = comando.ExecuteScalar();
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}