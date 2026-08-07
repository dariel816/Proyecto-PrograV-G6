using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos.Reportes
{
    /// <summary>
    /// Resultado de una agregación de ventas por producto, usado por el módulo de Reportes
    /// para mostrar cuánto se ha vendido de cada producto.
    /// </summary>
    public class ProductoVendido
    {
        /// <summary>Identificador del producto.</summary>
        public int ProductoId { get; set; }

        /// <summary>Nombre del producto.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Cantidad total de unidades vendidas del producto.</summary>
        public int CantidadVendida { get; set; }

        /// <summary>Monto total generado por la venta del producto.</summary>
        public decimal TotalVendido { get; set; }
    }
}
