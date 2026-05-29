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


    }


}






