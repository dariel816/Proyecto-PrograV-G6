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
    public class ProductoDAO
    {
        ConexionDB conexionDB = new ConexionDB();

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lista = new List<Producto>();

            MySqlConnection conexion = conexionDB.ObtenerConexion();

            string query = "SELECT * FROM productos";

            try
            {
                conexion.Open();

                MySqlCommand comando = new MySqlCommand(query, conexion);

                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(reader["id"]);
                    producto.Codigo = reader["codigo"].ToString();
                    producto.Nombre = reader["nombre"].ToString();
                    producto.Descripcion = reader["descripcion"].ToString();
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.Stock = Convert.ToInt32(reader["stock"]);

                    lista.Add(producto);
                }

                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }

            return lista;
        }

        public Producto ObtenerProductoPorId(int id)
        {
            Producto producto = null;
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = "SELECT * FROM productos WHERE id = @id";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["id"]);
                    producto.Codigo = reader["codigo"].ToString();
                    producto.Nombre = reader["nombre"].ToString();
                    producto.Descripcion = reader["descripcion"].ToString();
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.Stock = Convert.ToInt32(reader["stock"]);
                }
                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }
            return producto;
        }

        public bool InsertarProducto(Producto producto)
        {  // Método para insertar un nuevo producto en la base de datos

            MySqlConnection conexion = conexionDB.ObtenerConexion();

            string query = @"INSERT INTO productos (codigo, nombre, descripcion, precio, stock) values (@codigo, @nombre, @descripcion, @precio , @stock)"; // Consulta SQL para insertar un nuevo producto utilizando parámetros para evitar inyección SQL
            try
            {
                conexion.Open(); // Abrir la conexión a la base de datos

                MySqlCommand comando = new MySqlCommand(query, conexion); // Crear un comando SQL con la consulta y la conexión
                comando.Parameters.AddWithValue("@codigo", producto.Codigo); // Agregar los valores de los parámetros al comando utilizando las propiedades del objeto producto
                comando.Parameters.AddWithValue("@nombre", producto.Nombre); // Agregar el valor del parámetro @nombre con el valor de producto.Nombre
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion); // Agregar el valor del parámetro @descripcion con el valor de producto.Descripcion
                comando.Parameters.AddWithValue("@precio", producto.Precio);  // Agregar el valor del parámetro @precio con el valor de producto.Precio
                comando.Parameters.AddWithValue("@stock", producto.Stock); // Agregar el valor del parámetro @stock con el valor de producto.Stock

                comando.ExecuteNonQuery(); // Ejecutar el comando SQL para insertar el nuevo producto en la base de datos

                conexion.Close(); // Cerrar la conexión a la base de datos

                return true;  // Retornar true si la inserción fue exitosa
            }
            catch
            {
                conexion.Close(); // Cerrar la conexión a la base de datos en caso de error
                return false;  // Retornar false si ocurrió un error durante la inserción del producto

            }




        }

        public bool EditarProducto(Producto producto)
        {

            MySqlConnection conexion = conexionDB.ObtenerConexion();

            string query = @"UPDATE productos
                            SET codigo = @codigo,
                                nombre = @nombre,   
                                descripcion = @descripcion,
                                precio = @precio,
                                stock = @stock
                            WHERE id = @id"; // Consulta SQL para actualizar un producto existente utilizando parámetros para evitar inyección SQL
            try
            {
                conexion.Open(); // Abrir la conexión a la base de datos
                MySqlCommand comando = new MySqlCommand(query, conexion); // Crear un comando SQL con la consulta y la conexión

                comando.Parameters.AddWithValue("@id", producto.Id);
                comando.Parameters.AddWithValue("@codigo", producto.Codigo); // Agregar los valores de los parámetros al comando utilizando las propiedades del objeto producto
                comando.Parameters.AddWithValue("@nombre", producto.Nombre); // Agregar el valor del parámetro @nombre con el valor de producto.Nombre
                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion); // Agregar el valor del parámetro @descripcion con el valor de producto.Descripcion
                comando.Parameters.AddWithValue("@precio", producto.Precio);  // Agregar el valor del parámetro @precio con el valor de producto.Precio
                comando.Parameters.AddWithValue("@stock", producto.Stock); // Agregar el valor del parámetro @stock con el valor de producto.Stock

                comando.ExecuteNonQuery(); // Ejecutar el comando SQL para actualizar el producto en la base de datos
                conexion.Close(); // Cerrar la conexión a la base de datos

                return true;  // Retornar true si la actualización fue exitosa

            }
            catch
            {
                conexion.Close();
                return false; // Retornar false si ocurrió un error durante la actualización del producto
            }
        }

        public bool EliminarProducto(int id)
        {
            MySqlConnection conexion = conexionDB.ObtenerConexion();
            string query = @"DELETE FROM productos WHERE id = @id"; // Consulta SQL para eliminar un producto utilizando un parámetro para evitar inyección SQL
            try
            {
                conexion.Open(); // Abrir la conexión a la base de datos
                MySqlCommand comando = new MySqlCommand(query, conexion); // Crear un comando SQL con la consulta y la conexión
                comando.Parameters.AddWithValue("@id", id); // Agregar el valor del parámetro @id con el valor del ID del producto a eliminar
                comando.ExecuteNonQuery(); // Ejecutar el comando SQL para eliminar el producto de la base de datos
                conexion.Close(); // Cerrar la conexión a la base de datos
                return true;  // Retornar true si la eliminación fue exitosa
            }
            catch
            {
                conexion.Close();
                return false; // Retornar false si ocurrió un error durante la eliminación del producto
            }
        }
    }
}