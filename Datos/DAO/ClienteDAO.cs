using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    public class ClienteDAO
    {
        private string _connectionString;

        public ClienteDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Correo, Telefono FROM Clientes";

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

        public Cliente? ObtenerPorId(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Correo, Telefono FROM Clientes WHERE Id = @id";

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

        public bool Crear(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Clientes (Nombre, Correo, Telefono) VALUES (@nombre, @correo, @telefono)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", cliente.Nombre ?? string.Empty);
                    command.Parameters.AddWithValue("@correo", cliente.Correo ?? string.Empty);
                    command.Parameters.AddWithValue("@telefono", cliente.Telefono ?? string.Empty);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Actualizar(Cliente cliente)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Clientes SET Nombre = @nombre, Correo = @correo, Telefono = @telefono WHERE Id = @id";

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

        public bool Eliminar(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Clientes WHERE Id = @id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // Verifica si ya existe un correo en la tabla Clientes. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        public bool ExisteCorreo(string correo, int? excludeId = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM Clientes WHERE Correo = @correo AND Id <> @id"
                    : "SELECT COUNT(1) FROM Clientes WHERE Correo = @correo";

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

        // Verifica si ya existe un teléfono en la tabla Clientes. Si excludeId tiene valor, lo excluye de la verificación (útil al actualizar).
        public bool ExisteTelefono(string telefono, int? excludeId = null)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = excludeId.HasValue
                    ? "SELECT COUNT(1) FROM Clientes WHERE Telefono = @telefono AND Id <> @id"
                    : "SELECT COUNT(1) FROM Clientes WHERE Telefono = @telefono";

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
