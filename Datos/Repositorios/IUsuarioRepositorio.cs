using System.Collections.Generic;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Abstracción sobre el acceso a datos de Usuario, usada por la capa de Negocio para no
    /// depender directamente del DAO concreto (<see cref="SistemaVentas.Datos.DAO.UsuarioDAO"/>).
    /// </summary>
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Busca un usuario activo por su nombre de usuario.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a buscar.</param>
        /// <returns>El usuario encontrado, o <c>null</c> si no existe.</returns>
        Usuario? ObtenerPorNombreUsuario(string nombreUsuario);

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        /// <returns>Lista de todos los usuarios encontrados (puede estar vacía).</returns>
        List<Usuario> ObtenerTodos();

        /// <summary>
        /// Inserta un nuevo usuario.
        /// </summary>
        /// <param name="usuario">Datos del usuario a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool Crear(Usuario usuario);

        /// <summary>
        /// Verifica si ya existe un nombre de usuario registrado.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a verificar.</param>
        /// <returns><c>true</c> si ya existe un usuario con ese nombre.</returns>
        bool ExisteNombreUsuario(string nombreUsuario);
    }
}
