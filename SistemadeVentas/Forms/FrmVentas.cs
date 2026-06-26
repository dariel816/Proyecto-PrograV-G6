using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaVentas.Entidades.Modelos;
using SistemaVentas.Negocio;

namespace SistemadeVentas.Presentacion.Forms
{
    public partial class FrmVentas : Form
    {
        private VentaNegocio ventaNegocio = new VentaNegocio();
        private DetalleVentaNegocio detalleNegocio = new DetalleVentaNegocio();
        private ClienteNegocio clienteNegocio = new ClienteNegocio();
        private ProductoNegocio productoNegocio = new ProductoNegocio();
        private List<DetalleVenta> detallesTemp = new List<DetalleVenta>();
        private int ventaSeleccionada = 0;

        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos();
            CargarVentas();
        }

        private void CargarClientes()
        {
            try
            {
                cmbCliente.DataSource = clienteNegocio.ObtenerClientes();
                cmbCliente.DisplayMember = "Nombre";
                cmbCliente.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                cmbProducto.DataSource = productoNegocio.ObtenerProductos();
                cmbProducto.DisplayMember = "Nombre";
                cmbProducto.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentas()
        {
            try
            {
                var ventas = ventaNegocio.ObtenerVentas();
                dgvVentas.DataSource = ventas;

                if (dgvVentas.Columns.Contains("Detalles"))
                    dgvVentas.Columns["Detalles"].Visible = false;
                if (dgvVentas.Columns.Contains("Cliente"))
                    dgvVentas.Columns["Cliente"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                ventaSeleccionada = Convert.ToInt32(dgvVentas.SelectedRows[0].Cells["Id"].Value);
                CargarDetalles(ventaSeleccionada);
            }
        }

        private void CargarDetalles(int ventaId)
        {
            try
            {
                var venta = ventaNegocio.ObtenerVentaPorId(ventaId);
                if (venta != null)
                {
                    dgvDetalles.DataSource = venta.Detalles;
                    if (dgvDetalles.Columns.Contains("Venta"))
                        dgvDetalles.Columns["Venta"].Visible = false;
                    txtTotal.Text = venta.Total.ToString("C2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var producto = (Producto)cmbProducto.SelectedItem;

                var detalle = new DetalleVenta
                {
                    ProductoId = producto.Id,
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = cantidad * producto.Precio
                };

                detallesTemp.Add(detalle);
                ActualizarGridDetalles();
                ActualizarTotal();
                txtCantidad.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGridDetalles()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = new List<DetalleVenta>(detallesTemp);
            if (dgvDetalles.Columns.Contains("Venta"))
                dgvDetalles.Columns["Venta"].Visible = false;
        }

        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (var detalle in detallesTemp)
            {
                total += detalle.Subtotal;
            }
            txtTotal.Text = total.ToString("C2");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (detallesTemp.Count == 0)
                {
                    MessageBox.Show("Agregue al menos un detalle a la venta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var venta = new Venta
                {
                    ClienteId = (int)cmbCliente.SelectedValue,
                    Detalles = detallesTemp
                };

                int ventaId = ventaNegocio.CrearVenta(venta);

                if (ventaId > 0)
                {
                    MessageBox.Show("Venta guardada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarVentas();
                }
                else
                {
                    MessageBox.Show("Error al guardar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ventaSeleccionada == 0)
                {
                    MessageBox.Show("Seleccione una venta para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult resultado = MessageBox.Show("¿Desea eliminar la venta seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    if (ventaNegocio.EliminarVenta(ventaSeleccionada))
                    {
                        MessageBox.Show("Venta eliminada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarVentas();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            cmbCliente.SelectedIndex = 0;
            cmbProducto.SelectedIndex = 0;
            txtCantidad.Clear();
            txtTotal.Clear();
            detallesTemp.Clear();
            dgvDetalles.DataSource = null;
            ventaSeleccionada = 0;
        }
    }
}
