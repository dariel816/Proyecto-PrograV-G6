using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    /// <summary>
    /// Acceso a datos de la tabla <c>detalle_ventas</c> en MySQL mediante ADO.NET
    /// (MySql.Data.MySqlClient).
    /// </summary>
    public class DetalleVentaDAO
    {
        private readonly ConexionDB conexionDB = new ConexionDB();

        /// <summary>
        /// Obtiene todos los detalles (líneas de producto) asociados a una venta.
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean obtener.</param>
        /// <returns>Lista de detalles de la venta (puede estar vacía).</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al consultar la base de datos.</exception>
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

        /// <summary>
        /// Obtiene y bloquea los detalles de una venta dentro de una transacción existente.
        /// </summary>
        /// <param name="ventaId">Id de la venta.</param>
        /// <param name="conexion">Conexión abierta de la transacción actual.</param>
        /// <param name="transaccion">Transacción MySQL actual.</param>
        /// <returns>Detalles bloqueados de la venta.</returns>
        public List<DetalleVenta> ObtenerDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            const string query = @"SELECT id, venta_id, producto_id, cantidad, precio, subtotal
                                   FROM detalle_ventas
                                   WHERE venta_id = @venta_id
                                   ORDER BY producto_id
                                   FOR UPDATE";

            List<DetalleVenta> detalles = new List<DetalleVenta>();

            using (MySqlCommand comando = new MySqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@venta_id", ventaId);

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleVenta
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            VentaId = Convert.ToInt32(reader["venta_id"]),
                            ProductoId = Convert.ToInt32(reader["producto_id"]),
                            Cantidad = Convert.ToInt32(reader["cantidad"]),
                            PrecioUnitario = Convert.ToDecimal(reader["precio"]),
                            Subtotal = Convert.ToDecimal(reader["subtotal"])
                        });
                    }
                }
            }

            return detalles;
        }

        /// <summary>
        /// Busca un detalle de venta por su identificador.
        /// </summary>
        /// <param name="id">Id del detalle a buscar.</param>
        /// <returns>El detalle encontrado, o <c>null</c> si no existe.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al consultar la base de datos.</exception>
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

        /// <summary>
        /// Inserta un nuevo detalle de venta en la base de datos, abriendo su propia conexión.
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <returns><c>true</c> si se insertó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al insertar en la base de datos.</exception>
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

        /// <summary>
        /// Actualiza los datos de un detalle de venta existente, identificado por su Id.
        /// </summary>
        /// <param name="detalle">Detalle con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si se actualizó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al actualizar la base de datos.</exception>
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

        /// <summary>
        /// Elimina un detalle de venta de la base de datos por su Id.
        /// </summary>
        /// <param name="id">Id del detalle a eliminar.</param>
        /// <returns><c>true</c> si se eliminó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al eliminar en la base de datos.</exception>
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

        /// <summary>
        /// Elimina todos los detalles asociados a una venta (por ejemplo, al eliminar la venta completa).
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean eliminar.</param>
        /// <returns><c>true</c> si se eliminó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al eliminar en la base de datos.</exception>
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

        /// <summary>
        /// Elimina todos los detalles de una venta dentro de una transacción existente.
        /// </summary>
        /// <param name="ventaId">Id de la venta.</param>
        /// <param name="conexion">Conexión abierta de la transacción actual.</param>
        /// <param name="transaccion">Transacción MySQL actual.</param>
        /// <returns><c>true</c> si la operación se ejecutó correctamente.</returns>
        public bool EliminarDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            const string query = @"DELETE FROM detalle_ventas
                                   WHERE venta_id = @venta_id";

            using (MySqlCommand comando = new MySqlCommand(query, conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@venta_id", ventaId);
                comando.ExecuteNonQuery();
                return true;
            }
        }

        /// <summary>
        /// Variante transaccional de <see cref="InsertarDetalleVenta(DetalleVenta)"/>, para
        /// usarse dentro de una transacción ya existente (por ejemplo, la de
        /// <c>VentaNegocio.CrearVenta</c>), reutilizando la misma conexión y transacción
        /// en lugar de abrir una nueva.
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si se insertó al menos una fila.</returns>
        /// <exception cref="Exception">Se relanza si ocurre un error al insertar en la base de datos.</exception>
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