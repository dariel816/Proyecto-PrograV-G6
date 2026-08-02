using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    /// <summary>
    /// Acceso a datos de la tabla <c>ventas</c> en MySQL mediante ADO.NET
    /// (MySql.Data.MySqlClient).
    /// </summary>
    public class VentaDAO
    {
        private readonly ConexionDB conexionDB = new ConexionDB();

        /// <summary>
        /// Obtiene todas las ventas registradas en la tabla <c>ventas</c>.
        /// </summary>
        /// <returns>Lista de todas las ventas encontradas (puede estar vacía).</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al consultar la base de datos.</exception>
        public List<Venta> ObtenerVentas()
        {
            List<Venta> lista = new List<Venta>();

            string query =
                @"SELECT id, fecha, cliente_id, total
                  FROM ventas";

            try
            {
                using (MySqlConnection conexion =
                       conexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (MySqlCommand comando =
                           new MySqlCommand(query, conexion))
                    {
                        using (MySqlDataReader reader =
                               comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Fecha = Convert.ToDateTime(reader["fecha"]),
                                    ClienteId = Convert.ToInt32(
                                        reader["cliente_id"]),
                                    Total = Convert.ToDecimal(reader["total"])
                                };

                                lista.Add(venta);
                            }
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al obtener la lista de ventas: " +
                    ex.Message,
                    ex);
            }
        }

        /// <summary>
        /// Busca una venta por su identificador.
        /// </summary>
        /// <param name="id">Id de la venta a buscar.</param>
        /// <returns>La venta encontrada, o <c>null</c> si no existe.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al consultar la base de datos.</exception>
        public Venta? ObtenerVentaPorId(int id)
        {
            string query =
                @"SELECT id, fecha, cliente_id, total
                  FROM ventas
                  WHERE id = @id";

            try
            {
                using (MySqlConnection conexion =
                       conexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (MySqlCommand comando =
                           new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader =
                               comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Venta
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Fecha = Convert.ToDateTime(
                                        reader["fecha"]),
                                    ClienteId = Convert.ToInt32(
                                        reader["cliente_id"]),
                                    Total = Convert.ToDecimal(
                                        reader["total"])
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al obtener la venta con Id={id}: " +
                    ex.Message,
                    ex);
            }
        }

        /// <summary>
        /// Inserta una nueva venta en la base de datos, abriendo su propia conexión.
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        /// <exception cref="Exception">Se lanza si la base de datos no devuelve el Id, o se relanza ante un error de acceso a datos.</exception>
        public int InsertarVenta(Venta venta)
        {
            string query =
                @"INSERT INTO ventas
                  (fecha, cliente_id, total)
                  VALUES
                  (@fecha, @cliente_id, @total);

                  SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlConnection conexion =
                       conexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (MySqlCommand comando =
                           new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@fecha", venta.Fecha);

                        comando.Parameters.AddWithValue(
                            "@cliente_id", venta.ClienteId);

                        comando.Parameters.AddWithValue(
                            "@total", venta.Total);

                        object? resultado = comando.ExecuteScalar();

                        if (resultado == null ||
                            resultado == DBNull.Value)
                        {
                            throw new Exception(
                                "La base de datos no devolvió " +
                                "el Id de la venta.");
                        }

                        return Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al insertar la venta: " +
                    ex.Message,
                    ex);
            }
        }

        /// <summary>
        /// Actualiza los datos de una venta existente, identificada por su Id.
        /// </summary>
        /// <param name="venta">Venta con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si se actualizó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al actualizar la base de datos.</exception>
        public bool EditarVenta(Venta venta)
        {
            string query =
                @"UPDATE ventas
                  SET fecha = @fecha,
                      cliente_id = @cliente_id,
                      total = @total
                  WHERE id = @id";

            try
            {
                using (MySqlConnection conexion =
                       conexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (MySqlCommand comando =
                           new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue(
                            "@id", venta.Id);

                        comando.Parameters.AddWithValue(
                            "@fecha", venta.Fecha);

                        comando.Parameters.AddWithValue(
                            "@cliente_id", venta.ClienteId);

                        comando.Parameters.AddWithValue(
                            "@total", venta.Total);

                        int resultado = comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al editar la venta con Id={venta.Id}: " +
                    ex.Message,
                    ex);
            }
        }

        /// <summary>
        /// Elimina una venta de la base de datos por su Id.
        /// </summary>
        /// <param name="id">Id de la venta a eliminar.</param>
        /// <returns><c>true</c> si se eliminó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al eliminar en la base de datos.</exception>
        public bool EliminarVenta(int id)
        {
            string query =
                @"DELETE FROM ventas
                  WHERE id = @id";

            try
            {
                using (MySqlConnection conexion =
                       conexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (MySqlCommand comando =
                           new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@id", id);

                        int resultado = comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al eliminar la venta con Id={id}: " +
                    ex.Message,
                    ex);
            }
        }

        /// <summary>
        /// Variante transaccional de <see cref="InsertarVenta(Venta)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de
        /// <c>VentaNegocio.CrearVenta</c>), reutilizando la misma conexión y transacción
        /// en lugar de abrir una nueva.
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        /// <exception cref="Exception">Se lanza si la base de datos no devuelve el Id, o se relanza ante un error de acceso a datos.</exception>
        public int InsertarVenta(
            Venta venta,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            string query =
                @"INSERT INTO ventas
                  (fecha, cliente_id, total)
                  VALUES
                  (@fecha, @cliente_id, @total);

                  SELECT LAST_INSERT_ID();";

            try
            {
                using (MySqlCommand comando =
                       new MySqlCommand(
                           query,
                           conexion,
                           transaccion))
                {
                    comando.Parameters.AddWithValue(
                        "@fecha", venta.Fecha);

                    comando.Parameters.AddWithValue(
                        "@cliente_id", venta.ClienteId);

                    comando.Parameters.AddWithValue(
                        "@total", venta.Total);

                    object? resultado = comando.ExecuteScalar();

                    if (resultado == null ||
                        resultado == DBNull.Value)
                    {
                        throw new Exception(
                            "La base de datos no devolvió " +
                            "el Id de la venta.");
                    }

                    return Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al insertar la venta dentro " +
                    "de la transacción: " +
                    ex.Message,
                    ex);
            }
        }
    }
}