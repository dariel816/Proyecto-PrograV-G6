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
            btnProductos = new Button();
            btnClientes = new Button();
            btnVentas = new Button();
            btnReportes = new Button();
            SuspendLayout();
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(166, 68);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(75, 23);
            btnProductos.TabIndex = 0;
            btnProductos.Text = "Productos";
            btnProductos.UseVisualStyleBackColor = true;
            btnProductos.Click += btnProductos_Click;
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(309, 68);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(75, 23);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnVentas
            // 
            btnVentas.Location = new Point(452, 68);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(75, 23);
            btnVentas.TabIndex = 1;
            btnVentas.Text = "Ventas";
            btnVentas.UseVisualStyleBackColor = true;
            btnVentas.Click += btnVentas_Click;
            //
            // btnReportes
            //
            btnReportes.Location = new Point(595, 68);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(75, 23);
            btnReportes.TabIndex = 2;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = true;
            btnReportes.Click += btnReportes_Click;
            //
            // FrmMenu
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnReportes);
            Controls.Add(btnVentas);
            Controls.Add(btnClientes);
            Controls.Add(btnProductos);
            Name = "FrmMenu";
            Text = "Menu Principal";
            ResumeLayout(false);
        }

        #endregion

        private Button btnProductos;
        private Button btnClientes;
        private Button btnVentas;
        private Button btnReportes;
    }
}