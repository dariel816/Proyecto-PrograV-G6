using System.Collections.Generic;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Implementación de <see cref="IUsuarioRepositorio"/> que delega en <see cref="UsuarioDAO"/>.
    /// </summary>
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UsuarioDAO usuarioDAO;

        /// <summary>
        /// Crea el repositorio indicando la cadena de conexión a usar en cada operación.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos MySQL.</param>
        public UsuarioRepositorio(string connectionString)
        {
            usuarioDAO = new UsuarioDAO(connectionString);
        }

        /// <inheritdoc/>
        public Usuario? ObtenerPorNombreUsuario(string nombreUsuario) => usuarioDAO.ObtenerPorNombreUsuario(nombreUsuario);

        /// <inheritdoc/>
        public List<Usuario> ObtenerTodos() => usuarioDAO.ObtenerTodos();

        /// <inheritdoc/>
        public bool Crear(Usuario usuario) => usuarioDAO.Crear(usuario);

        /// <inheritdoc/>
        public bool ExisteNombreUsuario(string nombreUsuario) => usuarioDAO.ExisteNombreUsuario(nombreUsuario);
    }
}
