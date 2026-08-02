using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que aplanea los datos de un detalle de venta (incluyendo
    /// el nombre del producto) para transferirse entre las capas de Negocio y Presentación,
    /// sin propiedades de navegación ni depender directamente de la entidad DetalleVenta.
    /// </summary>
    public class DetalleVentaDTO
    {
        /// <summary>Identificador único del detalle de venta.</summary>
        public int Id { get; set; }

        /// <summary>Identificador de la venta a la que pertenece este detalle.</summary>
        public int VentaId { get; set; }

        /// <summary>Identificador del producto vendido en este detalle.</summary>
        public int ProductoId { get; set; }

        /// <summary>Nombre del producto, incluido para evitar consultas adicionales en la capa de Presentación.</summary>
        public string ProductoNombre { get; set; }

        /// <summary>Cantidad de unidades vendidas del producto.</summary>
        public int Cantidad { get; set; }

        /// <summary>Precio unitario del producto al momento de la venta.</summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>Subtotal correspondiente a este detalle (cantidad por precio unitario).</summary>
        public decimal Subtotal { get; set; }
    }
}
