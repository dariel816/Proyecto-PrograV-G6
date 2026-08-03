using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    /// <summary>
    /// Acceso a datos de la tabla <c>Usuarios</c> en MySQL mediante ADO.NET (MySql.Data.MySqlClient).
    /// </summary>
    public class UsuarioDAO
    {
        private string _connectionString;

        /// <summary>
        /// Crea el DAO indicando la cadena de conexión a usar en cada operación.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos MySQL.</param>
        public UsuarioDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Busca un usuario activo por su nombre de usuario, trayendo su hash de clave para
        /// que la capa de Negocio pueda validar la contraseña.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a buscar.</param>
        /// <returns>El usuario encontrado, o <c>null</c> si no existe.</returns>
        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, NombreUsuario, ClaveHash, Rol, Activo FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario ?? string.Empty);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreUsuario = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                ClaveHash = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Rol = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Activo = !reader.IsDBNull(4) && reader.GetBoolean(4)
                            };
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de todos los usuarios encontrados (puede estar vacía).</returns>
        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, NombreUsuario, ClaveHash, Rol, Activo FROM Usuarios";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreUsuario = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                ClaveHash = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                Rol = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Activo = !reader.IsDBNull(4) && reader.GetBoolean(4)
                            });
                        }
                    }
                }
            }

            return usuarios;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="usuario">Datos del usuario a insertar (con la contraseña ya hasheada).</param>
        /// <returns><c>true</c> si se insertó al menos una fila.</returns>
        public bool Crear(Usuario usuario)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Usuarios (NombreUsuario, ClaveHash, Rol, Activo) VALUES (@nombreUsuario, @claveHash, @rol, @activo)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario ?? string.Empty);
                    command.Parameters.AddWithValue("@claveHash", usuario.ClaveHash ?? string.Empty);
                    command.Parameters.AddWithValue("@rol", usuario.Rol ?? string.Empty);
                    command.Parameters.AddWithValue("@activo", usuario.Activo);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Verifica si ya existe un nombre de usuario, para no permitir duplicados al crear cuentas.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a verificar.</param>
        /// <returns><c>true</c> si ya existe un usuario con ese nombre.</returns>
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Usuarios WHERE NombreUsuario = @nombreUsuario";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario ?? string.Empty);

                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result) > 0;
                }
            }
        }
    }
}
