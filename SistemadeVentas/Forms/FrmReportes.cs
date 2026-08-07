using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos.Reportes;
using SistemaVentas.Negocio.Reportes;

namespace SistemadeVentas.Presentacion.Forms
{
    /// <summary>
    /// Formulario de reportes: genera reportes de ventas, productos y clientes (con gráfico y
    /// grilla) y permite exportarlos a PDF o Excel.
    /// </summary>
    public partial class FrmReportes : Form
    {
        private readonly ReporteNegocio reporteNegocio = new ReporteNegocio();
        private readonly ReporteExportador reporteExportador = new ReporteExportador();

        private const string TipoVentas = "Ventas";
        private const string TipoProductos = "Productos";
        private const string TipoClientes = "Clientes";

        private List<VentaDTO> ventasActuales = new List<VentaDTO>();
        private List<ProductoVendido> productosMasVendidosActuales = new List<ProductoVendido>();
        private List<ProductoDTO> productosBajoStockActuales = new List<ProductoDTO>();
        private List<ClienteCompra> clientesActuales = new List<ClienteCompra>(); // Lista para almacenar los clientes actuales
        private List<ProductoDTO> productosActuales = new List<ProductoDTO>(); // Lista para almacenar los productos actuales

        /// <summary>
        /// Inicializa el formulario de reportes.
        /// </summary>
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void DgvReporte_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var dgv = sender as DataGridView;
                if (dgv == null)
                    return;

                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgv.Columns.Count)
                    return;

