using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SistemaVentas.Entidades.Modelos
{
    /// <summary>
    /// Representa un cliente registrado en el sistema.
    /// </summary>
    public class Cliente
    {
        /// <summary>Identificador único del cliente.</summary>
        public int Id { get; set; }

        /// <summary>Nombre completo del cliente.</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Número de teléfono de contacto del cliente.</summary>
        public string Telefono { get; set; } = string.Empty;

        /// <summary>Correo electrónico de contacto del cliente.</summary>
        public string Correo { get; set; } = string.Empty;
    }
}
