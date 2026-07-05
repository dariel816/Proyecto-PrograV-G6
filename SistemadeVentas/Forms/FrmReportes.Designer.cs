namespace SistemadeVentas.Presentacion.Forms
{
    partial class FrmReportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblTipoReporte = new Label();
            cmbTipoReporte = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnGenerar = new Button();
            chartReporte = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dgvReporte = new DataGridView();
            btnExportarPdf = new Button();
            btnExportarExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)chartReporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            SuspendLayout();
            //
            // lblTipoReporte
            //
            lblTipoReporte.AutoSize = true;
            lblTipoReporte.Location = new Point(12, 15);
            lblTipoReporte.Name = "lblTipoReporte";
            lblTipoReporte.Size = new Size(94, 15);
            lblTipoReporte.TabIndex = 0;
            lblTipoReporte.Text = "Tipo de Reporte:";
            //
            // cmbTipoReporte
            //
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.Location = new Point(112, 12);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(150, 23);
            cmbTipoReporte.TabIndex = 1;
            cmbTipoReporte.SelectedIndexChanged += cmbTipoReporte_SelectedIndexChanged;
            //
            // lblDesde
            //
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(280, 15);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(41, 15);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(327, 12);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 23);
            dtpDesde.TabIndex = 3;
            //
            // lblHasta
            //
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(450, 15);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(38, 15);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(494, 12);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 23);
            dtpHasta.TabIndex = 5;
            //
            // btnGenerar
            //
            btnGenerar.Location = new Point(650, 11);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(90, 25);
            btnGenerar.TabIndex = 6;
            btnGenerar.Text = "Generar";
            btnGenerar.UseVisualStyleBackColor = true;
            btnGenerar.Click += btnGenerar_Click;
            //
            // chartReporte
            //
            chartArea1.Name = "ChartArea1";
            chartReporte.ChartAreas.Add(chartArea1);
            chartReporte.Location = new Point(12, 50);
            chartReporte.Name = "chartReporte";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series1.Name = "Series1";
            chartReporte.Series.Add(series1);
            chartReporte.Size = new Size(860, 220);
            chartReporte.TabIndex = 7;
            chartReporte.Text = "chartReporte";
            //
            // dgvReporte
            //
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Location = new Point(12, 280);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.Size = new Size(860, 260);
            dgvReporte.TabIndex = 8;
            //
            // btnExportarPdf
            //
            btnExportarPdf.Location = new Point(12, 550);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(120, 30);
            btnExportarPdf.TabIndex = 9;
            btnExportarPdf.Text = "Exportar PDF";
            btnExportarPdf.UseVisualStyleBackColor = true;
            btnExportarPdf.Click += btnExportarPdf_Click;
            //
            // btnExportarExcel
            //
            btnExportarExcel.Location = new Point(140, 550);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(120, 30);
            btnExportarExcel.TabIndex = 10;
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.UseVisualStyleBackColor = true;
            btnExportarExcel.Click += btnExportarExcel_Click;
            //
            // FrmReportes
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(896, 600);
            Controls.Add(btnExportarExcel);
            Controls.Add(btnExportarPdf);
            Controls.Add(dgvReporte);
            Controls.Add(chartReporte);
            Controls.Add(btnGenerar);
            Controls.Add(dtpHasta);
            Controls.Add(lblHasta);
            Controls.Add(dtpDesde);
            Controls.Add(lblDesde);
            Controls.Add(cmbTipoReporte);
            Controls.Add(lblTipoReporte);
            Name = "FrmReportes";
            Text = "Reportes";
            Load += FrmReportes_Load;
            ((System.ComponentModel.ISupportInitialize)chartReporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTipoReporte;
        private ComboBox cmbTipoReporte;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnGenerar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReporte;
        private DataGridView dgvReporte;
        private Button btnExportarPdf;
        private Button btnExportarExcel;
    }
}
