using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    public class VentaDAO
    {
        private readonly ConexionDB conexionDB = new ConexionDB();

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