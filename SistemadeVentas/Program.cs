using SistemadeVentas.Presentacion.Forms;
using SistemadeVentas.Presentacion.Utilidades;

namespace SistemadeVentas.Presentacion
{
    /// <summary>
    /// Clase de arranque de la aplicación de escritorio "Sistema de Ventas".
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación: inicializa la configuración de la
        /// aplicación (DPI alto, fuente predeterminada), exige iniciar sesión mediante
        /// <see cref="FrmLogin"/> y, si el login es exitoso, ejecuta el formulario <see cref="FrmMenu"/>.
        /// </summary>
        [STAThread]
      
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            while (true)
            {
                // Mostrar el formulario de inicio de sesión.
                using (FrmLogin frmLogin = new FrmLogin())
                {
                    if (frmLogin.ShowDialog() != DialogResult.OK ||
                        frmLogin.UsuarioAutenticado == null)
                    {
                        // Si se cancela o se cierra el login, termina la aplicación.
                        return;
                    }

                    SesionActual.Usuario = frmLogin.UsuarioAutenticado;
                }

                // Mostrar el menú principal.
                using (FrmMenu frmMenu = new FrmMenu())
                {
                    DialogResult resultadoMenu = frmMenu.ShowDialog();

                    // Retry significa que se presionó "Cerrar sesión".
                    if (resultadoMenu != DialogResult.Retry)
                    {
                        return;
                    }
                }

                // Limpiar la sesión antes de volver al login.
                SesionActual.Usuario = null;
            }
        }
    }
}