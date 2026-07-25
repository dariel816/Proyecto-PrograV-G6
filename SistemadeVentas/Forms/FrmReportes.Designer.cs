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
            btnGenerar = new FontAwesome.Sharp.IconButton();
            chartReporte = new System.Windows.Forms.DataVisualization.Charting.Chart();
            dgvReporte = new DataGridView();
            btnExportarPdf = new FontAwesome.Sharp.IconButton();
            btnExportarExcel = new FontAwesome.Sharp.IconButton();
            pnlEncabezado = new Panel();
            lblTituloForm = new Label();
            ((System.ComponentModel.ISupportInitialize)chartReporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).BeginInit();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            //
            // pnlEncabezado
            //
            pnlEncabezado.BackColor = Color.RoyalBlue;
            pnlEncabezado.Controls.Add(lblTituloForm);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(896, 70);
            pnlEncabezado.TabIndex = 11;
            //
            // lblTituloForm
            //
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloForm.ForeColor = Color.White;
            lblTituloForm.Location = new Point(20, 18);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(150, 30);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Reportes";
            //
            // lblTipoReporte
            //
            lblTipoReporte.AutoSize = true;
            lblTipoReporte.Location = new Point(12, 85);
            lblTipoReporte.Name = "lblTipoReporte";
            lblTipoReporte.Size = new Size(94, 15);
            lblTipoReporte.TabIndex = 0;
            lblTipoReporte.Text = "Tipo de Reporte:";
            //
            // cmbTipoReporte
            //
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.Location = new Point(112, 82);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(150, 23);
            cmbTipoReporte.TabIndex = 1;
            cmbTipoReporte.SelectedIndexChanged += cmbTipoReporte_SelectedIndexChanged;
            //
            // lblDesde
            //
            lblDesde.AutoSize = true;
            lblDesde.Location = new Point(280, 85);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(41, 15);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(327, 82);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(110, 23);
            dtpDesde.TabIndex = 3;
            //
            // lblHasta
            //
            lblHasta.AutoSize = true;
            lblHasta.Location = new Point(450, 85);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(38, 15);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(494, 82);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(110, 23);
            dtpHasta.TabIndex = 5;
            //
            // btnGenerar
            //
            btnGenerar.BackColor = Color.RoyalBlue;
            btnGenerar.Cursor = Cursors.Hand;
            btnGenerar.FlatAppearance.BorderSize = 0;
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.ForeColor = Color.White;
            btnGenerar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlassChart;
            btnGenerar.IconColor = Color.White;
            btnGenerar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGenerar.IconSize = 18;
            btnGenerar.Location = new Point(650, 81);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(90, 25);
            btnGenerar.TabIndex = 6;
            btnGenerar.Text = "Generar";
            btnGenerar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            //
            // chartReporte
            //
            chartArea1.Name = "ChartArea1";
            chartReporte.ChartAreas.Add(chartArea1);
            chartReporte.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartReporte.Location = new Point(12, 120);
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
            dgvReporte.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReporte.Location = new Point(12, 350);
            dgvReporte.Name = "dgvReporte";
            dgvReporte.Size = new Size(860, 260);
            dgvReporte.TabIndex = 8;
            //
            // btnExportarPdf
            //
            btnExportarPdf.BackColor = Color.FromArgb(250, 235, 235);
            btnExportarPdf.Cursor = Cursors.Hand;
            btnExportarPdf.FlatAppearance.BorderSize = 0;
            btnExportarPdf.FlatStyle = FlatStyle.Flat;
            btnExportarPdf.ForeColor = Color.Firebrick;
            btnExportarPdf.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            btnExportarPdf.IconColor = Color.Firebrick;
            btnExportarPdf.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportarPdf.IconSize = 18;
            btnExportarPdf.Location = new Point(12, 620);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(120, 30);
            btnExportarPdf.TabIndex = 9;
            btnExportarPdf.Text = "Exportar PDF";
            btnExportarPdf.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportarPdf.UseVisualStyleBackColor = false;
            btnExportarPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExportarPdf.Click += btnExportarPdf_Click;
            //
            // btnExportarExcel
            //
            btnExportarExcel.BackColor = Color.FromArgb(230, 245, 230);
            btnExportarExcel.Cursor = Cursors.Hand;
            btnExportarExcel.FlatAppearance.BorderSize = 0;
            btnExportarExcel.FlatStyle = FlatStyle.Flat;
            btnExportarExcel.ForeColor = Color.ForestGreen;
            btnExportarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnExportarExcel.IconColor = Color.ForestGreen;
            btnExportarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportarExcel.IconSize = 18;
            btnExportarExcel.Location = new Point(140, 620);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(120, 30);
            btnExportarExcel.TabIndex = 10;
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportarExcel.UseVisualStyleBackColor = false;
            btnExportarExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExportarExcel.Click += btnExportarExcel_Click;
            //
            // FrmReportes
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(896, 670);
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
            Controls.Add(pnlEncabezado);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimumSize = new Size(750, 500);
            Name = "FrmReportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes";
            Load += FrmReportes_Load;
            ((System.ComponentModel.ISupportInitialize)chartReporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReporte).EndInit();
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
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
        private FontAwesome.Sharp.IconButton btnGenerar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReporte;
        private DataGridView dgvReporte;
        private FontAwesome.Sharp.IconButton btnExportarPdf;
        private FontAwesome.Sharp.IconButton btnExportarExcel;
        private Panel pnlEncabezado;
        private Label lblTituloForm;
    }
}
