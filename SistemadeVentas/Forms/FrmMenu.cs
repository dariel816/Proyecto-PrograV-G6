using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemadeVentas.Presentacion.Forms; // Importación del espacio de nombres que contiene los formularios de la aplicación para poder navegar entre ellos desde el menú principal

namespace SistemadeVentas.Presentacion.Forms
{
    /// <summary>
    /// Formulario de menú principal: punto de navegación hacia los formularios de
    /// Productos, Clientes, Ventas y Reportes.
    /// </summary>
    public partial class FrmMenu : Form
    {
        /// <summary>
        /// Rol con permiso para ver el módulo de Reportes. Cualquier otro rol
        /// (por ejemplo, "Empleado" o "Vendedor") no ve el botón de Reportes.
        /// </summary>
        private const string RolConAccesoAReportes = "Administrador";

        /// <summary>
        /// Inicializa una nueva instancia del formulario de menú principal. Si hay una sesión
        /// activa (<see cref="Utilidades.SesionActual"/>), muestra el usuario y su rol en el
        /// título de la ventana, oculta el módulo de Reportes a quien no sea Administrador,
        /// y aplica un efecto hover a los botones de cada módulo.
        /// </summary>
        public FrmMenu()
        {
            InitializeComponent();

            if (Utilidades.SesionActual.Usuario != null)
            {
                Text = $"Sistema de Ventas - {Utilidades.SesionActual.Usuario.NombreUsuario} ({Utilidades.SesionActual.Usuario.Rol})";
            }

            bool tieneAccesoAReportes = string.Equals(
                Utilidades.SesionActual.Usuario?.Rol,
                RolConAccesoAReportes,
                StringComparison.OrdinalIgnoreCase);

            btnReportes.Visible = tieneAccesoAReportes;

            AplicarHoverModulo(btnProductos);
            AplicarHoverModulo(btnClientes);
            AplicarHoverModulo(btnVentas);

            if (tieneAccesoAReportes)
            {
                AplicarHoverModulo(btnReportes);
            }
        }

        /// <summary>
        /// Resalta la tarjeta del módulo con un fondo azul muy suave al pasar el mouse,
        /// para reforzar que los botones del menú son interactivos.
        /// </summary>
        /// <param name="boton">Botón de módulo al que se le aplica el efecto hover.</param>
        private static void AplicarHoverModulo(FontAwesome.Sharp.IconButton boton)
        {
            Color colorNormal = boton.BackColor;
            Color colorHover = Color.FromArgb(230, 238, 250);
            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorNormal;
        }

        /// <summary>
        /// Abre el formulario de gestión de productos como diálogo modal.
        /// </summary>
        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProductos frmProductos = new FrmProductos(); // Creación de una instancia del formulario de productos
            frmProductos.ShowDialog(); // Mostrar el formulario de productos
        }

        /// <summary>
        /// Abre el formulario de gestión de clientes como diálogo modal.
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmClientes frmClientes = new FrmClientes(); // Creación de una instancia del formulario de clientes
            frmClientes.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario de gestión de ventas como diálogo modal.
        /// </summary>
        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmVentas frmVentas = new FrmVentas(); // Creación de una instancia del formulario de ventas
            frmVentas.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario de reportes como diálogo modal.
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frmReportes = new FrmReportes(); // Creación de una instancia del formulario de reportes
            frmReportes.ShowDialog();
        }
    }
}
