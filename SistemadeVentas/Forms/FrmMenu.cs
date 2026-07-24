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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProductos frmProductos = new FrmProductos(); // Creación de una instancia del formulario de productos
            frmProductos.ShowDialog(); // Mostrar el formulario de productos
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmClientes frmClientes = new FrmClientes(); // Creación de una instancia del formulario de clientes
            frmClientes.ShowDialog();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FrmVentas frmVentas = new FrmVentas(); // Creación de una instancia del formulario de ventas
            frmVentas.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frmReportes = new FrmReportes(); // Creación de una instancia del formulario de reportes
            frmReportes.ShowDialog();
        }

        private void btnProductosNuevo_Click(object sender, EventArgs e)
        {
            FrmProductos frmProductos = new FrmProductos(); // Creación de una instancia del formulario de productos
            frmProductos.ShowDialog(); // Mostrar el formulario de productos
        }
    }
}
