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
    /// <summary>
    /// Reglas de negocio y validaciones para la gestión de los detalles (líneas) de una venta,
    /// incluyendo el cálculo de subtotales. Trabaja con <see cref="DetalleVentaDTO"/> y delega
    /// el acceso a datos en los repositorios obtenidos mediante <see cref="RepositorioFactory"/>.
    /// </summary>
    public class DetalleVentaNegocio
    {
        private readonly IDetalleVentaRepositorio detalleVentaRepositorio;
        private readonly IProductoRepositorio productoRepositorio;

        /// <summary>
        /// Crea una nueva instancia de <see cref="DetalleVentaNegocio"/> y obtiene los repositorios
        /// de detalles de venta y de productos a través de la fábrica de repositorios.
        /// </summary>
        public DetalleVentaNegocio()
        {
            detalleVentaRepositorio = RepositorioFactory.CrearDetalleVentaRepositorio();
            productoRepositorio = RepositorioFactory.CrearProductoRepositorio();
        }

        /// <summary>
        /// Obtiene todos los detalles asociados a una venta específica.
        /// </summary>
        /// <param name="ventaId">Identificador de la venta.</param>
        /// <returns>Lista de detalles en formato <see cref="DetalleVentaDTO"/>.</returns>
        public List<DetalleVentaDTO> ObtenerDetallesPorVenta(int ventaId)
        {
            List<DetalleVenta> detalles = detalleVentaRepositorio.ObtenerDetallesPorVenta(ventaId);
            return detalles.Select(ADto).ToList();
        }

        /// <summary>
        /// Busca un detalle de venta por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle de venta.</param>
        /// <returns>El <see cref="DetalleVentaDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        public DetalleVentaDTO? ObtenerDetallePorId(int id)
        {
            DetalleVenta? detalle = detalleVentaRepositorio.ObtenerDetallePorId(id);
            return detalle == null ? null : ADto(detalle);
        }

        /// <summary>
        /// Valida y agrega un nuevo detalle a una venta, verificando que el producto sea válido
        /// y que la cantidad y el precio sean mayores a 0. Calcula el subtotal antes de guardar.
        /// </summary>
        /// <param name="detalleDto">Datos del detalle a agregar.</param>
        /// <returns><c>true</c> si el detalle fue insertado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
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

        /// <summary>
        /// Valida y actualiza un detalle de venta existente, verificando que la cantidad y el
        /// precio sean mayores a 0. Recalcula el subtotal antes de guardar.
        /// </summary>
        /// <param name="detalleDto">Datos actualizados del detalle de venta.</param>
        /// <returns><c>true</c> si el detalle fue actualizado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool EditarDetalle(DetalleVentaDTO detalleDto)
        {
            if (detalleDto.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a 0.");

            if (detalleDto.PrecioUnitario <= 0)
                throw new Exception("El precio debe ser mayor a 0.");

            detalleDto.Subtotal = detalleDto.Cantidad * detalleDto.PrecioUnitario;

            return detalleVentaRepositorio.EditarDetalleVenta(AEntidad(detalleDto));
        }

        /// <summary>
        /// Elimina un detalle de venta por su identificador.
        /// </summary>
        /// <param name="id">Identificador del detalle a eliminar.</param>
        /// <returns><c>true</c> si el detalle fue eliminado correctamente.</returns>
        public bool EliminarDetalle(int id)
        {
            return detalleVentaRepositorio.EliminarDetalleVenta(id);
        }

        /// <summary>
        /// Función de mapeo: convierte una entidad <see cref="DetalleVenta"/> en su
        /// <see cref="DetalleVentaDTO"/> correspondiente, resolviendo el nombre del producto asociado.
        /// </summary>
        /// <param name="detalle">Entidad de detalle de venta proveniente del repositorio.</param>
        /// <returns>El <see cref="DetalleVentaDTO"/> equivalente.</returns>
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

        /// <summary>
        /// Función de mapeo: convierte un <see cref="DetalleVentaDTO"/> en la entidad
        /// <see cref="DetalleVenta"/> correspondiente.
        /// </summary>
        /// <param name="detalleDto">DTO de detalle de venta proveniente de la capa de presentación.</param>
        /// <returns>La entidad <see cref="DetalleVenta"/> equivalente.</returns>
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
