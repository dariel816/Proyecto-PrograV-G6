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
            lblStockDisponible = new Label();
            lblCantidad = new Label();
            txtCantidad = new TextBox();
            btnAgregar = new FontAwesome.Sharp.IconButton();
            lblProducto = new Label();
            cmbProducto = new ComboBox();
            lblCliente = new Label();
            cmbCliente = new ComboBox();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            btnNueva = new FontAwesome.Sharp.IconButton();
            lblTotal = new Label();
            txtTotal = new TextBox();
            pnlEncabezado = new Panel();
            lblTituloForm = new Label();
            btnVolver = new Button();
            gbVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            gbDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            gbNuevaVenta.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // gbVentas
            // 
            gbVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbVentas.Controls.Add(dgvVentas);
            gbVentas.Location = new Point(22, 90);
            gbVentas.Margin = new Padding(6, 7, 6, 7);
            gbVentas.Name = "gbVentas";
            gbVentas.Padding = new Padding(6, 7, 6, 7);
            gbVentas.Size = new Size(1459, 260);
            gbVentas.TabIndex = 0;
            gbVentas.TabStop = false;
            gbVentas.Text = "Ventas";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Dock = DockStyle.Fill;
            dgvVentas.GridColor = Color.LightGray;
            dgvVentas.Location = new Point(6, 34);
            dgvVentas.Margin = new Padding(6, 7, 6, 7);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersWidth = 62;
            dgvVentas.Size = new Size(1447, 219);
            dgvVentas.TabIndex = 0;
            dgvVentas.SelectionChanged += dgvVentas_SelectionChanged;
            // 
            // gbDetalles
            // 
            gbDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDetalles.Controls.Add(dgvDetalles);
            gbDetalles.Location = new Point(22, 365);
            gbDetalles.Margin = new Padding(6, 7, 6, 7);
            gbDetalles.Name = "gbDetalles";
            gbDetalles.Padding = new Padding(6, 7, 6, 7);
            gbDetalles.Size = new Size(1459, 190);
            gbDetalles.TabIndex = 1;
            gbDetalles.TabStop = false;
            gbDetalles.Text = "Detalles de la Venta";
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalles.BackgroundColor = Color.White;
            dgvDetalles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalles.Dock = DockStyle.Fill;
            dgvDetalles.GridColor = Color.LightGray;
            dgvDetalles.Location = new Point(6, 34);
            dgvDetalles.Margin = new Padding(6, 7, 6, 7);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersWidth = 62;
            dgvDetalles.Size = new Size(1447, 149);
            dgvDetalles.TabIndex = 0;
            // 
            // gbNuevaVenta
            // 
            gbNuevaVenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbNuevaVenta.Controls.Add(lblStockDisponible);
            gbNuevaVenta.Controls.Add(lblCantidad);
            gbNuevaVenta.Controls.Add(txtCantidad);
            gbNuevaVenta.Controls.Add(btnAgregar);
            gbNuevaVenta.Controls.Add(lblProducto);
            gbNuevaVenta.Controls.Add(cmbProducto);
            gbNuevaVenta.Controls.Add(lblCliente);
            gbNuevaVenta.Controls.Add(cmbCliente);
            gbNuevaVenta.Location = new Point(15, 587);
            gbNuevaVenta.Margin = new Padding(6, 7, 6, 7);
            gbNuevaVenta.Name = "gbNuevaVenta";
            gbNuevaVenta.Padding = new Padding(6, 7, 6, 7);
            gbNuevaVenta.Size = new Size(1461, 141);
            gbNuevaVenta.TabIndex = 2;
            gbNuevaVenta.TabStop = false;
            gbNuevaVenta.Text = "Nueva Venta";
            // 
            // lblStockDisponible
            // 
            lblStockDisponible.AutoSize = true;
            lblStockDisponible.Location = new Point(696, 34);
            lblStockDisponible.Name = "lblStockDisponible";
            lblStockDisponible.Size = new Size(261, 28);
            lblStockDisponible.TabIndex = 13;
            lblStockDisponible.Text = "Stock disponible: 0 unidades";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(979, 83);
            lblCantidad.Margin = new Padding(6, 0, 6, 0);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(91, 28);
            lblCantidad.TabIndex = 4;
            lblCantidad.Text = "Cantidad";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(1080, 77);
            txtCantidad.Margin = new Padding(6, 7, 6, 7);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(116, 34);
            txtCantidad.TabIndex = 5;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.RoyalBlue;
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.ForeColor = Color.White;
            btnAgregar.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnAgregar.IconColor = Color.White;
            btnAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAgregar.IconSize = 24;
            btnAgregar.Location = new Point(1234, 73);
            btnAgregar.Margin = new Padding(6, 7, 6, 7);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(196, 49);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar Detalle";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(488, 85);
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
            cmbProducto.Location = new Point(593, 77);
            cmbProducto.Margin = new Padding(6, 7, 6, 7);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(364, 36);
            cmbProducto.TabIndex = 3;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(24, 83);
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
            cmbCliente.Location = new Point(107, 77);
            cmbCliente.Margin = new Padding(6, 7, 6, 7);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(364, 36);
            cmbCliente.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.RoyalBlue;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.White;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(622, 799);
            btnGuardar.Margin = new Padding(6, 7, 6, 7);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(138, 49);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnEliminar.IconColor = Color.White;
            btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEliminar.IconSize = 24;
            btnEliminar.Location = new Point(941, 799);
            btnEliminar.Margin = new Padding(6, 7, 6, 7);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(138, 49);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnNueva
            // 
            btnNueva.BackColor = Color.FromArgb(235, 235, 235);
            btnNueva.Cursor = Cursors.Hand;
            btnNueva.FlatAppearance.BorderSize = 0;
            btnNueva.FlatStyle = FlatStyle.Flat;
            btnNueva.ForeColor = Color.DimGray;
            btnNueva.IconChar = FontAwesome.Sharp.IconChar.Broom;
            btnNueva.IconColor = Color.DimGray;
            btnNueva.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNueva.IconSize = 24;
            btnNueva.Location = new Point(782, 799);
            btnNueva.Margin = new Padding(6, 7, 6, 7);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(138, 49);
            btnNueva.TabIndex = 9;
            btnNueva.Text = "Nueva";
            btnNueva.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNueva.UseVisualStyleBackColor = false;
            btnNueva.Click += btnNueva_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(22, 774);
            lblTotal.Margin = new Padding(6, 0, 6, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(86, 28);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "Total (₡):";
            // 
            // txtTotal
            // 
            txtTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotal.ForeColor = Color.RoyalBlue;
            txtTotal.Location = new Point(127, 768);
            txtTotal.Margin = new Padding(6, 7, 6, 7);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(180, 39);
            txtTotal.TabIndex = 11;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.RoyalBlue;
            pnlEncabezado.Controls.Add(lblTituloForm);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1497, 70);
            pnlEncabezado.TabIndex = 3;
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloForm.ForeColor = Color.White;
            lblTituloForm.Location = new Point(20, 18);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(287, 45);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Gestión de Ventas";
            // 
            // btnVolver
            // 
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = Color.RoyalBlue;
            btnVolver.Location = new Point(12, 831);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(217, 45);
            btnVolver.TabIndex = 12;
            btnVolver.Text = "← Volver al menú";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmVentas
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1497, 905);
            Controls.Add(btnVolver);
            Controls.Add(txtTotal);
            Controls.Add(lblTotal);
            Controls.Add(btnNueva);
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(gbNuevaVenta);
            Controls.Add(gbDetalles);
            Controls.Add(gbVentas);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(6, 7, 6, 7);
            MinimumSize = new Size(1000, 650);
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
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
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
        private FontAwesome.Sharp.IconButton btnAgregar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnNueva;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private Panel pnlEncabezado;
        private Label lblTituloForm;
        private Button btnVolver;
        private Label lblStockDisponible;
    }
}
