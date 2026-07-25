using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class ProductoNegocio
    {
        private readonly IProductoRepositorio productoRepositorio;

        public ProductoNegocio()
        {
            productoRepositorio = RepositorioFactory.CrearProductoRepositorio();
        }

        public List<ProductoDTO> ObtenerProductos()
        {
            return productoRepositorio.ObtenerProductos().Select(ADto).ToList();
        }

        public ProductoDTO? ObtenerProductoPorId(int id)
        {
            var producto = productoRepositorio.ObtenerProductoPorId(id);
            return producto == null ? null : ADto(producto);
        }

        public bool InsertarProducto(ProductoDTO productoDto)
        {
            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (productoDto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            // Validaciones de unicidad
            if (productoRepositorio.ExisteCodigo(productoDto.Codigo, null))
                throw new Exception("El código ya está registrado.");

            if (productoRepositorio.ExisteNombre(productoDto.Nombre, null))
                throw new Exception("El nombre del producto ya está registrado.");

            return productoRepositorio.InsertarProducto(AEntidad(productoDto));
        }

        public bool EditarProducto(ProductoDTO productoDto)
        {
            if (string.IsNullOrWhiteSpace(productoDto.Nombre))
                throw new Exception("El nombre del producto es requerido.");

            if (productoDto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            // Validaciones de unicidad (excluir el propio registro)
            if (productoRepositorio.ExisteCodigo(productoDto.Codigo, productoDto.Id))
                throw new Exception("El código ya está registrado por otro producto.");

            if (productoRepositorio.ExisteNombre(productoDto.Nombre, productoDto.Id))
                throw new Exception("El nombre del producto ya está registrado por otro producto.");

            return productoRepositorio.EditarProducto(AEntidad(productoDto));
        }

        public bool EliminarProducto(int id)
        {
            return productoRepositorio.EliminarProducto(id);
        }

        public bool ActualizarStock(int id, int nuevoStock)
        {
            return productoRepositorio.ActualizarStock(id, nuevoStock);
        }

        public bool ActualizarStock(int productoId, int nuevoStock, MySqlConnection conexion, MySqlTransaction transaccion)
        {
            return productoRepositorio.ActualizarStock(productoId, nuevoStock, conexion, transaccion);
        }

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
