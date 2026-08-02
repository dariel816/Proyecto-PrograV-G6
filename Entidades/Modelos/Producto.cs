using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos
{
    /// <summary>
    /// Representa un producto disponible para la venta en el sistema.
    /// </summary>
    public class Producto
    {
        /// <summary>Identificador único del producto.</summary>
        public int Id { get; set; }
        /// <summary>Código interno que identifica al producto.</summary>
        public string Codigo { get; set; } = string.Empty;
        /// <summary>Nombre del producto.</summary>
        public string Nombre { get; set; } = string.Empty;
        /// <summary>Descripción detallada del producto.</summary>
        public string Descripcion { get; set; } = string.Empty;
        /// <summary>Precio unitario de venta del producto.</summary>
        public decimal Precio { get; set; }
        /// <summary>Cantidad de unidades disponibles en inventario.</summary>
        public int Stock { get; set; }

        /// <summary>Devuelve el nombre del producto como representación en texto.</summary>
        public override string ToString()
        {
            return Nombre;
        }
    }
}
