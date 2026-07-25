using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class DetalleVentaNegocio
    {
        private readonly IDetalleVentaRepositorio detalleVentaRepositorio;
        private readonly IProductoRepositorio productoRepositorio;

        public DetalleVentaNegocio()
        {
            detalleVentaRepositorio = RepositorioFactory.CrearDetalleVentaRepositorio();
            productoRepositorio = RepositorioFactory.CrearProductoRepositorio();
        }

        public List<DetalleVentaDTO> ObtenerDetallesPorVenta(int ventaId)
        {
            List<DetalleVenta> detalles = detalleVentaRepositorio.ObtenerDetallesPorVenta(ventaId);
            return detalles.Select(ADto).ToList();
        }

        public DetalleVentaDTO? ObtenerDetallePorId(int id)
        {
            DetalleVenta? detalle = detalleVentaRepositorio.ObtenerDetallePorId(id);
            return detalle == null ? null : ADto(detalle);
        }

        public bool AgregarDetalle(DetalleVentaDTO detalleDto)
        {
            if (detalleDto.ProductoId <= 0)
                throw new Exception("El producto es requerido.");

            if (detalleDto.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            if (detalleDto.PrecioUnitario <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            detalleDto.Subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario;

            return detalleVentaRepositorio.InsertarDetalleVenta(AEntidad(detalleDto));
        }

        public bool EditarDetalle(DetalleVentaDTO detalleDto)
        {
            if (detalleDto.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            if (detalleDto.PrecioUnitario <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            detalleDto.Subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario;

            return detalleVentaRepositorio.EditarDetalleVenta(AEntidad(detalleDto));
        }

        public bool EliminarDetalle(int id)
        {
            return detalleVentaRepositorio.EliminarDetalleVenta(id);
        }

        private DetalleVentaDTO ADto(DetalleVenta detalle)
        {
            var producto = productoRepositorio.ObtenerProductoPorId(detalle.ProductoId);

            return new DetalleVentaDTO
            {
                Id = detalle.Id,
                VentaId = detalle.VentaId,
                ProductoId = detalle.ProductoId,
                ProductoNombre = producto?.Nombre ?? string.Empty,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
                Subtotal = detalle.Subtotal
            };
        }

        private static DetalleVenta AEntidad(DetalleVentaDTO detalleDto)
        {
            return new DetalleVenta
            {
                Id = detalleDto.Id,
                VentaId = detalleDto.VentaId,
                ProductoId = detalleDto.ProductoId,
                Cantidad = detalleDto.Cantidad,
                PrecioUnitario = detalleDto.PrecioUnitario,
                Subtotal = detalleDto.Subtotal
            };
        }
    }
}
