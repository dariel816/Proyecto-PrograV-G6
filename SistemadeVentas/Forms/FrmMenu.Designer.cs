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
            btnClientes = new Button();
            btnVentas = new Button();
            btnReportes = new Button();
            pnlEncabezado = new Panel();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            lblBienvenida = new Label();
            lblInstruccion = new Label();
            lbl = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnProductosNuevo = new FontAwesome.Sharp.IconButton();
            pnlEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // btnClientes
            // 
            btnClientes.Cursor = Cursors.Hand;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClientes.Location = new Point(81, 389);
            btnClientes.Margin = new Padding(4, 6, 4, 6);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(170, 100);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "👥  Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnVentas
            // 
            btnVentas.Cursor = Cursors.Hand;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVentas.Location = new Point(376, 258);
            btnVentas.Margin = new Padding(4, 6, 4, 6);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(170, 100);
            btnVentas.TabIndex = 1;
            btnVentas.Text = "💰  Ventas";
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            // 
            // btnReportes
            // 
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Location = new Point(376, 389);
            btnReportes.Margin = new Padding(4, 6, 4, 6);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(170, 100);
            btnReportes.TabIndex = 2;
            btnReportes.Text = "📊  Reportes";
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
            pnlEncabezado.Size = new Size(966, 100);
            pnlEncabezado.TabIndex = 3;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = SystemColors.WindowText;
            lblSubtitulo.Location = new Point(3, 72);
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
            lblTitulo.Location = new Point(3, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(456, 60);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SISTEMA DE VENTAS";
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblBienvenida.Location = new Point(25, 127);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(273, 48);
            lblBienvenida.TabIndex = 4;
            lblBienvenida.Text = "Menú principal";
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Location = new Point(23, 197);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(395, 28);
            lblInstruccion.TabIndex = 5;
            lblInstruccion.Text = "Seleccione el módulo que desea administrar";
            // 
            // lbl
            // 
            lbl.AutoSize = true;
            lbl.Location = new Point(80, 331);
            lbl.Name = "lbl";
            lbl.Size = new Size(197, 28);
            lbl.TabIndex = 6;
            lbl.Text = "Inventario y catálogo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 480);
            label1.Name = "label1";
            label1.Size = new Size(186, 28);
            label1.TabIndex = 7;
            label1.Text = " Registro de clientes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(376, 331);
            label2.Name = "label2";
            label2.Size = new Size(184, 28);
            label2.TabIndex = 8;
            label2.Text = "Crear nuevas ventas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(376, 480);
            label3.Name = "label3";
            label3.Size = new Size(220, 28);
            label3.TabIndex = 9;
            label3.Text = "Consultas y exportación\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(631, 516);
            label4.Name = "label4";
            label4.Size = new Size(296, 25);
            label4.TabIndex = 10;
            label4.Text = "Sistema de Ventas | Programación 5";
            // 
            // btnProductosNuevo
            // 
            btnProductosNuevo.Cursor = Cursors.Hand;
            btnProductosNuevo.FlatAppearance.BorderSize = 0;
            btnProductosNuevo.FlatStyle = FlatStyle.Flat;
            btnProductosNuevo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProductosNuevo.IconChar = FontAwesome.Sharp.IconChar.Box;
            btnProductosNuevo.IconColor = Color.White;
            btnProductosNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnProductosNuevo.IconSize = 42;
            btnProductosNuevo.ImageAlign = ContentAlignment.MiddleLeft;
            btnProductosNuevo.Location = new Point(81, 233);
            btnProductosNuevo.Name = "btnProductosNuevo";
            btnProductosNuevo.Size = new Size(220, 95);
            btnProductosNuevo.TabIndex = 11;
            btnProductosNuevo.Text = "Productos";
            btnProductosNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProductosNuevo.UseVisualStyleBackColor = true;
            btnProductosNuevo.Click += btnProductosNuevo_Click;
            // 
            // FrmMenu
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            // enlarge default size and allow resizing
            ClientSize = new Size(1100, 700);
            Controls.Add(btnProductosNuevo);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lbl);
            Controls.Add(lblInstruccion);
            Controls.Add(lblBienvenida);
            Controls.Add(pnlEncabezado);
            Controls.Add(btnReportes);
            Controls.Add(btnVentas);
            Controls.Add(btnClientes);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ActiveCaptionText;
            FormBorderStyle = FormBorderStyle.Sizable;
            Margin = new Padding(4, 6, 4, 6);
            MaximizeBox = true;
            Name = "FrmMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Ventas";
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnClientes;
        private Button btnVentas;
        private Button btnReportes;
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblBienvenida;
        private Label lblInstruccion;
        private Label lbl;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private FontAwesome.Sharp.IconButton btnProductosNuevo;
    }
}