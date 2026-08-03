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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Antes de mostrar el menú principal, se exige iniciar sesión.
            // Si el usuario cierra el login o cancela, la aplicación termina sin abrir el menú.
            using (FrmLogin frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() != DialogResult.OK || frmLogin.UsuarioAutenticado == null)
                {
                    return;
                }

                SesionActual.Usuario = frmLogin.UsuarioAutenticado;
            }

            Application.Run(new FrmMenu());//Incia el formulario FrmMenu al iniciar la aplicación
        }
    }
}