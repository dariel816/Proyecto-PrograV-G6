using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos
{
    /// <summary>
    /// Representa el detalle de una venta: un producto específico, su cantidad y su subtotal dentro de una venta.
    /// </summary>
    public class DetalleVenta
    {
        /// <summary>Identificador único del detalle de venta.</summary>
        public int Id { get; set; }

        /// <summary>Identificador de la venta a la que pertenece este detalle.</summary>
        public int VentaId { get; set; }

        /// <summary>Identificador del producto vendido en este detalle.</summary>
        public int ProductoId { get; set; }

        /// <summary>Cantidad de unidades vendidas del producto.</summary>
        public int Cantidad { get; set; }

        /// <summary>Precio unitario del producto al momento de la venta.</summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>Subtotal correspondiente a este detalle (cantidad por precio unitario).</summary>
        public decimal Subtotal { get; set; }

        /// <summary>Venta a la que pertenece este detalle (propiedad de navegación).</summary>
        public Venta? Venta { get; set; }

        /// <summary>Producto vendido en este detalle (propiedad de navegación).</summary>
        public Producto? Producto { get; set; }
    }
}
