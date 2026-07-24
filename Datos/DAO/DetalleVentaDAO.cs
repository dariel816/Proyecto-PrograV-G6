using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    public class DetalleVentaDAO
    {
        private readonly ConexionDB conexionDB = new ConexionDB();

        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            string query =
                @"SELECT id,
                         venta_id,
                         producto_id,
                         cantidad,
                         precio,
                         subtotal
                  FROM detalle_ventas
                  WHERE venta_id = @venta_id";

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
                            "@venta_id", ventaId);

                        using (MySqlDataReader reader =
                               comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DetalleVenta detalle =
                                    new DetalleVenta
                                    {
                                        Id = Convert.ToInt32(
                                            reader["id"]),

                                        VentaId = Convert.ToInt32(
                                            reader["venta_id"]),

                                        ProductoId = Convert.ToInt32(
                                            reader["producto_id"]),

                                        Cantidad = Convert.ToInt32(
                                            reader["cantidad"]),

                                        PrecioUnitario =
                                            Convert.ToDecimal(
                                                reader["precio"]),

                                        Subtotal = Convert.ToDecimal(
                                            reader["subtotal"])
                                    };

                                lista.Add(detalle);
                            }
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al obtener los detalles de la venta " +
                    $"con Id={ventaId}: {ex.Message}",
                    ex);
            }
        }

        public DetalleVenta? ObtenerDetallePorId(int id)
        {
            string query =
                @"SELECT id,
                         venta_id,
                         producto_id,
                         cantidad,
                         precio,
                         subtotal
                  FROM detalle_ventas
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
                                return new DetalleVenta
                                {
                                    Id = reader.IsDBNull(
                                        reader.GetOrdinal("id"))
                                        ? 0
                                        : Convert.ToInt32(
                                            reader["id"]),

                                    VentaId = reader.IsDBNull(
                                        reader.GetOrdinal("venta_id"))
                                        ? 0
                                        : Convert.ToInt32(
                                            reader["venta_id"]),

                                    ProductoId = reader.IsDBNull(
                                        reader.GetOrdinal("producto_id"))
                                        ? 0
                                        : Convert.ToInt32(
                                            reader["producto_id"]),

                                    Cantidad = reader.IsDBNull(
                                        reader.GetOrdinal("cantidad"))
                                        ? 0
                                        : Convert.ToInt32(
                                            reader["cantidad"]),

                                    PrecioUnitario = reader.IsDBNull(
                                        reader.GetOrdinal("precio"))
                                        ? 0m
                                        : Convert.ToDecimal(
                                            reader["precio"]),

                                    Subtotal = reader.IsDBNull(
                                        reader.GetOrdinal("subtotal"))
                                        ? 0m
                                        : Convert.ToDecimal(
                                            reader["subtotal"])
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
                    $"Error al obtener el detalle con Id={id}: " +
                    ex.Message,
                    ex);
            }
        }

        public bool InsertarDetalleVenta(DetalleVenta detalle)
        {
            string query =
                @"INSERT INTO detalle_ventas
                  (venta_id,
                   producto_id,
                   cantidad,
                   precio,
                   subtotal)
                  VALUES
                  (@venta_id,
                   @producto_id,
                   @cantidad,
                   @precio,
                   @subtotal)";

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
                            "@venta_id", detalle.VentaId);

                        comando.Parameters.AddWithValue(
                            "@producto_id", detalle.ProductoId);

                        comando.Parameters.AddWithValue(
                            "@cantidad", detalle.Cantidad);

                        comando.Parameters.AddWithValue(
                            "@precio", detalle.PrecioUnitario);

                        comando.Parameters.AddWithValue(
                            "@subtotal", detalle.Subtotal);

                        int resultado =
                            comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al insertar el detalle de la venta: " +
                    ex.Message,
                    ex);
            }
        }

        public bool EditarDetalleVenta(DetalleVenta detalle)
        {
            string query =
                @"UPDATE detalle_ventas
                  SET venta_id = @venta_id,
                      producto_id = @producto_id,
                      cantidad = @cantidad,
                      precio = @precio,
                      subtotal = @subtotal
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
                            "@id", detalle.Id);

                        comando.Parameters.AddWithValue(
                            "@venta_id", detalle.VentaId);

                        comando.Parameters.AddWithValue(
                            "@producto_id", detalle.ProductoId);

                        comando.Parameters.AddWithValue(
                            "@cantidad", detalle.Cantidad);

                        comando.Parameters.AddWithValue(
                            "@precio", detalle.PrecioUnitario);

                        comando.Parameters.AddWithValue(
                            "@subtotal", detalle.Subtotal);

                        int resultado =
                            comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al editar el detalle con Id={detalle.Id}: " +
                    ex.Message,
                    ex);
            }
        }

        public bool EliminarDetalleVenta(int id)
        {
            string query =
                @"DELETE FROM detalle_ventas
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

                        int resultado =
                            comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al eliminar el detalle con Id={id}: " +
                    ex.Message,
                    ex);
            }
        }

        public bool EliminarDetallesPorVenta(int ventaId)
        {
            string query =
                @"DELETE FROM detalle_ventas
                  WHERE venta_id = @venta_id";

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
                            "@venta_id", ventaId);

                        int resultado =
                            comando.ExecuteNonQuery();

                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al eliminar los detalles de la venta " +
                    $"con Id={ventaId}: {ex.Message}",
                    ex);
            }
        }

        public bool InsertarDetalleVenta(
            DetalleVenta detalle,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            string query =
                @"INSERT INTO detalle_ventas
                  (venta_id,
                   producto_id,
                   cantidad,
                   precio,
                   subtotal)
                  VALUES
                  (@venta_id,
                   @producto_id,
                   @cantidad,
                   @precio,
                   @subtotal)";

            try
            {
                using (MySqlCommand comando =
                       new MySqlCommand(
                           query,
                           conexion,
                           transaccion))
                {
                    comando.Parameters.AddWithValue(
                        "@venta_id", detalle.VentaId);

                    comando.Parameters.AddWithValue(
                        "@producto_id", detalle.ProductoId);

                    comando.Parameters.AddWithValue(
                        "@cantidad", detalle.Cantidad);

                    comando.Parameters.AddWithValue(
                        "@precio", detalle.PrecioUnitario);

                    comando.Parameters.AddWithValue(
                        "@subtotal", detalle.Subtotal);

                    int resultado =
                        comando.ExecuteNonQuery();

                    return resultado > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error al insertar el detalle del producto " +
                    $"Id={detalle.ProductoId} dentro de la transacción: " +
                    ex.Message,
                    ex);
            }
        }
    }
}