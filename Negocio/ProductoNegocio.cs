using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    /// <summary>
    /// Reglas de negocio y validaciones para la gestión de productos, incluyendo
    /// operaciones de stock y exportación/importación del catálogo en formato JSON.
    /// Trabaja con <see cref="ProductoDTO"/> y delega el acceso a datos en el repositorio
    /// obtenido mediante <see cref="RepositorioFactory"/>.
    /// </summary>
    public class ProductoNegocio
    {
        private readonly IProductoRepositorio productoRepositorio;

        /// <summary>
        /// Crea una nueva instancia de <see cref="ProductoNegocio"/> y obtiene el repositorio
        /// de productos a través de la fábrica de repositorios.
        /// </summary>
        public ProductoNegocio()
        {
            productoRepositorio = RepositorioFactory.CrearProductoRepositorio();
        }

        /// <summary>
        /// Obtiene la lista completa de productos registrados.
        /// </summary>
        /// <returns>Lista de productos en formato <see cref="ProductoDTO"/>.</returns>
        public List<ProductoDTO> ObtenerProductos()
        {
            return productoRepositorio.ObtenerProductos().Select(ADto).ToList();
        }

        /// <summary>
        /// Busca un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <returns>El <see cref="ProductoDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        public ProductoDTO? ObtenerProductoPorId(int id)
        {
            var producto = productoRepositorio.ObtenerProductoPorId(id);
            return producto == null ? null : ADto(producto);
        }

        /// <summary>
        /// Obtiene un producto usando la conexión y transacción indicadas, bloqueando su fila
        /// hasta finalizar la transacción. Se utiliza al validar y descontar inventario.
        /// </summary>
        /// <param name="id">Id del producto.</param>
        /// <param name="conexion">Conexión abierta de la transacción actual.</param>
        /// <param name="transaccion">Transacción MySQL actual.</param>
        /// <returns>El producto bloqueado, o <c>null</c> si no existe.</returns>
        public ProductoDTO? ObtenerProductoPorId(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            var producto = productoRepositorio.ObtenerProductoPorId(id, conexion, transaccion);
            return producto == null ? null : ADto(producto);
        }

        /// <summary>
        /// Valida y registra un nuevo producto, verificando que el nombre no esté vacío,
        /// que el precio sea mayor a 0 y que el código y el nombre no estén ya registrados.
        /// </summary>
        /// <param name="productoDto">Datos del producto a insertar.</param>
        /// <returns><c>true</c> si el producto fue insertado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool InsertarProducto(ProductoDTO productoDto)
        {
            ValidarDatosProducto(productoDto);

            if (productoRepositorio.ExisteCodigo(productoDto.Codigo, null))
                throw new Exception("El código ya está registrado.");

            if (productoRepositorio.ExisteNombre(productoDto.Nombre, null))
                throw new Exception("El nombre del producto ya está registrado.");

            return productoRepositorio.InsertarProducto(AEntidad(productoDto));
        }

        /// <summary>
        /// Valida y actualiza los datos de un producto existente, verificando que el nombre
        /// no esté vacío, que el precio sea mayor a 0 y que el código y el nombre no estén
        /// registrados por otro producto.
        /// </summary>
        /// <param name="productoDto">Datos actualizados del producto.</param>
        /// <returns><c>true</c> si el producto fue actualizado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool EditarProducto(ProductoDTO productoDto)
        {
            ValidarDatosProducto(productoDto);

            if (productoDto.Id <= 0)
                throw new Exception("El producto seleccionado no es válido.");

            if (productoRepositorio.ExisteCodigo(
                    productoDto.Codigo,
                    productoDto.Id))
            {
                throw new Exception(
                    "El código ya está registrado por otro producto.");
            }

            if (productoRepositorio.ExisteNombre(
                    productoDto.Nombre,
                    productoDto.Id))
            {
                throw new Exception(
                    "El nombre del producto ya está registrado por otro producto.");
            }

            return productoRepositorio.EditarProducto(AEntidad(productoDto));
        }

        /// <summary>
        /// Elimina un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar.</param>
        /// <returns><c>true</c> si el producto fue eliminado correctamente.</returns>
        public bool EliminarProducto(int id)
        {
            if (id <= 0)
                throw new Exception("El producto seleccionado no es válido.");

            if (productoRepositorio.TieneVentas(id))
            {
                throw new Exception(
                    "No se puede eliminar el producto porque aparece en una venta registrada.");
            }

            return productoRepositorio.EliminarProducto(id);
        }

        /// <summary>
        /// Actualiza el stock de un producto de forma independiente (sin transacción externa).
        /// </summary>
        /// <param name="id">Identificador del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a establecer.</param>
        /// <returns><c>true</c> si el stock fue actualizado correctamente.</returns>
        public bool ActualizarStock(int id, int nuevoStock)
        {
            return productoRepositorio.ActualizarStock(id, nuevoStock);
        }

        /// <summary>
        /// Actualiza el stock de un producto participando en una conexión y transacción MySQL
        /// existentes, permitiendo que la operación forme parte de una transacción más amplia
        /// (por ejemplo, la creación de una venta).
        /// </summary>
        /// <param name="productoId">Identificador del producto.</param>
        /// <param name="nuevoStock">Nueva cantidad de stock a establecer.</param>
        /// <param name="conexion">Conexión MySQL activa sobre la que se ejecuta la operación.</param>
        /// <param name="transaccion">Transacción MySQL en curso a la que se une la operación.</param>
        /// <returns><c>true</c> si el stock fue actualizado correctamente.</returns>
        public bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion)
        {
            return productoRepositorio.ActualizarStock(productoId, nuevoStock, conexion, transaccion);
        }

        /// <summary>
        /// Exporta el catálogo completo de productos a un archivo JSON, serializando
        /// la lista de <see cref="ProductoDTO"/> con formato indentado mediante <c>System.Text.Json</c>.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo .json de destino donde se escribirá el catálogo.</param>
        public void ExportarCatalogoJson(string rutaArchivo)
        {
            List<ProductoDTO> productos = ObtenerProductos();

            string json = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(rutaArchivo, json);
        }

        /// <summary>
        /// Importa un catálogo de productos desde un archivo JSON, deserializando una lista
        /// de <see cref="ProductoDTO"/> con <c>System.Text.Json</c> e insertando cada producto
        /// mediante <see cref="InsertarProducto"/>. Los productos importados se insertan siempre
        /// como nuevos (se ignora el Id original) y los que fallan la validación de negocio
        /// se omiten sin detener la importación del resto.
        /// </summary>
        /// <param name="rutaArchivo">Ruta del archivo .json de origen a importar.</param>
        /// <returns>Una tupla con la cantidad de productos importados y la cantidad omitidos por error.</returns>
        public (int Importados, int Omitidos) ImportarCatalogoJson(string rutaArchivo)
        {
            string json = File.ReadAllText(rutaArchivo);

            List<ProductoDTO>? productos = JsonSerializer.Deserialize<List<ProductoDTO>>(json);

            int importados = 0;
            int omitidos = 0;

            if (productos != null)
            {
                foreach (ProductoDTO producto in productos)
                {
                    try
                    {
                        producto.Id = 0;
                        if (InsertarProducto(producto))
                            importados++;
                        else
                            omitidos++;
                    }
                    catch
                    {
                        omitidos++;
                    }
                }
            }

            return (importados, omitidos);
        }

        /// <summary>
        /// Función de mapeo: convierte una entidad <see cref="Producto"/> en su <see cref="ProductoDTO"/> correspondiente.
        /// </summary>
        /// <param name="producto">Entidad de producto proveniente del repositorio.</param>
        /// <returns>El <see cref="ProductoDTO"/> equivalente.</returns>
        private static ProductoDTO ADto(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock
            };
        }


        private static void ValidarDatosProducto(ProductoDTO productoDto)
        {
            if (productoDto == null)
                throw new ArgumentNullException(nameof(productoDto));

            productoDto.Codigo = productoDto.Codigo?.Trim() ?? string.Empty;
            productoDto.Nombre = productoDto.Nombre?.Trim() ?? string.Empty;
            productoDto.Descripcion =
                productoDto.Descripcion?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(productoDto.Codigo))
                throw new Exception("El código del producto es requerido.");

            if (productoDto.Codigo.Length > 50)
                throw new Exception("El código no puede superar los 50 caracteres.");

            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (productoDto.Nombre.Length > 100)
                throw new Exception("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(productoDto.Descripcion))
                throw new Exception("La descripción del producto es requerida.");

            if (productoDto.Descripcion.Length > 255)
            {
                throw new Exception(
                    "La descripción no puede superar los 255 caracteres.");
            }

            if (productoDto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            if (productoDto.Precio > 99_999_999.99m)
                throw new Exception("El precio ingresado es demasiado alto.");

            if (productoDto.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");
        }
        /// <summary>
        /// Función de mapeo: convierte un <see cref="ProductoDTO"/> en la entidad <see cref="Producto"/> correspondiente.
        /// </summary>
        /// <param name="productoDto">DTO de producto proveniente de la capa de presentación.</param>
        /// <returns>La entidad <see cref="Producto"/> equivalente.</returns>
        private static Producto AEntidad(ProductoDTO productoDto)
        {
            return new Producto
            {
                Id = productoDto.Id,
                Codigo = productoDto.Codigo,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Stock = productoDto.Stock
            };
        }
    }
}