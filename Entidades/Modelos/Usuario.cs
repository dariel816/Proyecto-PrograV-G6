using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos
{
    /// <summary>
    /// Representa una cuenta de usuario del sistema, usada para autenticarse (login).
    /// </summary>
    public class Usuario
    {
        /// <summary>Identificador único del usuario.</summary>
        public int Id { get; set; }

        /// <summary>Nombre de usuario, único en el sistema.</summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>Hash PBKDF2 (con salt) de la contraseña. Nunca se guarda la contraseña en texto plano.</summary>
        public string ClaveHash { get; set; } = string.Empty;

        /// <summary>Rol del usuario dentro del sistema (por ejemplo, "Administrador" o "Empleado").</summary>
        public string Rol { get; set; } = "Empleado";

        /// <summary>Indica si la cuenta está activa; los usuarios inactivos no pueden iniciar sesión.</summary>
        public bool Activo { get; set; } = true;
    }
}
