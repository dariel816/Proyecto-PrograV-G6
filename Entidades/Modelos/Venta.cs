using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos
{
    /// <summary>
    /// Representa una venta registrada en el sistema, junto con sus detalles.
    /// </summary>
    public class Venta
    {
        /// <summary>Identificador único de la venta.</summary>
        public int Id { get; set; }

        /// <summary>Fecha en que se realizó la venta.</summary>
        public DateTime Fecha { get; set; }

        /// <summary>Identificador del cliente al que pertenece la venta.</summary>
        public int ClienteId { get; set; }

        /// <summary>Monto total de la venta.</summary>
        public decimal Total { get; set; }

        /// <summary>Cliente asociado a la venta (propiedad de navegación).</summary>
        public Cliente? Cliente { get; set; }

        /// <summary>Lista de detalles (productos y cantidades) que componen la venta.</summary>
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
