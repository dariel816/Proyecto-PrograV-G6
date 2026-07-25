namespace SistemadeVentas.Presentacion.Forms
{
    partial class FrmMenu
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
            btnProductos = new FontAwesome.Sharp.IconButton();
            btnClientes = new FontAwesome.Sharp.IconButton();
            btnVentas = new FontAwesome.Sharp.IconButton();
            btnReportes = new FontAwesome.Sharp.IconButton();
            pnlEncabezado = new Panel();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            lblBienvenida = new Label();
            lblInstruccion = new Label();
            label4 = new Label();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            //
            // btnProductos
            //
            btnProductos.Cursor = Cursors.Hand;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProductos.IconChar = FontAwesome.Sharp.IconChar.Boxes;
            btnProductos.IconColor = Color.RoyalBlue;
            btnProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnProductos.IconSize = 36;
            btnProductos.Location = new Point(150, 250);
            btnProductos.Margin = new Padding(4, 6, 4, 6);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(220, 110);
            btnProductos.TabIndex = 0;
            btnProductos.Text = "Productos";
            btnProductos.TextImageRelation = TextImageRelation.ImageAboveText;
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            //
            // btnClientes
            //
            btnClientes.Cursor = Cursors.Hand;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnClientes.IconColor = Color.RoyalBlue;
            btnClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClientes.IconSize = 36;
            btnClientes.Location = new Point(150, 390);
            btnClientes.Margin = new Padding(4, 6, 4, 6);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(220, 110);
            btnClientes.TabIndex = 1;
            btnClientes.Text = "Clientes";
            btnClientes.TextImageRelation = TextImageRelation.ImageAboveText;
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            //
            // btnVentas
            //
            btnVentas.Cursor = Cursors.Hand;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVentas.IconChar = FontAwesome.Sharp.IconChar.MoneyBillWave;
            btnVentas.IconColor = Color.RoyalBlue;
            btnVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnVentas.IconSize = 36;
            btnVentas.Location = new Point(470, 250);
            btnVentas.Margin = new Padding(4, 6, 4, 6);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(220, 110);
            btnVentas.TabIndex = 2;
            btnVentas.Text = "Ventas";
            btnVentas.TextImageRelation = TextImageRelation.ImageAboveText;
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            //
            // btnReportes
            //
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.IconChar = FontAwesome.Sharp.IconChar.ChartPie;
            btnReportes.IconColor = Color.RoyalBlue;
            btnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportes.IconSize = 36;
            btnReportes.Location = new Point(470, 390);
            btnReportes.Margin = new Padding(4, 6, 4, 6);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(220, 110);
            btnReportes.TabIndex = 3;
            btnReportes.Text = "Reportes";
            btnReportes.TextImageRelation = TextImageRelation.ImageAboveText;
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            //
            // pnlEncabezado
            //
            pnlEncabezado.BackColor = Color.RoyalBlue;
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1100, 100);
            pnlEncabezado.TabIndex = 4;
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.WhiteSmoke;
            lblSubtitulo.Location = new Point(20, 65);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(498, 28);
            lblSubtitulo.TabIndex = 4;
            lblSubtitulo.Text = "Administración de productos, clientes, ventas y reportes";
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(456, 60);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SISTEMA DE VENTAS";
            //
            // lblBienvenida
            //
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.ForeColor = Color.RoyalBlue;
            lblBienvenida.Location = new Point(25, 130);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(273, 48);
            lblBienvenida.TabIndex = 5;
            lblBienvenida.Text = "Menú principal";
            //
            // lblInstruccion
            //
            lblInstruccion.AutoSize = true;
            lblInstruccion.ForeColor = Color.DimGray;
            lblInstruccion.Location = new Point(27, 190);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(395, 28);
            lblInstruccion.TabIndex = 6;
            lblInstruccion.Text = "Seleccione el módulo que desea administrar";
            //
            // label4
            //
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.DimGray;
            label4.Location = new Point(760, 645);
            label4.Name = "label4";
            label4.Size = new Size(296, 25);
            label4.TabIndex = 7;
            label4.Text = "Sistema de Ventas | Programación 5";
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            //
            // FrmMenu
            //
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1100, 700);
            Controls.Add(label4);
            Controls.Add(lblInstruccion);
            Controls.Add(lblBienvenida);
            Controls.Add(pnlEncabezado);
            Controls.Add(btnReportes);
            Controls.Add(btnVentas);
            Controls.Add(btnClientes);
            Controls.Add(btnProductos);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.Sizable;
            Margin = new Padding(4, 6, 4, 6);
            MaximizeBox = true;
            MinimumSize = new Size(900, 600);
            Name = "FrmMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Ventas";
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FontAwesome.Sharp.IconButton btnProductos;
        private FontAwesome.Sharp.IconButton btnClientes;
        private FontAwesome.Sharp.IconButton btnVentas;
        private FontAwesome.Sharp.IconButton btnReportes;
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblBienvenida;
        private Label lblInstruccion;
        private Label label4;
    }
}
