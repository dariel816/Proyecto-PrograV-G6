using SistemaVentas.Entidades.DTOs;

namespace SistemadeVentas.Presentacion.Utilidades
{
    /// <summary>
    /// Guarda en memoria el usuario autenticado durante la ejecución de la aplicación
    /// (equivalente a una "sesión" en una app de escritorio de un solo proceso).
    /// </summary>
    public static class SesionActual
    {
        /// <summary>Usuario actualmente autenticado, o <c>null</c> si no hay sesión activa.</summary>
        public static UsuarioDTO? Usuario { get; set; }

        /// <summary>Indica si hay un usuario autenticado en la sesión actual.</summary>
        public static bool HaySesionActiva => Usuario != null;

        /// <summary>
        /// Cierra la sesión actual, olvidando el usuario autenticado.
        /// </summary>
        public static void CerrarSesion()
        {
            Usuario = null;
        }
    }
}
