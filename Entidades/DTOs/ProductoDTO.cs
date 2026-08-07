using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) con los datos de un producto listos para transferirse
    /// entre las capas de Negocio y Presentación, sin propiedades de navegación.
    /// </summary>
    public class ProductoDTO
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
    }
}
