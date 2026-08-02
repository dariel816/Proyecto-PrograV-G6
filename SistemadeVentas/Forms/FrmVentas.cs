using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Negocio;

namespace SistemadeVentas.Presentacion.Forms
{
    /// <summary>
    /// Formulario de gestión de ventas: registro de una venta con sus detalles (líneas de
    /// producto), listado, y eliminación de ventas existentes.
    /// </summary>
    public partial class FrmVentas : Form
    {
        private VentaNegocio ventaNegocio = new VentaNegocio();
        private DetalleVentaNegocio detalleNegocio = new DetalleVentaNegocio();
        private ClienteNegocio clienteNegocio = new ClienteNegocio();
        private ProductoNegocio productoNegocio = new ProductoNegocio();
        private List<DetalleVentaDTO> detallesTemp = new List<DetalleVentaDTO>();
        private int ventaSeleccionada = 0;

        /// <summary>
        /// Inicializa el formulario de ventas.
        /// </summary>
        public FrmVentas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carga los combos de clientes y productos y el listado de ventas al mostrarse el formulario.
        /// </summary>
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarProductos();
            CargarVentas();
        }

        /// <summary>
        /// Carga la lista de clientes en el combo de selección de clientes.
        /// </summary>
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

        /// <summary>
        /// Carga la lista de productos en el combo de selección de productos.
        /// </summary>
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

        /// <summary>
        /// Carga (o recarga) el listado de ventas en el <c>DataGridView</c> principal, ocultando
        /// las columnas internas "Detalles" y "ClienteId".
        /// </summary>
        private void CargarVentas()
        {
            try
            {
                var ventas = ventaNegocio.ObtenerVentas();
                dgvVentas.DataSource = ventas;

                if (dgvVentas.Columns.Contains("Detalles"))
                    dgvVentas.Columns["Detalles"].Visible = false;
                if (dgvVentas.Columns.Contains("ClienteId"))
                    dgvVentas.Columns["ClienteId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Al seleccionar una venta en el listado, guarda su Id como venta seleccionada
        /// y carga sus detalles.
        /// </summary>
        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count > 0)
            {
                ventaSeleccionada = Convert.ToInt32(dgvVentas.SelectedRows[0].Cells["Id"].Value);
                CargarDetalles(ventaSeleccionada);
            }
        }

        /// <summary>
        /// Carga los detalles de la venta indicada en el <c>DataGridView</c> de detalles
        /// y actualiza el campo de total.
        /// </summary>
        /// <param name="ventaId">Identificador de la venta cuyos detalles se van a mostrar.</param>
        private void CargarDetalles(int ventaId)
        {
            try
            {
                var venta = ventaNegocio.ObtenerVentaPorId(ventaId);
                if (venta != null)
                {
                    dgvDetalles.DataSource = venta.Detalles;
                    if (dgvDetalles.Columns.Contains("VentaId"))
                        dgvDetalles.Columns["VentaId"].Visible = false;
                    txtTotal.Text = venta.Total.ToString("C2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida el producto y la cantidad seleccionados y agrega un nuevo detalle temporal
        /// a la venta en construcción, actualizando la grilla de detalles y el total.
        /// </summary>
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

                var producto = (ProductoDTO)cmbProducto.SelectedItem;

                var detalle = new DetalleVentaDTO
                {
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre,
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

        /// <summary>
        /// Refresca el <c>DataGridView</c> de detalles con la lista temporal de detalles
        /// de la venta en construcción, ocultando la columna interna "VentaId".
        /// </summary>
        private void ActualizarGridDetalles()
        {
            dgvDetalles.DataSource = null;
            dgvDetalles.DataSource = new List<DetalleVentaDTO>(detallesTemp);
            if (dgvDetalles.Columns.Contains("VentaId"))
                dgvDetalles.Columns["VentaId"].Visible = false;
        }

        /// <summary>
        /// Recalcula y muestra en pantalla el total de la venta en construcción, sumando
        /// los subtotales de todos los detalles temporales.
        /// </summary>
        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (var detalle in detallesTemp)
            {
                total += detalle.Subtotal;
            }
            txtTotal.Text = total.ToString("C2");
        }

        /// <summary>
        /// Valida que haya un cliente seleccionado y al menos un detalle agregado, y guarda la
        /// venta junto con sus detalles mediante <see cref="VentaNegocio.CrearVenta"/> (operación
        /// transaccional que también descuenta el stock de los productos vendidos).
        /// </summary>
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

                var venta = new VentaDTO
                {
                    ClienteId = (int)cmbCliente.SelectedValue,
                    Fecha = DateTime.Now,
                    Detalles = detallesTemp
                };

                bool ventaGuardada =
     ventaNegocio.CrearVenta(venta, detallesTemp);

                if (ventaGuardada)
                {
                    MessageBox.Show(
                        "Venta guardada exitosamente",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LimpiarFormulario();
                    CargarProductos();
                    CargarVentas();
                }
                else
                {
                    MessageBox.Show(
                        "Error al guardar la venta",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación y elimina la venta actualmente seleccionada.
        /// </summary>
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

        /// <summary>
        /// Limpia el formulario para iniciar el registro de una nueva venta.
        /// </summary>
        private void btnNueva_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        /// <summary>
        /// Restablece los controles del formulario (cliente, producto, cantidad, total y
        /// detalles temporales) a su estado inicial.
        /// </summary>
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
