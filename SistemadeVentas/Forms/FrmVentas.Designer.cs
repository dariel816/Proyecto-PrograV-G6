namespace SistemadeVentas.Presentacion.Forms
{
    partial class FrmVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gbVentas = new GroupBox();
            dgvVentas = new DataGridView();
            gbDetalles = new GroupBox();
            dgvDetalles = new DataGridView();
            gbNuevaVenta = new GroupBox();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            btnAgregar = new Button();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnNueva = new Button();
            lblTotal = new Label();
            txtTotal = new TextBox();
            gbVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            gbDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            gbNuevaVenta.SuspendLayout();
            SuspendLayout();
            // 
            // gbVentas
            // 
            gbVentas.Controls.Add(dgvVentas);
            gbVentas.Location = new Point(22, 26);
            gbVentas.Margin = new Padding(6, 7, 6, 7);
            gbVentas.Name = "gbVentas";
            gbVentas.Padding = new Padding(6, 7, 6, 7);
            gbVentas.Size = new Size(1422, 431);
            gbVentas.TabIndex = 0;
            gbVentas.TabStop = false;
            gbVentas.Text = "Ventas";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Dock = DockStyle.Fill;
            dgvVentas.Location = new Point(6, 34);
            dgvVentas.Margin = new Padding(6, 7, 6, 7);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersWidth = 62;
            dgvVentas.Size = new Size(1410, 390);
            dgvVentas.TabIndex = 0;
            dgvVentas.SelectionChanged += dgvVentas_SelectionChanged;
            // 
            // gbDetalles
            // 
            gbDetalles.Controls.Add(dgvDetalles);
            gbDetalles.Location = new Point(22, 469);
            gbDetalles.Margin = new Padding(6, 7, 6, 7);
            gbDetalles.Name = "gbDetalles";
            gbDetalles.Padding = new Padding(6, 7, 6, 7);
            gbDetalles.Size = new Size(1422, 323);
            gbDetalles.TabIndex = 1;
            gbDetalles.TabStop = false;
            gbDetalles.Text = "Detalles de la Venta";
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalles.Dock = DockStyle.Fill;
            dgvDetalles.Location = new Point(6, 34);
            dgvDetalles.Margin = new Padding(6, 7, 6, 7);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersWidth = 62;
            dgvDetalles.Size = new Size(1410, 282);
            dgvDetalles.TabIndex = 0;
            // 
            // gbNuevaVenta
            // 
            gbNuevaVenta.Controls.Add(lblCantidad);
            gbNuevaVenta.Controls.Add(txtCantidad);
            gbNuevaVenta.Controls.Add(btnAgregar);
            gbNuevaVenta.Controls.Add(lblProducto);
            gbNuevaVenta.Controls.Add(cmbProducto);
            gbNuevaVenta.Controls.Add(lblCliente);
            gbNuevaVenta.Controls.Add(cmbCliente);
            gbNuevaVenta.Location = new Point(22, 805);
            gbNuevaVenta.Margin = new Padding(6, 7, 6, 7);
            gbNuevaVenta.Name = "gbNuevaVenta";
            gbNuevaVenta.Padding = new Padding(6, 7, 6, 7);
            gbNuevaVenta.Size = new Size(1422, 172);
            gbNuevaVenta.TabIndex = 2;
            gbNuevaVenta.TabStop = false;
            gbNuevaVenta.Text = "Nueva Venta";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(966, 47);
            lblCantidad.Margin = new Padding(6, 0, 6, 0);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(91, 28);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(1067, 41);
            txtCantidad.Margin = new Padding(6, 7, 6, 7);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(116, 34);
            txtCantidad.TabIndex = 5;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(1197, 41);
            btnAgregar.Margin = new Padding(6, 7, 6, 7);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(196, 49);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar Detalle";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(476, 47);
            lblProducto.Margin = new Padding(6, 0, 6, 0);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(93, 28);
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Producto";
            // 
            // cmbProducto
            // 
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(580, 41);
            cmbProducto.Margin = new Padding(6, 7, 6, 7);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(364, 36);
            cmbProducto.TabIndex = 3;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(11, 47);
            lblCliente.Margin = new Padding(6, 0, 6, 0);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(72, 28);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente";
            // 
            // cmbCliente
            // 
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(94, 41);
            cmbCliente.Margin = new Padding(6, 7, 6, 7);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(364, 36);
            cmbCliente.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1194, 991);
            btnGuardar.Margin = new Padding(6, 7, 6, 7);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(138, 49);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(894, 991);
            btnEliminar.Margin = new Padding(6, 7, 6, 7);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(138, 49);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnNueva
            // 
            btnNueva.Location = new Point(1043, 991);
            btnNueva.Margin = new Padding(6, 7, 6, 7);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(138, 49);
            btnNueva.TabIndex = 9;
            btnNueva.Text = "Nueva";
            btnNueva.UseVisualStyleBackColor = true;
            btnNueva.Click += btnNueva_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(28, 991);
            lblTotal.Margin = new Padding(6, 0, 6, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(58, 28);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "Total:";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(101, 991);
            txtTotal.Margin = new Padding(6, 7, 6, 7);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(180, 34);
            txtTotal.TabIndex = 11;
            // 
            // FrmVentas
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(966, 553);
            Controls.Add(txtTotal);
            Controls.Add(lblTotal);
            Controls.Add(btnNueva);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(gbNuevaVenta);
            Controls.Add(gbDetalles);
            Controls.Add(gbVentas);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6, 7, 6, 7);
            MaximizeBox = false;
            Name = "FrmVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Ventas";
            Load += FrmVentas_Load;
            gbVentas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            gbDetalles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            gbNuevaVenta.ResumeLayout(false);
            gbNuevaVenta.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.GroupBox gbVentas;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.GroupBox gbDetalles;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.GroupBox gbNuevaVenta;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
    }
}
