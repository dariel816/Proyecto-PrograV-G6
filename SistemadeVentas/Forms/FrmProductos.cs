using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Entidades.DTOs;//Importación del espacio de nombres que contiene los DTO para trabajar con los datos de productos en la capa de presentación
using SistemaVentas.Negocio;



namespace SistemadeVentas.Presentacion.Forms


{
    /// <summary>
    /// Formulario de gestión de productos: alta, edición, eliminación, listado y
    /// exportación/importación del catálogo en formato JSON.
    /// </summary>
    public partial class FrmProductos : Form
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();//Instancia de la capa de negocio para productos

        /// <summary>
        /// Inicializa el formulario de productos y carga el listado inicial de productos.
        /// </summary>
        public FrmProductos()
        {
            InitializeComponent();
            CargarProductos();//Llamada al método para cargar los productos en el DataGridView al iniciar el formulario
        }

        /// <summary>
        /// Carga (o recarga) el listado de productos en el <c>DataGridView</c> del formulario.
        /// </summary>
        private void CargarProductos()
        {
            dgvProductos.DataSource = productoNegocio.ObtenerProductos();
        }

        /// <summary>
        /// Limpia los campos de entrada (ID, código, nombre, descripción, precio, stock) después
        /// de agregar, actualizar o eliminar un producto.
        /// </summary>
        private void limpiarCampos() //Método para limpiar los campos de entrada después de agregar o actualizar un producto
        {
            txtID.Clear(); // Limpiar el campo de ID (aunque generalmente no se debería mostrar ni editar el ID directamente)
            txtCodigo.Clear(); // Limpiar el campo de código
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCodigo.Focus();  // Establecer el foco en el campo de código para facilitar la entrada de datos
        }

