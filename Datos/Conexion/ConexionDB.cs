using MySql.Data.MySqlClient;

namespace SistemaVentas.Datos.Conexion
{
    public class ConexionDB
    {
        private string cadenaConexion =
            "server=localhost;database=sistema_ventas;user=root;password=root123;";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);

            return conexion;
        }
    }
}