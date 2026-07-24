using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Conexion;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.DAO
{
    public class DetalleVentaDAO
    {
        ConexionDB conexionDB = new ConexionDB();

        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM detalle_ventas WHERE venta_id = @venta_id";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@venta_id", ventaId);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    DetalleVenta detalle = new DetalleVenta();
                    detalle.Id = Convert.ToInt32(reader["id"]);
                    detalle.VentaId = Convert.ToInt32(reader["venta_id"]);
                    detalle.ProductoId = Convert.ToInt32(reader["producto_id"]);
                    detalle.Cantidad = Convert.ToInt32(reader["cantidad"]);
                    detalle.PrecioUnitario = Convert.ToDecimal(reader["precio_unitario"]);
                    detalle.Subtotal = Convert.ToDecimal(reader["subtotal"]);
                    lista.Add(detalle);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }
            return lista;
        }

        public DetalleVenta? ObtenerDetallePorId(int id)
        {
            DetalleVenta? detalle = null;
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM detalle_ventas WHERE id = @id";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    detalle = new DetalleVenta();
                    detalle.Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : Convert.ToInt32(reader["id"]);
                    detalle.VentaId = reader.IsDBNull(reader.GetOrdinal("venta_id")) ? 0 : Convert.ToInt32(reader["venta_id"]);
                    detalle.ProductoId = reader.IsDBNull(reader.GetOrdinal("producto_id")) ? 0 : Convert.ToInt32(reader["producto_id"]);
                    detalle.Cantidad = reader.IsDBNull(reader.GetOrdinal("cantidad")) ? 0 : Convert.ToInt32(reader["cantidad"]);
                    detalle.PrecioUnitario = reader.IsDBNull(reader.GetOrdinal("precio_unitario")) ? 0m : Convert.ToDecimal(reader["precio_unitario"]);
                    detalle.Subtotal = reader.IsDBNull(reader.GetOrdinal("subtotal")) ? 0m : Convert.ToDecimal(reader["subtotal"]);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }
            return detalle;
        }

        public bool InsertarDetalleVenta(DetalleVenta detalle)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "INSERT INTO detalle_ventas (venta_id, producto_id, cantidad, precio_unitario, subtotal) VALUES (@venta_id, @producto_id, @cantidad, @precio_unitario, @subtotal)";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@venta_id", detalle.VentaId);
                comando.Parameters.AddWithValue("@producto_id", detalle.ProductoId);
                comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                comando.Parameters.AddWithValue("@precio_unitario", detalle.PrecioUnitario);
                comando.Parameters.AddWithValue("@subtotal", detalle.Subtotal);

                int resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado > 0;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }

        public bool EditarDetalleVenta(DetalleVenta detalle)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "UPDATE detalle_ventas SET venta_id=@venta_id, producto_id=@producto_id, cantidad=@cantidad, precio_unitario=@precio_unitario, subtotal=@subtotal WHERE id=@id";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", detalle.Id);
                comando.Parameters.AddWithValue("@venta_id", detalle.VentaId);
                comando.Parameters.AddWithValue("@producto_id", detalle.ProductoId);
                comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                comando.Parameters.AddWithValue("@precio_unitario", detalle.PrecioUnitario);
                comando.Parameters.AddWithValue("@subtotal", detalle.Subtotal);

                int resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado > 0;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }

        public bool EliminarDetalleVenta(int id)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "DELETE FROM detalle_ventas WHERE id = @id";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                int resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado > 0;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }

        public bool EliminarDetallesPorVenta(int ventaId)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "DELETE FROM detalle_ventas WHERE venta_id = @venta_id";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@venta_id", ventaId);
                int resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado > 0;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }
    }
}
