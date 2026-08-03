using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemadeVentas.Presentacion.Utilidades
{
    /// <summary>
    /// Centraliza la paleta de colores y las reglas de estilo visual de la aplicación,
    /// para que todos los formularios (Productos, Clientes, Ventas, Reportes) luzcan
    /// consistentes y con una jerarquía visual clara.
    /// </summary>
    public static class TemaVisual
    {
        // Paleta de colores principal de la aplicación
        public static readonly Color ColorPrimario = Color.RoyalBlue;
        public static readonly Color ColorPrimarioOscuro = Color.FromArgb(40, 70, 160);
        public static readonly Color ColorPrimarioClaro = Color.FromArgb(230, 238, 250);
        public static readonly Color ColorPeligro = Color.IndianRed;
        public static readonly Color ColorNeutro = Color.FromArgb(235, 235, 235);
        public static readonly Color ColorFondo = Color.FromArgb(245, 247, 250);
        public static readonly Color ColorTextoSecundario = Color.DimGray;
        public static readonly Color ColorFilaAlterna = Color.FromArgb(240, 244, 250);
        public static readonly Color ColorSeleccion = Color.FromArgb(66, 133, 244);
        public static readonly Font FuenteBase = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        public static readonly Font FuenteEncabezadoGrid = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);

        /// <summary>
        /// Aplica un estilo consistente a un DataGridView: encabezados resaltados,
        /// filas alternadas, selección clara y una experiencia de solo lectura
        /// pensada para listados (evita edición accidental de celdas).
        /// </summary>
        public static void EstilizarGrid(DataGridView grid, bool soloLectura = true)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.GridColor = Color.FromArgb(225, 228, 235);
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = soloLectura;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 38;
            grid.RowTemplate.Height = 32;
            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimario;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = FuenteEncabezadoGrid;
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorPrimario;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            grid.DefaultCellStyle.Font = FuenteBase;
            grid.DefaultCellStyle.SelectionBackColor = ColorSeleccion;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Padding = new Padding(6, 2, 0, 2);

            grid.AlternatingRowsDefaultCellStyle.BackColor = ColorFilaAlterna;
        }

        /// <summary>
        /// Aplica un borde plano y moderno a un conjunto de TextBox, reemplazando el
        /// borde 3D por defecto de WinForms (que se ve anticuado) por uno plano y sutil.
        /// </summary>
        public static void EstilizarCampos(params Control[] controles)
        {
            foreach (var control in controles)
            {
                if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
            }
        }

        /// <summary>
        /// Configura un ErrorProvider para que combine con la paleta de la app
        /// (parpadeo suave, icono a la derecha del control).
        /// </summary>
        public static void ConfigurarErrorProvider(ErrorProvider proveedor)
        {
            proveedor.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            proveedor.Icon = SystemIcons.Warning;
        }

        /// <summary>
        /// Marca visualmente un campo como inválido (borde rojo + icono de ErrorProvider + mensaje).
        /// </summary>
        public static void MarcarInvalido(Control control, ErrorProvider proveedor, string mensaje)
        {
            proveedor.SetError(control, mensaje);
            control.BackColor = Color.FromArgb(253, 235, 235);
        }

        /// <summary>
        /// Limpia la marca visual de error de un campo.
        /// </summary>
        public static void MarcarValido(Control control, ErrorProvider proveedor)
        {
            proveedor.SetError(control, string.Empty);
            control.BackColor = Color.White;
        }

        /// <summary>
        /// Da jerarquía tipográfica consistente a los títulos de GroupBox
        /// (semibold + color primario oscuro) en lugar del texto plano por
        /// defecto de WinForms, para que "Datos", "Listado", "Filtros", etc.
        /// se distingan claramente del resto del contenido.
        /// </summary>
        public static void EstilizarGroupBox(params GroupBox[] grupos)
        {
            foreach (var grupo in grupos)
            {
                grupo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
                grupo.ForeColor = ColorPrimarioOscuro;
            }
        }

        /// <summary>
        /// Agrega retroalimentación visual sutil al pasar el mouse sobre un
        /// IconButton (oscurece/aclara ligeramente el color base), para que
        /// los botones de acción se sientan interactivos en vez de estáticos.
        /// </summary>
        public static void AplicarEfectoHover(FontAwesome.Sharp.IconButton boton, Color colorHover)
        {
            Color colorOriginal = boton.BackColor;
            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorOriginal;
        }

        /// <summary>
        /// Calcula una variante ligeramente más oscura de un color, útil para
        /// generar el color "hover" a partir del color base de un botón.
        /// </summary>
        public static Color Oscurecer(Color color, float factor = 0.12f)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));
            return Color.FromArgb(color.A, Math.Max(r, 0), Math.Max(g, 0), Math.Max(b, 0));
        }
    }
}
