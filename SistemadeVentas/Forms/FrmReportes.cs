using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos.Reportes;
using SistemaVentas.Negocio.Reportes;

namespace SistemadeVentas.Presentacion.Forms
{
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

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            cmbTipoReporte.Items.AddRange(new object[] { TipoVentas, TipoProductos, TipoClientes });
            cmbTipoReporte.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Now.AddMonths(-1);
            dtpHasta.Value = DateTime.Now;

            ActualizarEstadoFechas();
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFechas();
        }

        private void ActualizarEstadoFechas()
        {
            bool esVentas = cmbTipoReporte.SelectedItem?.ToString() == TipoVentas;
            dtpDesde.Enabled = esVentas;
            dtpHasta.Enabled = esVentas;
        }

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
    }
}
