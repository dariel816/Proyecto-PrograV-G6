using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    public class ProductoDAO
    {
        ConexionDB conexionDB = new ConexionDB();

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

        // Verifica si ya existe un código en la tabla productos. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
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

        // Verifica si ya existe un nombre en la tabla productos. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
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
