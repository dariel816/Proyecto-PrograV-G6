using SistemadeVentas.Presentacion.Forms;

namespace SistemadeVentas.Presentacion
{
    /// <summary>
    /// Clase de arranque de la aplicación de escritorio "Sistema de Ventas".
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación: inicializa la configuración de la
        /// aplicación (DPI alto, fuente predeterminada) y ejecuta el formulario <see cref="FrmMenu"/>.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FrmMenu());//Incia el formulario FrmMenu al iniciar la aplicación
        }
    }
}