                var colName = dgv.Columns[e.ColumnIndex].Name;
                if ((colName == "Stock" || colName == "CantidadVendida" || colName == "Cantidad") && e.Value != null)
                {
                    if (e.Value is int vi)
                    {
                        e.Value = vi + " unidades";
                        e.FormattingApplied = true;
                    }
                    else if (int.TryParse(Convert.ToString(e.Value), out int v))
                    {
                        e.Value = v + " unidades";
                        e.FormattingApplied = true;
                    }
                }
            }
            catch
            {
                // ignorar
            }
        }

        /// <summary>
        /// Configura el combo de tipos de reporte, establece el rango de fechas por defecto
        /// (último mes) y ajusta el estado habilitado/deshabilitado de los selectores de fecha.
        /// </summary>
        private void FrmReportes_Load(object sender, EventArgs e)
        {
            cmbTipoReporte.Items.AddRange(new object[] { TipoVentas, TipoProductos, TipoClientes });
            cmbTipoReporte.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;

            ActualizarEstadoFechas();
        }

        /// <summary>
        /// Actualiza el estado habilitado de los selectores de fecha cuando cambia el tipo
        /// de reporte seleccionado.
        /// </summary>
        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFechas();
        }

        /// <summary>
        /// Habilita los selectores de fecha únicamente cuando el tipo de reporte seleccionado
        /// es "Ventas", ya que los reportes de productos y clientes no filtran por rango de fechas.
        /// </summary>
        private void ActualizarEstadoFechas()
        {
            bool esVentas = cmbTipoReporte.SelectedItem?.ToString() == TipoVentas;
            dtpDesde.Enabled = esVentas;
            dtpHasta.Enabled = esVentas;
        }

        /// <summary>
        /// Genera el reporte correspondiente al tipo seleccionado en el combo (Ventas,
        /// Productos o Clientes), actualizando la grilla y el gráfico del formulario.
        /// </summary>
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cmbTipoReporte.SelectedItem?.ToString())
                {
                    case TipoVentas:
                        GenerarReporteVentas();
                        break;
                    case TipoProductos:
                        GenerarReporteProductos();
                        break;
                    case TipoClientes:
                        GenerarReporteClientes();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene las ventas dentro del rango de fechas seleccionado y las muestra en la grilla,
        /// además de graficar el total vendido por mes.
        /// </summary>
        private void GenerarReporteVentas()
        {
            ventasActuales = reporteNegocio.ObtenerVentasPorRango(dtpDesde.Value, dtpHasta.Value);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = ventasActuales;
            if (dgvReporte.Columns.Contains("Detalles"))
                dgvReporte.Columns["Detalles"].Visible = false;
            if (dgvReporte.Columns.Contains("Cliente"))
                dgvReporte.Columns["Cliente"].Visible = false;
            if (dgvReporte.Columns.Contains("ClienteId"))
                dgvReporte.Columns["ClienteId"].Visible = false;

            // Formatear columna Total con símbolo ₡
            if (dgvReporte.Columns.Contains("Total"))
            {
                var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                nfi.CurrencySymbol = "₡";
                dgvReporte.Columns["Total"].DefaultCellStyle.Format = "C2";
                dgvReporte.Columns["Total"].DefaultCellStyle.FormatProvider = nfi;
                dgvReporte.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dgvReporte.CellFormatting -= DgvReporte_CellFormatting;
            dgvReporte.CellFormatting += DgvReporte_CellFormatting;

            var ventasPorMes = reporteNegocio.ObtenerVentasPorMes();
            var serie = chartReporte.Series["Series1"];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Column;
            foreach (var punto in ventasPorMes)
            {
                serie.Points.AddXY(punto.Periodo, punto.Total);
            }
            chartReporte.ChartAreas["ChartArea1"].AxisX.Title = "Mes";
            chartReporte.ChartAreas["ChartArea1"].AxisY.Title = "Total vendido";
        }

        /// <summary>
        /// Obtiene el listado completo de productos, los productos más vendidos y los de bajo
        /// stock, mostrando el listado en la grilla y graficando la cantidad vendida de los
        /// productos más vendidos.
        /// </summary>
        private void GenerarReporteProductos()
        {
            productosActuales = reporteNegocio.ObtenerTodosLosProductos();

            productosMasVendidosActuales =
                reporteNegocio.ObtenerProductosMasVendidos(5);

            productosBajoStockActuales =
                reporteNegocio.ObtenerProductosBajoStock(5);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = productosActuales;

            if (dgvReporte.Columns.Contains("Id"))
                dgvReporte.Columns["Id"].Visible = false;

            // Formatear Precio con ₡ y Stock con sufijo unidades
            if (dgvReporte.Columns.Contains("Precio"))
            {
                var nfi = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                nfi.CurrencySymbol = "₡";
                dgvReporte.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvReporte.Columns["Precio"].DefaultCellStyle.FormatProvider = nfi;
                dgvReporte.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvReporte.Columns.Contains("Stock"))
            {
                dgvReporte.CellFormatting -= DgvReporte_CellFormatting;
                dgvReporte.CellFormatting += DgvReporte_CellFormatting;
            }

            var serie = chartReporte.Series["Series1"];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Column;

            foreach (var producto in productosMasVendidosActuales)
            {
                serie.Points.AddXY(
                    producto.Nombre,
                    producto.CantidadVendida);
            }

            chartReporte.ChartAreas["ChartArea1"].AxisX.Title =
                "Producto";

            chartReporte.ChartAreas["ChartArea1"].AxisY.Title =
                "Cantidad vendida";
        }

        /// <summary>
        /// Obtiene los clientes con más compras y los muestra en la grilla, graficando
        /// el total comprado por cada uno.
        /// </summary>
        private void GenerarReporteClientes()
        {
            clientesActuales = reporteNegocio.ObtenerClientesConMasCompras(5);

            dgvReporte.DataSource = null;
            dgvReporte.DataSource = clientesActuales;

            var serie = chartReporte.Series["Series1"];
            serie.Points.Clear();
            serie.ChartType = SeriesChartType.Column;
            foreach (var cliente in clientesActuales)
            {
                serie.Points.AddXY(cliente.Nombre, cliente.TotalComprado);
            }
            chartReporte.ChartAreas["ChartArea1"].AxisX.Title = "Cliente";
            chartReporte.ChartAreas["ChartArea1"].AxisY.Title = "Total comprado";
        }

        /// <summary>
        /// Solicita al usuario una ruta de archivo y exporta a PDF el reporte actualmente
        /// generado (Ventas, Productos o Clientes), según el tipo seleccionado.
        /// </summary>
        private void btnExportarPdf_Click(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog { Filter = "Archivo PDF (*.pdf)|*.pdf", FileName = "Reporte.pdf" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    switch (cmbTipoReporte.SelectedItem?.ToString())
                    {
                        case TipoVentas:
                            reporteExportador.GenerarPdfVentas(ventasActuales, dialogo.FileName);
                            break;
                        case TipoProductos:
                            reporteExportador.GenerarPdfProductos(productosActuales, productosBajoStockActuales, productosMasVendidosActuales, dialogo.FileName);
                            break;
                        case TipoClientes:
                            reporteExportador.GenerarPdfClientes(clientesActuales, dialogo.FileName);
                            break;
                    }

                    MessageBox.Show("Reporte exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Solicita al usuario una ruta de archivo y exporta a Excel el reporte actualmente
        /// generado (Ventas, Productos o Clientes), según el tipo seleccionado.
        /// </summary>
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog { Filter = "Archivo Excel (*.xlsx)|*.xlsx", FileName = "Reporte.xlsx" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    switch (cmbTipoReporte.SelectedItem?.ToString())
                    {
                        case TipoVentas:
                            reporteExportador.GenerarExcelVentas(ventasActuales, dialogo.FileName);
                            break;
                        case TipoProductos:
                            reporteExportador.GenerarExcelProductos(productosActuales, productosBajoStockActuales, productosMasVendidosActuales, dialogo.FileName);
                            break;
                        case TipoClientes:
                            reporteExportador.GenerarExcelClientes(clientesActuales, dialogo.FileName);
                            break;
                    }

                    MessageBox.Show("Reporte exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();// Cierra el formulario actual
        }
    }
}
