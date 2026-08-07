using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    /// <summary>
    /// Acceso a datos de la tabla <c>clientes</c> en MySQL mediante ADO.NET
    /// (MySql.Data.MySqlClient).
    /// </summary>
    public class ClienteDAO
    {
        private string _connectionString;

        /// <summary>
        /// Crea el DAO indicando la cadena de conexión a usar en cada operación.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos MySQL.</param>
        public ClienteDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados en la tabla <c>clientes</c>.
        /// </summary>
        /// <returns>Lista de todos los clientes encontrados (puede estar vacía).</returns>
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT id, nombre, correo, telefono FROM clientes";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Correo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Telefono = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        /// <summary>
        /// Busca un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente a buscar.</param>
        /// <returns>El cliente encontrado, o <c>null</c> si no existe.</returns>
        public Cliente? ObtenerPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT id, nombre, correo, telefono FROM clientes WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                Correo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Telefono = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                            };
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Inserta un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="cliente">Datos del cliente a insertar.</param>
        /// <returns><c>true</c> si se insertó al menos una fila.</returns>
        public bool Crear(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO clientes (nombre, correo, telefono) VALUES (@nombre, @correo, @telefono)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.Nombre ?? string.Empty);
                    command.Parameters.AddWithValue("@correo", cliente.Correo ?? string.Empty);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono ?? string.Empty);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente, identificado por su Id.
        /// </summary>
        /// <param name="cliente">Cliente con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si se actualizó al menos una fila.</returns>
        public bool Actualizar(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE clientes SET nombre = @nombre, correo = @correo, telefono = @telefono WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", cliente.Id);
                    command.Parameters.AddWithValue("@nombre", cliente.Nombre ?? string.Empty);
                    command.Parameters.AddWithValue("@correo", cliente.Correo ?? string.Empty);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono ?? string.Empty);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Elimina un cliente de la base de datos por su Id.
        /// </summary>
        /// <param name="id">Id del cliente a eliminar.</param>
        /// <returns><c>true</c> si se eliminó al menos una fila.</returns>
        public bool Eliminar(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM clientes WHERE id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Verifica si ya existe un correo en la tabla clientes. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        /// </summary>
        /// <param name="correo">Correo a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese correo.</returns>
        public bool ExisteCorreo(string correo, int? excludeId = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM clientes WHERE correo = @correo AND id <> @id"
                    : "SELECT COUNT(1) FROM clientes WHERE correo = @correo";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@correo", correo ?? string.Empty);
                    if (excludeId.HasValue)
                        command.Parameters.AddWithValue("@id", excludeId.Value);

                    object result = command.ExecuteScalar();
                    int count = Convert.ToInt32(result);
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Verifica si ya existe un teléfono en la tabla clientes. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        /// </summary>
        /// <param name="telefono">Teléfono a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese teléfono.</returns>
        public bool ExisteTelefono(string telefono, int? excludeId = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM clientes WHERE telefono = @telefono AND id <> @id"
                    : "SELECT COUNT(1) FROM clientes WHERE telefono = @telefono";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@telefono", telefono ?? string.Empty);
                    if (excludeId.HasValue)
                        command.Parameters.AddWithValue("@id", excludeId.Value);

                    object result = command.ExecuteScalar();
                    int count = Convert.ToInt32(result);
                    return count > 0;
                }
            }
        }
    }
}