using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) con los datos de un cliente listos para transferirse
    /// entre las capas de Negocio y Presentación, sin propiedades de navegación.
    /// </summary>
    public class ClienteDTO
    {
        /// <summary>Identificador único del cliente.</summary>
        public int Id { get; set; }

        /// <summary>Nombre completo del cliente.</summary>
        public string Nombre { get; set; }

        /// <summary>Número de teléfono de contacto del cliente.</summary>
        public string Telefono { get; set; }

        /// <summary>Correo electrónico de contacto del cliente.</summary>
        public string Correo { get; set; }
    }
}
