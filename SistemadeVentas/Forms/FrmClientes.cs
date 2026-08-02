using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Negocio;
using SistemaVentas.Entidades.DTOs;

namespace SistemadeVentas.Presentacion.Forms
{
    /// <summary>
    /// Formulario de gestión de clientes: alta, edición, eliminación y listado.
    /// </summary>
    public partial class FrmClientes : Form
    {
        private ClienteNegocio clienteNegocio = new ClienteNegocio();

        /// <summary>
        /// Inicializa el formulario de clientes y carga el listado inicial de clientes.
        /// </summary>
        public FrmClientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        /// <summary>
        /// Carga (o recarga) el listado de clientes en el <c>DataGridView</c> del formulario.
        /// </summary>
        private void CargarClientes()
        {
            try
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clienteNegocio.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario (Id, Nombre, Correo, Teléfono) y regresa el foco
        /// al campo Nombre.
        /// </summary>
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtNombre.Focus();
        }

        /// <summary>
        /// Valida los campos y guarda un nuevo cliente, verificando previamente que el correo
        /// y el teléfono no estén ya registrados por otro cliente.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos estén completos y con formato correcto
                if (!ValidarCampos())
                    return;

                ClienteDTO cliente = new ClienteDTO
                {
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                // Verificar unicidad de correo y teléfono
                if (clienteNegocio.ObtenerClientes().Exists(c => string.Equals(c.Correo?.Trim(), cliente.Correo, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El correo electrónico ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                if (clienteNegocio.ObtenerClientes().Exists(c => string.Equals(c.Telefono?.Trim(), cliente.Telefono, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El teléfono ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                bool resultado = clienteNegocio.InsertarCliente(cliente);

                if (resultado)
                {
                    MessageBox.Show("Cliente guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
                }
                else
                {
                    MessageBox.Show("Error al guardar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida los campos y actualiza el cliente seleccionado, verificando previamente que
        /// el correo y el teléfono no estén registrados por otro cliente.
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos antes de editar
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Seleccione un cliente para actualizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                if (!int.TryParse(txtId.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID de cliente inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ClienteDTO cliente = new ClienteDTO
                {
                    Id = id,
                    Nombre = txtNombre.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Correo = txtCorreo.Text.Trim()
                };

                // Verificar unicidad de correo y teléfono excluyendo el propio registro
                if (clienteNegocio.ObtenerClientes().Exists(c => c.Id != id && string.Equals(c.Correo?.Trim(), cliente.Correo, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El correo electrónico ya está registrado por otro cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                if (clienteNegocio.ObtenerClientes().Exists(c => c.Id != id && string.Equals(c.Telefono?.Trim(), cliente.Telefono, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El teléfono ya está registrado por otro cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefono.Focus();
                    return;
                }

                bool resultado = clienteNegocio.EditarCliente(cliente);

                if (resultado)
                {
                    MessageBox.Show("Cliente actualizado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación y elimina el cliente seleccionado.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult resultado = MessageBox.Show("¿Desea eliminar el cliente seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool eliminado = clienteNegocio.EliminarCliente(Convert.ToInt32(txtId.Text));

                    if (eliminado)
                    {
                        MessageBox.Show("Cliente eliminado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarClientes();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia los campos del formulario al presionar el botón Limpiar.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Carga los datos del cliente seleccionado en el <c>DataGridView</c> hacia los
        /// campos de texto del formulario.
        /// </summary>
        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvClientes.SelectedRows[0];
                txtId.Text = row.Cells["Id"].Value?.ToString() ?? "";
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
                txtCorreo.Text = row.Cells["Correo"].Value?.ToString() ?? "";
                txtTelefono.Text = row.Cells["Telefono"].Value?.ToString() ?? "";
            }
        }

        /// <summary>
        /// Valida que todos los campos del formulario de clientes estén completos y sean válidos
        /// (nombre requerido, teléfono con al menos 7 dígitos, correo con formato válido).
        /// </summary>
        /// <returns><c>true</c> si todos los campos son válidos; de lo contrario <c>false</c>.</returns>
        // Valida que todos los campos del formulario de clientes estén completos y sean válidos
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El campo Teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            // Validar formato de teléfono: al menos 7 dígitos y caracteres permitidos
            string digitsOnly = new string(txtTelefono.Text.Where(char.IsDigit).ToArray());
            if (digitsOnly.Length < 7)
            {
                MessageBox.Show("Ingrese un teléfono válido (al menos 7 dígitos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El campo Correo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            // Validar formato de correo básico
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtCorreo.Text.Trim());
                if (addr.Address != txtCorreo.Text.Trim())
                {
                    MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            return true;
        }
    }
}
