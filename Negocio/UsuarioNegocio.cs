using System;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;
using SistemaVentas.Negocio.Seguridad;

namespace SistemaVentas.Negocio
{
    /// <summary>
    /// Reglas de negocio para la autenticación y creación de usuarios. Obtiene el repositorio
    /// de usuarios mediante <see cref="RepositorioFactory"/> y nunca maneja contraseñas en
    /// texto plano fuera de <see cref="PasswordHasher"/>.
    /// </summary>
    public class UsuarioNegocio
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        /// <summary>
        /// Crea una nueva instancia de <see cref="UsuarioNegocio"/> y obtiene el repositorio
        /// de usuarios a través de la fábrica de repositorios.
        /// </summary>
        public UsuarioNegocio()
        {
            usuarioRepositorio = RepositorioFactory.CrearUsuarioRepositorio();
        }

        /// <summary>
        /// Valida las credenciales de inicio de sesión. No revela cuál fue el motivo del fallo
        /// (usuario inexistente, inactivo o contraseña incorrecta) para no dar pistas a quien
        /// intenta adivinar credenciales.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario ingresado.</param>
        /// <param name="clave">Contraseña en texto plano ingresada.</param>
        /// <returns>El <see cref="UsuarioDTO"/> autenticado, o <c>null</c> si las credenciales no son válidas.</returns>
        public UsuarioDTO? ValidarLogin(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
                return null;

            Usuario? usuario = usuarioRepositorio.ObtenerPorNombreUsuario(nombreUsuario.Trim());

            if (usuario == null || !usuario.Activo)
                return null;

            if (!PasswordHasher.VerificarClave(clave, usuario.ClaveHash))
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol
            };
        }

        /// <summary>
        /// Valida y crea un nuevo usuario, hasheando la contraseña antes de guardarla. Útil
        /// para un futuro módulo de administración de usuarios.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a registrar.</param>
        /// <param name="clave">Contraseña en texto plano (se hashea antes de guardarse).</param>
        /// <param name="rol">Rol asignado al nuevo usuario (por defecto "Empleado").</param>
        /// <returns><c>true</c> si el usuario fue creado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool CrearUsuario(string nombreUsuario, string clave, string rol = "Empleado")
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new Exception("El nombre de usuario es requerido.");

            if (string.IsNullOrWhiteSpace(clave) || clave.Length < 6)
                throw new Exception("La contraseña debe tener al menos 6 caracteres.");

            if (usuarioRepositorio.ExisteNombreUsuario(nombreUsuario.Trim()))
                throw new Exception("El nombre de usuario ya está registrado.");

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario.Trim(),
                ClaveHash = PasswordHasher.HashearClave(clave),
                Rol = rol,
                Activo = true
            };

            return usuarioRepositorio.Crear(usuario);
        }
    }
}