        /// <summary>
        /// Valida que todos los campos requeridos (código, nombre, descripción, precio y stock)
        /// estén llenos.
        /// </summary>
        /// <returns><c>true</c> si todos los campos requeridos están completos; de lo contrario <c>false</c>.</returns>
        // Valida que todos los campos requeridos estén llenos y tienen formato correcto
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("El campo Código es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("El campo Descripción es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El campo Precio es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("El campo Stock es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida los campos y guarda un nuevo producto, verificando previamente que el código
        /// y el nombre no estén ya registrados.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos estén completos antes de intentar guardar
                if (!ValidarCampos())
                {
                    return;
                }
                ProductoDTO producto = new ProductoDTO();
                producto.Codigo = txtCodigo.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();

                // Usar TryParse para evitar excepciones si el usuario ingresa valores no válidos
                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                {
                    MessageBox.Show("Ingrese un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                {
                    MessageBox.Show("Ingrese un stock válido (número entero).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                producto.Precio = precio;
                producto.Stock = stock;

                // Verificar unicidad en UI antes de persistir
                if (productoNegocio.ObtenerProductos().Exists(p => string.Equals(p.Codigo?.Trim(), producto.Codigo, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El código ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus();
                    return;
                }

                if (productoNegocio.ObtenerProductos().Exists(p => string.Equals(p.Nombre?.Trim(), producto.Nombre, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("El nombre del producto ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                bool resultado = productoNegocio.InsertarProducto(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto agregado exitosamente.");
                    CargarProductos(); // Recargar los productos para mostrar el nuevo producto agregado
                    limpiarCampos(); // Limpiar los campos de entrada después de agregar el producto
                }
                else
                {
                    MessageBox.Show("Error al agregar el producto.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Manejador reservado para el evento de clic en el contenido de una celda del
        /// <c>DataGridView</c> de productos. Actualmente no realiza ninguna acción.
        /// </summary>
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) //Evento para manejar el clic en una celda del DataGridView, se utiliza para cargar los datos del producto seleccionado en los campos de entrada para su edición
        {

        }

        /// <summary>
        /// Carga los datos del producto de la fila seleccionada en el <c>DataGridView</c>
        /// hacia los campos de texto del formulario, para su edición o eliminación.
        /// </summary>
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e) //Evento para manejar el clic en una celda del DataGridView, se utiliza para cargar los datos del producto seleccionado en los campos de entrada para su edición
        {
            if (e.RowIndex >= 0) // Verificar que se haya seleccionado una fila válida
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];// Obtener la fila seleccionada
                txtCodigo.Text = fila.Cells["Codigo"].Value.ToString(); // Cargar el valor del código del producto en el campo de texto correspondiente
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString(); // Cargar el valor del nombre del producto en el campo de texto correspondiente
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString(); // Cargar el valor de la descripción del producto en el campo de texto correspondiente
                txtPrecio.Text = fila.Cells["Precio"].Value.ToString(); // Cargar el valor del precio del producto en el campo de texto correspondiente
                txtStock.Text = fila.Cells["Stock"].Value.ToString(); // Cargar el valor del stock del producto en el campo de texto correspondiente

                txtID.Text = fila.Cells["Id"].Value.ToString(); // Cargar el valor del ID del producto en el campo de texto correspondiente para su uso en la actualización o eliminación del producto


            }
        }

        /// <summary>
        /// Valida los campos y actualiza el producto seleccionado.
        /// </summary>
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try

            {

                // Validar campos antes de editar
                if (!ValidarCampos())
                    return;

                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Seleccione un producto válido para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProductoDTO producto = new ProductoDTO();

                if (!int.TryParse(txtID.Text.Trim(), out int id))
                {
                    MessageBox.Show("ID de producto inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                producto.Id = id; // Convertir el valor del ID del producto a entero para su uso en la actualización
                producto.Codigo = txtCodigo.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                {
                    MessageBox.Show("Ingrese un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                {
                    MessageBox.Show("Ingrese un stock válido (número entero).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return;
                }

                producto.Precio = precio;
                producto.Stock = stock;

                bool resultado = productoNegocio.EditarProducto(producto);

                if (resultado)
                {
                    MessageBox.Show("Producto actualizado exitosamente.");
                    CargarProductos(); // Recargar los productos para mostrar el producto actualizado   
                    limpiarCampos(); // Limpiar los campos de entrada después de actualizar el producto


                }
                else
                {
                    MessageBox.Show("Error al actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        /// <summary>
        /// Solicita confirmación y elimina el producto seleccionado.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text == "")
                {
                    MessageBox.Show("Seleccione un producto para eliminar.");
                    return;
                }

                DialogResult resultadoPregunta = MessageBox.Show(

                    "Esta Seguro que desea eliminar el producto seleccionado",
                    "Desea Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultadoPregunta == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(txtID.Text); // Convertir el valor del ID del producto a entero para su uso en la eliminación
                    bool resultado = productoNegocio.EliminarProducto(id); // Llamar al método para eliminar el producto de la base de datos
                    if (resultado)
                    {
                        MessageBox.Show("Producto eliminado exitosamente.");
                        CargarProductos(); // Recargar los productos para mostrar el producto eliminado
                        limpiarCampos(); // Limpiar los campos de entrada después de eliminar el producto
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }




        }

        /// <summary>
        /// Limpia los campos de entrada al presionar el botón Limpiar.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos(); // Llamada al método para limpiar los campos de entrada cuando se hace clic en el botón "Limpiar"
        }

        /// <summary>
        /// Solicita al usuario una ruta de archivo y exporta el catálogo completo de productos
        /// a un archivo JSON mediante <see cref="ProductoNegocio.ExportarCatalogoJson"/>.
        /// </summary>
        private void btnExportarJson_Click(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog { Filter = "Archivo JSON (*.json)|*.json", FileName = "CatalogoProductos.json" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    productoNegocio.ExportarCatalogoJson(dialogo.FileName);
                    MessageBox.Show("Catálogo exportado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar el catálogo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Solicita al usuario un archivo JSON y lo importa al catálogo de productos mediante
        /// <see cref="ProductoNegocio.ImportarCatalogoJson"/>, informando cuántos productos
        /// fueron importados y cuántos omitidos.
        /// </summary>
        private void btnImportarJson_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog { Filter = "Archivo JSON (*.json)|*.json" })
            {
                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    var resultado = productoNegocio.ImportarCatalogoJson(dialogo.FileName);
                    CargarProductos();
                    MessageBox.Show(
                        $"Importación completa: {resultado.Importados} producto(s) importado(s), {resultado.Omitidos} omitido(s).",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al importar el catálogo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close(); // Cierra el formulario de productos y vuelve al menú principal
        }
    }
}









