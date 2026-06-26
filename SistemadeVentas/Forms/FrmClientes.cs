using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos.DAO;//Importación del espacio de nombres que contiene la clase ClienteDAO para acceder a los métodos de datos relacionados con clientes
using SistemaVentas.Entidades.Modelos;//Importación del espacio de nombres que contiene la clase Cliente para trabajar con los objetos de tipo Cliente

namespace SistemadeVentas.Presentacion.Forms
{
    public partial class FrmClientes : Form


    {

        ClienteDAO clienteDAO = new ClienteDAO(); // Creación de una instancia de ClienteDAO para acceder a los métodos de datos relacionados con clientes

        public FrmClientes()
        {


            InitializeComponent();
            CargarClientes(); // Llamada al método para cargar los clientes en el DataGridView al iniciar el formulario
        }

        private void CargarClientes()// Método para cargar los clientes en el DataGridView
        {
            dgvClientes.DataSource = null; // Limpiar la fuente de datos del DataGridView antes de cargar los nuevos datos
            dgvClientes.DataSource = clienteDAO.ObtenerClientes(); // Establecer la fuente de datos del DataGridView con la lista de clientes obtenida del método ObtenerClientes() de ClienteDAO
        }
        private void limpiarCampos() // Método para limpiar los campos de entrada después de agregar o actualizar un cliente
        {
            txtId.Clear(); // Limpiar el campo de ID (aunque generalmente no se debería mostrar ni editar el ID directamente)
            txtNombre.Clear(); // Limpiar el campo de nombre
            txtCorreo.Clear(); // Limpiar el campo de correo
            txtTelefono.Clear(); // Limpiar el campo de teléfono
            txtNombre.Focus(); // Establecer el foco en el campo de nombre para facilitar la entrada de datos
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();

            cliente.Nombre = txtNombre.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Correo = txtCorreo.Text;

            bool resultado = clienteDAO.InsertarCliente(cliente);

            if (resultado)
            {
                MessageBox.Show("Cliente guardado correctamente");
                CargarClientes();
                limpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el cliente");
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();

            cliente.Id = Convert.ToInt32(txtId.Text);
            cliente.Nombre = txtNombre.Text;
            cliente.Telefono = txtTelefono.Text;
            cliente.Correo = txtCorreo.Text;

            bool resultado = clienteDAO.EditarCliente(cliente);

            if (resultado)
            {
                MessageBox.Show("Cliente actualizado correctamente");
                CargarClientes();
                limpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el cliente");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Desea eliminar este cliente?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                int id = Convert.ToInt32(txtId.Text);

                bool resultado = clienteDAO.EliminarCliente(id);

                if (resultado)
                {
                    MessageBox.Show("Cliente eliminado correctamente");
                    CargarClientes();
                    limpiarCampos();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }
    }
}
