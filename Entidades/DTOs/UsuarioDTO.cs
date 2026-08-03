using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que representa al usuario ya autenticado. A propósito NO
    /// incluye la contraseña ni su hash: esto es lo único que debe viajar hacia la capa de
    /// Presentación tras un login exitoso.
    /// </summary>
    public class UsuarioDTO
    {
        /// <summary>Identificador único del usuario.</summary>
        public int Id { get; set; }

        /// <summary>Nombre de usuario con el que inició sesión.</summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>Rol del usuario autenticado.</summary>
        public string Rol { get; set; } = string.Empty;
    }
}
