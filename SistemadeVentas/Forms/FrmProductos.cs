using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos.DAO;//Importación del espacio de nombres que contiene la clase ProductoDAO para acceder a los métodos de datos relacionados con productos
using SistemaVentas.Entidades.Modelos;//Importación del espacio de nombres que contiene la clase Producto para trabajar con los objetos de tipo Producto



namespace SistemadeVentas.Presentacion.Forms


{
    public partial class FrmProductos : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();//Instancia de ProductoDAO para acceder a los métodos de datos relacionados con productos

        public FrmProductos()
        {
            InitializeComponent();
            CargarProductos();//Llamada al método para cargar los productos en el DataGridView al iniciar el formulario
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = productoDAO.ObtenerProductos();
        }

        private void limpiarCampos() //Método para limpiar los campos de entrada después de agregar o actualizar un producto
        {
            txtID.Clear(); // Limpiar el campo de ID (aunque generalmente no se debería mostrar ni editar el ID directamente)
            txtCodigo.Clear(); // Limpiar el campo de código
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCodigo.Focus();  // Establecer el foco en el campo de código para facilitar la entrada de datos
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();

                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = decimal.Parse(txtPrecio.Text);
                producto.Stock = int.Parse(txtStock.Text);

                bool resultado = productoDAO.InsertarProducto(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto agregado exitosamente.");
                    CargarProductos(); // Recargar los productos para mostrar el nuevo producto agregado
                    limpiarCampos(); // Limpiar los campos de entrada después de agregar el producto
                }
                else
                {
                    MessageBox.Show("Error al agregar el producto.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) //Evento para manejar el clic en una celda del DataGridView, se utiliza para cargar los datos del producto seleccionado en los campos de entrada para su edición
        {

        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e) //Evento para manejar el clic en una celda del DataGridView, se utiliza para cargar los datos del producto seleccionado en los campos de entrada para su edición
        {
            if (e.RowIndex >= 0) // Verificar que se haya seleccionado una fila válida
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];// Obtener la fila seleccionada
                txtCodigo.Text = fila.Cells["Codigo"].Value.ToString(); // Cargar el valor del código del producto en el campo de texto correspondiente
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString(); // Cargar el valor del nombre del producto en el campo de texto correspondiente
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString(); // Cargar el valor de la descripción del producto en el campo de texto correspondiente
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString(); // Cargar el valor del precio del producto en el campo de texto correspondiente
                txtStock.Text = fila.Cells["Stock"].Value.ToString(); // Cargar el valor del stock del producto en el campo de texto correspondiente

                txtID.Text = fila.Cells["Id"].Value.ToString(); // Cargar el valor del ID del producto en el campo de texto correspondiente para su uso en la actualización o eliminación del producto


            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try

            {

                Producto producto = new Producto();

                producto.Id = Convert.ToInt32(txtID.Text); // Convertir el valor del ID del producto a entero para su uso en la actualización
                producto.Codigo = txtCodigo.Text;
                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = Convert.ToDecimal(txtPrecio.Text);
                producto.Stock = Convert.ToInt32(txtStock.Text);

                bool resultado = productoDAO.EditarProducto(producto); // Llamar al método para actualizar el producto en la base de datos

                if (resultado)
                {
                    MessageBox.Show("Producto actualizado exitosamente.");
                    CargarProductos(); // Recargar los productos para mostrar el producto actualizado   
                    limpiarCampos(); // Limpiar los campos de entrada después de actualizar el producto


                }
                else
                {
                    MessageBox.Show("Error al actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Seleccione un producto para eliminar.");
                    return;
                }

                DialogResult resultadoPregunta = MessageBox.Show(

                    "Esta Seguro que desea eliminar el producto seleccionado",
                    "Desea Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultadoPregunta == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(txtID.Text); // Convertir el valor del ID del producto a entero para su uso en la eliminación
                    bool resultado = productoDAO.EliminarProducto(id); // Llamar al método para eliminar el producto de la base de datos
                    if (resultado)
                    {
                        MessageBox.Show("Producto eliminado exitosamente.");
                        CargarProductos(); // Recargar los productos para mostrar el producto eliminado
                        limpiarCampos(); // Limpiar los campos de entrada después de eliminar el producto
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }




        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos(); // Llamada al método para limpiar los campos de entrada cuando se hace clic en el botón "Limpiar"
        }
    }
}









