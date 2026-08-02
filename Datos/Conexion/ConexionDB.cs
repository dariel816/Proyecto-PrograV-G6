using MySql.Data.MySqlClient;

namespace SistemaVentas.Datos.Conexion
{
    /// <summary>
    /// Encapsula la cadena de conexión a la base de datos MySQL y provee
    /// instancias de <see cref="MySqlConnection"/> listas para usar en los DAO.
    /// </summary>
    public class ConexionDB
    {
        private string cadenaConexion =
            "server=localhost;database=sistema_ventas;user=root;password=root123;";

        /// <summary>
        /// Crea una nueva conexión MySQL configurada con la cadena de conexión
        /// interna. La conexión se devuelve cerrada; quien la reciba es
        /// responsable de abrirla (Open) y liberarla (Dispose/using).
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="MySqlConnection"/> sin abrir.</returns>
        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);

            return conexion;
        }
    }
}