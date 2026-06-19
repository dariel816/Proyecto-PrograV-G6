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
    

    public class ClienteDAO
    {
        ConexionDB conexionDB = new ConexionDB();
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM clientes";


            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = Convert.ToInt32(reader["id"]);
                    cliente.Nombre = reader["nombre"].ToString();
                    
                    cliente.Correo = reader["correo"].ToString();
                    cliente.Telefono = reader["telefono"].ToString();
                    lista.Add(cliente);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
                

            }
            return lista;
        }
        public bool InsertarCliente(Cliente cliente)
        {  // Método para insertar un nuevo cliente en la base de datos
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "INSERT INTO clientes (nombre, correo, telefono) VALUES (@nombre, @correo, @telefono)";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                
                comando.Parameters.AddWithValue("@correo", cliente.Correo);
                comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                int resultado = comando.ExecuteNonQuery();
                conexion.Close();
                return resultado > 0; // Retorna true si se insertó correctamente
            }
            catch
            {
                conexion.Close();
                return false; // Retorna false si hubo un error
            }
        }

        public bool EditarCliente(Cliente cliente)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();

            string query = @"UPDATE clientes
                             SET nombre=@nombre,
                                 telefono=@telefono,
                                 correo=@correo
                             WHERE id=@id";

            try
            {
                conexion.Open();

                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", cliente.Id);
                comando.Parameters.AddWithValue("@nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
                comando.Parameters.AddWithValue("@correo", cliente.Correo);

                comando.ExecuteNonQuery();

                conexion.Close();
                return true;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }

        public bool EliminarCliente(int id)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();

            string query = "DELETE FROM clientes WHERE id=@id";

            try
            {
                conexion.Open();

                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();

                conexion.Close();
                return true;
            }
            catch
            {
                conexion.Close();
                return false;
            }
        }
    }
}
