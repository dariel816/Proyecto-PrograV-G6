using System;
using System.Windows.Forms;
using SistemaVentas.Negocio;
using SistemaVentas.Entidades.DTOs;
using SistemadeVentas.Presentacion.Utilidades;

namespace SistemadeVentas.Presentacion.Forms
{
    /// <summary>
    /// Formulario de inicio de sesión, mostrado antes que <see cref="FrmMenu"/>. Solo permite
    /// continuar hacia el menú principal si las credenciales ingresadas son válidas.
    /// </summary>
    public partial class FrmLogin : Form
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        /// <summary>
        /// Usuario que quedó autenticado tras un login exitoso, para que <c>Program.cs</c>
        /// pueda pasarlo a <see cref="FrmMenu"/>. Es <c>null</c> si el login no se completó.
        /// </summary>
        public UsuarioDTO? UsuarioAutenticado { get; private set; }

        /// <summary>
        /// Inicializa el formulario de login y aplica el estilo visual a sus campos.
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            TemaVisual.EstilizarCampos(txtUsuario, txtClave);
            TemaVisual.AplicarEfectoHover(btnIngresar, TemaVisual.Oscurecer(TemaVisual.ColorPrimario));
            txtClave.UseSystemPasswordChar = true;

            // Permite iniciar sesión presionando Enter desde cualquiera de los dos campos.
            txtUsuario.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtClave.Focus(); } };
            txtClave.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnIngresar_Click(s, e); } };

            AcceptButton = btnIngresar;
            txtUsuario.Focus();
        }

        /// <summary>
        /// Valida las credenciales ingresadas y, si son correctas, cierra el formulario con
        /// resultado <see cref="DialogResult.OK"/> dejando el usuario autenticado disponible
        /// en <see cref="UsuarioAutenticado"/>.
        /// </summary>
        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            lblError.Visible = false;

            string nombreUsuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
            {
                MostrarError("Ingrese usuario y contraseña.");
                return;
            }

            try
            {
                btnIngresar.Enabled = false;

                UsuarioDTO? usuario = usuarioNegocio.ValidarLogin(nombreUsuario, clave);

                if (usuario == null)
                {
                    MostrarError("Usuario o contraseña incorrectos.");
                    txtClave.Clear();
                    txtClave.Focus();
                    return;
                }

                UsuarioAutenticado = usuario;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Errores de infraestructura (ej. MySQL apagado) sí se muestran completos,
                // a diferencia de credenciales inválidas, que se ocultan a propósito.
                MessageBox.Show("No fue posible conectar con la base de datos:\n" + ex.Message,
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnIngresar.Enabled = true;
            }
        }

        /// <summary>
        /// Muestra un mensaje de error debajo de los campos de usuario y contraseña.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar.</param>
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        /// <summary>
        /// Cancela el login y cierra la aplicación (el formulario se cierra con
        /// <see cref="DialogResult.Cancel"/>, y <c>Program.cs</c> no abre <see cref="FrmMenu"/>).
        /// </summary>
        private void btnSalir_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
