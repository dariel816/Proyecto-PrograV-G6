namespace SistemadeVentas.Presentacion.Forms
{
    partial class FrmLogin
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlEncabezado = new Panel();
            lblSubtitulo = new Label();
            lblTituloForm = new Label();
            pnlContenido = new Panel();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            lblError = new Label();
            btnIngresar = new FontAwesome.Sharp.IconButton();
            btnSalir = new FontAwesome.Sharp.IconButton();
            pnlEncabezado.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.RoyalBlue;
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTituloForm);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(409, 90);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 9.75F);
            lblSubtitulo.ForeColor = Color.FromArgb(230, 238, 250);
            lblSubtitulo.Location = new Point(26, 54);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(169, 28);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Sistema de Ventas";
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloForm.ForeColor = Color.White;
            lblTituloForm.Location = new Point(24, 16);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(242, 48);
            lblTituloForm.TabIndex = 0;
            lblTituloForm.Text = "Iniciar Sesión";
            // 
            // pnlContenido
            // 
            pnlContenido.Controls.Add(lblUsuario);
            pnlContenido.Controls.Add(txtUsuario);
            pnlContenido.Controls.Add(lblClave);
            pnlContenido.Controls.Add(txtClave);
            pnlContenido.Controls.Add(lblError);
            pnlContenido.Controls.Add(btnIngresar);
            pnlContenido.Controls.Add(btnSalir);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 90);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(30, 25, 30, 20);
            pnlContenido.Size = new Size(409, 244);
            pnlContenido.TabIndex = 1;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.ForeColor = Color.DimGray;
            lblUsuario.Location = new Point(30, 25);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(76, 25);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(30, 44);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Nombre de usuario";
            txtUsuario.Size = new Size(340, 31);
            txtUsuario.TabIndex = 1;
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.ForeColor = Color.DimGray;
            lblClave.Location = new Point(30, 82);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(105, 25);
            lblClave.TabIndex = 2;
            lblClave.Text = "Contraseña:";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(30, 101);
            txtClave.Name = "txtClave";
            txtClave.PlaceholderText = "••••••••";
            txtClave.Size = new Size(340, 31);
            txtClave.TabIndex = 3;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.IndianRed;
            lblError.Location = new Point(30, 132);
            lblError.Name = "lblError";
            lblError.Size = new Size(275, 25);
            lblError.TabIndex = 4;
            lblError.Text = "Usuario o contraseña incorrectos.";
            lblError.Visible = false;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.RoyalBlue;
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.ForeColor = Color.White;
            btnIngresar.IconChar = FontAwesome.Sharp.IconChar.SignIn;
            btnIngresar.IconColor = Color.White;
            btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIngresar.IconSize = 20;
            btnIngresar.Location = new Point(24, 183);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(160, 38);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "Ingresar";
            btnIngresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(235, 235, 235);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.ForeColor = Color.DimGray;
            btnSalir.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnSalir.IconColor = Color.DimGray;
            btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSalir.IconSize = 18;
            btnSalir.Location = new Point(216, 183);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(160, 38);
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            btnSalir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmLogin
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(409, 334);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión - Sistema de Ventas";
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEncabezado;
        private Label lblTituloForm;
        private Label lblSubtitulo;
        private Panel pnlContenido;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblClave;
        private TextBox txtClave;
        private Label lblError;
        private FontAwesome.Sharp.IconButton btnIngresar;
        private FontAwesome.Sharp.IconButton btnSalir;
    }
}
