using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
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

        private int ventaEnEdicionId = 0;
        private DateTime fechaVentaEnEdicion = DateTime.MinValue;

        private readonly Dictionary<int, int> cantidadesOriginalesEdicion =
            new Dictionary<int, int>();

        /// <summary>
        /// Inicializa el formulario de ventas.
        /// </summary>
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void DgvDetalles_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var dgv = sender as DataGridView;
                if (dgv == null)
                    return;

                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.Columns.Count)
                    return;

                var colName = dgv.Columns[e.ColumnIndex].Name;
                if ((colName == "Cantidad") && e.Value != null)
                {
                    if (e.Value is int cantidadInt)
                    {
                        e.Value = cantidadInt + " unidades";
                        e.FormattingApplied = true;
                    }
                    else if (int.TryParse(Convert.ToString(e.Value), out int cantidad))
                    {
                        e.Value = cantidad + " unidades";
                        e.FormattingApplied = true;
                    }
                }
            }
            catch
            {
                // ignorar errores de formateo
            }
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
                var clientes = clienteNegocio.ObtenerClientes() ?? new System.Collections.Generic.List<SistemaVentas.Entidades.DTOs.ClienteDTO>();
                cmbCliente.DataSource = null;
                cmbCliente.DisplayMember = "Nombre";
                cmbCliente.ValueMember = "Id";
                cmbCliente.DataSource = clientes;
                if (clientes.Count > 0)
                    cmbCliente.SelectedIndex = 0;
                else
                    cmbCliente.SelectedIndex = -1;
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
                var productos = productoNegocio.ObtenerProductos() ?? new System.Collections.Generic.List<SistemaVentas.Entidades.DTOs.ProductoDTO>();
                cmbProducto.DataSource = null;
                cmbProducto.DisplayMember = "Nombre";
                cmbProducto.ValueMember = "Id";
                cmbProducto.DataSource = productos;
                if (productos.Count > 0)
                    cmbProducto.SelectedIndex = 0;
                else
                    cmbProducto.SelectedIndex = -1;
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
                // Formatear columna Total con símbolo de colones
                if (dgvVentas.Columns.Contains("Total"))
                {
                    var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                    nfi.CurrencySymbol = "₡";
                    dgvVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                    dgvVentas.Columns["Total"].DefaultCellStyle.FormatProvider = nfi;
                    dgvVentas.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
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

                    // Formatear columnas de detalles: PrecioUnitario y Subtotal con símbolo ₡, Cantidad con sufijo
                    if (dgvDetalles.Columns.Contains("PrecioUnitario"))
                    {
                        var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                        nfi.CurrencySymbol = "₡";
                        dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                        dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.FormatProvider = nfi;
                        dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    if (dgvDetalles.Columns.Contains("Subtotal"))
                    {
                        var nfi2 = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                        nfi2.CurrencySymbol = "₡";
                        dgvDetalles.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
                        dgvDetalles.Columns["Subtotal"].DefaultCellStyle.FormatProvider = nfi2;
                        dgvDetalles.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    // Suscribir evento para formatear Cantidad con 'unidades'
                    dgvDetalles.CellFormatting -= DgvDetalles_CellFormatting;
                    dgvDetalles.CellFormatting += DgvDetalles_CellFormatting;

                    txtTotal.Text = "₡" + venta.Total.ToString("N2");
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
                if (cmbProducto.SelectedItem is not ProductoDTO producto)
                {
                    MessageBox.Show("Seleccione un producto", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //var producto = (ProductoDTO)cmbProducto.SelectedItem;

                DetalleVentaDTO? detalleExistente = detallesTemp.Find(
                    d => d.ProductoId == producto.Id);

                int cantidadResultante =
    ventaEnEdicionId > 0 && detalleExistente != null
        ? cantidad
        : cantidad + (detalleExistente?.Cantidad ?? 0);

                int stockDisponible =
                    ObtenerStockDisponible(producto);

                if (cantidadResultante > stockDisponible)
                {
                    MessageBox.Show(
                        $"Stock insuficiente para {producto.Nombre}. Disponible: {stockDisponible}.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (detalleExistente != null)
                {
                    detalleExistente.Cantidad = cantidadResultante;
                    detalleExistente.Subtotal =
                        detalleExistente.Cantidad * detalleExistente.PrecioUnitario;
                }
                else
                {
                    var detalle = new DetalleVentaDTO
                    {
                        ProductoId = producto.Id,
                        ProductoNombre = producto.Nombre,
                        Cantidad = cantidad,
                        PrecioUnitario = producto.Precio,
                        Subtotal = cantidad * producto.Precio
                    };

                    detallesTemp.Add(detalle);
                }

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
            // Aplicar formateo similar al de detalles guardados: PrecioUnitario y Subtotal con símbolo ₡,
            // y suscribirse al evento para mostrar Cantidad con 'unidades'.
            if (dgvDetalles.Columns.Contains("PrecioUnitario"))
            {
                var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                nfi.CurrencySymbol = "₡";
                dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.FormatProvider = nfi;
                dgvDetalles.Columns["PrecioUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDetalles.Columns.Contains("Subtotal"))
            {
                var nfi2 = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                nfi2.CurrencySymbol = "₡";
                dgvDetalles.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
                dgvDetalles.Columns["Subtotal"].DefaultCellStyle.FormatProvider = nfi2;
                dgvDetalles.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dgvDetalles.CellFormatting -= DgvDetalles_CellFormatting;
            dgvDetalles.CellFormatting += DgvDetalles_CellFormatting;
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


        private int ObtenerStockDisponible(ProductoDTO producto)
        {
            int cantidadOriginal =
                ventaEnEdicionId > 0 &&
                cantidadesOriginalesEdicion.TryGetValue(
                    producto.Id,
                    out int cantidad)
                    ? cantidad
                    : 0;

            return checked(producto.Stock + cantidadOriginal);
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
                if (cmbCliente.SelectedItem is not ClienteDTO clienteSeleccionado)
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
                    Id = ventaEnEdicionId,
                    ClienteId = clienteSeleccionado.Id,
                    Fecha = ventaEnEdicionId > 0
        ? fechaVentaEnEdicion
        : DateTime.Now,
                    Detalles = detallesTemp
                };

                bool esEdicion = ventaEnEdicionId > 0;

                bool ventaGuardada = esEdicion
                    ? ventaNegocio.EditarVenta(venta, detallesTemp)
                    : ventaNegocio.CrearVenta(venta, detallesTemp);


                ventaNegocio.CrearVenta(venta, detallesTemp);

                if (ventaGuardada)
                {
                    MessageBox.Show(esEdicion ? "Venta actualizada exitosamente"
                        : "Venta guardada exitosamente",
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
            if (cmbCliente.Items.Count > 0)
                cmbCliente.SelectedIndex = 0;
            else
                cmbCliente.SelectedIndex = -1;

            if (cmbProducto.Items.Count > 0)
                cmbProducto.SelectedIndex = 0;
            else
                cmbProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtTotal.Clear();
            detallesTemp.Clear();
            dgvDetalles.DataSource = null;
            ventaSeleccionada = 0;

            cantidadesOriginalesEdicion.Clear();

            ventaEnEdicionId = 0;
            fechaVentaEnEdicion = DateTime.MinValue;

            gbNuevaVenta.Text = "Nueva Venta";
            btnGuardar.Text = "Guardar";

            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            dgvVentas.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();// Cierra el formulario actual y vuelve al formulario principal
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarStockDisponible();
        }

        private void MostrarStockDisponible()
        {
            if (cmbProducto.SelectedItem is ProductoDTO producto)
            {
                int stockDisponible =
                    ObtenerStockDisponible(producto);

                lblStockDisponible.Text =
                    $"Stock disponible: {stockDisponible}";
            }
            else
            {
                lblStockDisponible.Text =
                    "Stock disponible: 0";
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ventaSeleccionada <= 0)
                {
                    MessageBox.Show(
                        "Seleccione una venta para editar.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                VentaDTO? venta =
                    ventaNegocio.ObtenerVentaPorId(ventaSeleccionada);

                if (venta == null)
                {
                    MessageBox.Show(
                        "La venta seleccionada ya no existe.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    CargarVentas();
                    return;
                }

                ventaEnEdicionId = venta.Id;
                fechaVentaEnEdicion = venta.Fecha;

                detallesTemp.Clear();
                cantidadesOriginalesEdicion.Clear();

                foreach (DetalleVentaDTO detalle in venta.Detalles)
                {
                    DetalleVentaDTO copia = new DetalleVentaDTO
                    {
                        Id = detalle.Id,
                        VentaId = detalle.VentaId,
                        ProductoId = detalle.ProductoId,
                        ProductoNombre = detalle.ProductoNombre,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        Subtotal = detalle.Subtotal
                    };

                    detallesTemp.Add(copia);

                    cantidadesOriginalesEdicion[detalle.ProductoId] =
                        detalle.Cantidad;
                }

                cmbCliente.SelectedValue = venta.ClienteId;

                gbNuevaVenta.Text = $"Editando Venta #{venta.Id}";
                btnGuardar.Text = "Actualizar";
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                dgvVentas.Enabled = false;

                ActualizarGridDetalles();
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al preparar la edición: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.CurrentRow?.DataBoundItem
    is not DetalleVentaDTO detalle)
            {
                MessageBox.Show(
                    "Seleccione un detalle para quitar.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            detallesTemp.RemoveAll(
                d => d.ProductoId == detalle.ProductoId);

            ActualizarGridDetalles();
            ActualizarTotal();
        }
    }
}