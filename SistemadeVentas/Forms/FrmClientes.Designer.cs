namespace SistemadeVentas.Presentacion.Forms
{
    partial class FrmClientes
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
            txtId = new TextBox();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            lblNombre = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            btnEditar = new FontAwesome.Sharp.IconButton();
            btnEliminar = new FontAwesome.Sharp.IconButton();
            btnLimpiar = new FontAwesome.Sharp.IconButton();
            btnExportarJson = new FontAwesome.Sharp.IconButton();
            btnImportarJson = new FontAwesome.Sharp.IconButton();
            dgvClientes = new DataGridView();
            pnlEncabezado = new Panel();
            lblTituloForm = new Label();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // txtId
            // 
            txtId.Location = new Point(700, 97);
            txtId.Name = "txtId";
            txtId.Size = new Size(100, 33);
            txtId.TabIndex = 10;
            txtId.Visible = false;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(140, 97);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(320, 33);
            txtNombre.TabIndex = 1;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(140, 137);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(320, 33);
            txtTelefono.TabIndex = 2;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(140, 177);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(320, 33);
            txtCorreo.TabIndex = 3;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(30, 100);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(89, 28);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(30, 140);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(90, 28);
            lblTelefono.TabIndex = 0;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(30, 180);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(76, 28);
            lblCorreo.TabIndex = 0;
            lblCorreo.Text = "Correo:";
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
            btnGuardar.IconSize = 20;
            btnGuardar.Location = new Point(30, 262);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 36);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.FromArgb(230, 238, 250);
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.ForeColor = Color.RoyalBlue;
            btnEditar.IconChar = FontAwesome.Sharp.IconChar.Edit;
            btnEditar.IconColor = Color.RoyalBlue;
            btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnEditar.IconSize = 20;
            btnEditar.Location = new Point(167, 262);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(120, 36);
            btnEditar.TabIndex = 5;
            btnEditar.Text = "Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
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
            btnEliminar.IconSize = 20;
            btnEliminar.Location = new Point(322, 262);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 36);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(235, 235, 235);
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.ForeColor = Color.DimGray;
            btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            btnLimpiar.IconColor = Color.DimGray;
            btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLimpiar.IconSize = 20;
            btnLimpiar.Location = new Point(465, 262);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(120, 36);
            btnLimpiar.TabIndex = 7;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnExportarJson
            // 
            btnExportarJson.BackColor = Color.FromArgb(230, 240, 250);
            btnExportarJson.Cursor = Cursors.Hand;
            btnExportarJson.FlatAppearance.BorderSize = 0;
            btnExportarJson.FlatStyle = FlatStyle.Flat;
            btnExportarJson.ForeColor = Color.SteelBlue;
            btnExportarJson.IconChar = FontAwesome.Sharp.IconChar.ArrowRightFromFile;
            btnExportarJson.IconColor = Color.SteelBlue;
            btnExportarJson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportarJson.IconSize = 18;
            btnExportarJson.Location = new Point(719, 262);
            btnExportarJson.Name = "btnExportarJson";
            btnExportarJson.Size = new Size(160, 41);
            btnExportarJson.TabIndex = 8;
            btnExportarJson.Text = "Exportar JSON";
            btnExportarJson.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExportarJson.UseVisualStyleBackColor = false;
            btnExportarJson.Click += btnExportarJson_Click;
            // 
            // btnImportarJson
            // 
            btnImportarJson.BackColor = Color.FromArgb(250, 242, 225);
            btnImportarJson.Cursor = Cursors.Hand;
            btnImportarJson.FlatAppearance.BorderSize = 0;
            btnImportarJson.FlatStyle = FlatStyle.Flat;
            btnImportarJson.ForeColor = Color.DarkOrange;
            btnImportarJson.IconChar = FontAwesome.Sharp.IconChar.ArrowRightToFile;
            btnImportarJson.IconColor = Color.DarkOrange;
            btnImportarJson.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnImportarJson.IconSize = 18;
            btnImportarJson.Location = new Point(719, 206);
            btnImportarJson.Name = "btnImportarJson";
            btnImportarJson.Size = new Size(160, 41);
            btnImportarJson.TabIndex = 9;
            btnImportarJson.Text = "Importar JSON";
            btnImportarJson.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnImportarJson.UseVisualStyleBackColor = false;
            btnImportarJson.Click += btnImportarJson_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.GridColor = Color.White;
            dgvClientes.Location = new Point(30, 328);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 62;
            dgvClientes.Size = new Size(840, 290);
            dgvClientes.TabIndex = 8;
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.RoyalBlue;
            pnlEncabezado.Controls.Add(lblTituloForm);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(929, 70);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloForm.ForeColor = Color.White;
            lblTituloForm.Location = new Point(20, 18);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(306, 45);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Gestión de Clientes";
            // 
            // btnVolver
            // 
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderColor = Color.RoyalBlue;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = Color.RoyalBlue;
            btnVolver.Location = new Point(30, 634);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(217, 34);
            btnVolver.TabIndex = 11;
            btnVolver.Text = "← Volver al menú";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FrmClientes
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(929, 680);
            Controls.Add(btnVolver);
            Controls.Add(dgvClientes);
            Controls.Add(btnImportarJson);
            Controls.Add(btnExportarJson);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnGuardar);
            Controls.Add(lblCorreo);
            Controls.Add(lblTelefono);
            Controls.Add(lblNombre);
            Controls.Add(txtCorreo);
            Controls.Add(txtTelefono);
            Controls.Add(txtNombre);
            Controls.Add(txtId);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MinimumSize = new Size(700, 450);
            Name = "FrmClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Clientes";
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtId;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private Label lblNombre;
        private Label lblTelefono;
        private Label lblCorreo;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnExportarJson;
        private FontAwesome.Sharp.IconButton btnImportarJson;
        private DataGridView dgvClientes;
        private Panel pnlEncabezado;
        private Label lblTituloForm;
        private Button btnVolver;
    }
}
