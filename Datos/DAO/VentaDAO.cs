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
    public class VentaDAO
    {
        ConexionDB conexionDB = new ConexionDB();

        public List<Venta> ObtenerVentas()
        {
            List<Venta> lista = new List<Venta>();
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM ventas";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta();
                    venta.Id = Convert.ToInt32(reader["id"]);
                    venta.Fecha = Convert.ToDateTime(reader["fecha"]);
                    venta.ClienteId = Convert.ToInt32(reader["cliente_id"]);
                    venta.Total = Convert.ToDecimal(reader["total"]);
                    lista.Add(venta);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }
            return lista;
        }

        public Venta ObtenerVentaPorId(int id)
        {
            Venta venta = null;
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM ventas WHERE id = @id";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    venta = new Venta();
                    venta.Id = Convert.ToInt32(reader["id"]);
                    venta.Fecha = Convert.ToDateTime(reader["fecha"]);
                    venta.ClienteId = Convert.ToInt32(reader["cliente_id"]);
                    venta.Total = Convert.ToDecimal(reader["total"]);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }
            return venta;
        }

        public int InsertarVenta(Venta venta)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "INSERT INTO ventas (fecha, cliente_id, total) VALUES (@fecha, @cliente_id, @total); SELECT LAST_INSERT_ID();";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@fecha", venta.Fecha);
                comando.Parameters.AddWithValue("@cliente_id", venta.ClienteId);
                comando.Parameters.AddWithValue("@total", venta.Total);

                object resultado = comando.ExecuteScalar();
                conexion.Close();
                return Convert.ToInt32(resultado);
            }
            catch
            {
                conexion.Close();
                return 0;
            }
        }

        public bool EditarVenta(Venta venta)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "UPDATE ventas SET fecha=@fecha, cliente_id=@cliente_id, total=@total WHERE id=@id";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", venta.Id);
                comando.Parameters.AddWithValue("@fecha", venta.Fecha);
                comando.Parameters.AddWithValue("@cliente_id", venta.ClienteId);
                comando.Parameters.AddWithValue("@total", venta.Total);

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

        public bool EliminarVenta(int id)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "DELETE FROM ventas WHERE id = @id";
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
    }
}
