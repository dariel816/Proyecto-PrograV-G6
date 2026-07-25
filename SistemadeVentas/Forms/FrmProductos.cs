using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaVentas.Datos.DAO;//Importación del espacio de nombres que contiene la clase ProductoDAO para acceder a los métodos de datos relacionados con productos
using SistemaVentas.Entidades.Modelos;//Importación del espacio de nombres que contiene la clase Producto para trabajar con los objetos de tipo Producto
using SistemaVentas.Negocio;



namespace SistemadeVentas.Presentacion.Forms


{
    public partial class FrmProductos : Form
    {
        ProductoNegocio productoNegocio = new ProductoNegocio();//Instancia de la capa de negocio para productos

        public FrmProductos()
        {
            InitializeComponent();
            CargarProductos();//Llamada al método para cargar los productos en el DataGridView al iniciar el formulario
        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = productoNegocio.ObtenerProductos();
        }

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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que todos los campos estén completos antes de intentar guardar
                if (!ValidarCampos())
                {
                    return;
                }
                Producto producto = new Producto();
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

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) //Evento para manejar el clic en una celda del DataGridView, se utiliza para cargar los datos del producto seleccionado en los campos de entrada para su edición
        {

        }

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

                Producto producto = new Producto();

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos(); // Llamada al método para limpiar los campos de entrada cuando se hace clic en el botón "Limpiar"
        }
    }
}









