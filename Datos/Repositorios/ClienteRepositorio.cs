using System.Collections.Generic;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Implementación de <see cref="IClienteRepositorio"/> que delega en <see cref="ClienteDAO"/>.
    /// </summary>
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ClienteDAO clienteDAO;

        /// <summary>
        /// Crea el repositorio, instanciando el DAO con la cadena de conexión indicada.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos MySQL.</param>
        public ClienteRepositorio(string connectionString)
        {
            clienteDAO = new ClienteDAO(connectionString);
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de todos los clientes encontrados (puede estar vacía).</returns>
        public List<Cliente> ObtenerTodos() => clienteDAO.ObtenerTodos();

        /// <summary>
        /// Busca un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente a buscar.</param>
        /// <returns>El cliente encontrado, o <c>null</c> si no existe.</returns>
        public Cliente? ObtenerPorId(int id) => clienteDAO.ObtenerPorId(id);

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Datos del cliente a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool Crear(Cliente cliente) => clienteDAO.Crear(cliente);

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="cliente">Cliente con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool Actualizar(Cliente cliente) => clienteDAO.Actualizar(cliente);

        /// <summary>
        /// Elimina un cliente por su Id.
        /// </summary>
        /// <param name="id">Id del cliente a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool Eliminar(int id) => clienteDAO.Eliminar(id);

        /// <summary>   
        /// Verifica si un cliente tiene ventas asociadas
        /// </summary>
        /// <param name="id">Id del cliente a verificar.</param>
        /// <returns><c>true</c> si el cliente tiene ventas asociadas.</returns>  
        /// 

        public bool TieneVentas(int id) => clienteDAO.TieneVentas(id);

        /// <summary>
        /// Verifica si ya existe un cliente con ese correo.
        /// </summary>
        /// <param name="correo">Correo a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese correo.</returns>
        public bool ExisteCorreo(string correo, int? excludeId = null) => clienteDAO.ExisteCorreo(correo, excludeId);

        /// <summary>
        /// Verifica si ya existe un cliente con ese teléfono.
        /// </summary>
        /// <param name="telefono">Teléfono a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese teléfono.</returns>
        public bool ExisteTelefono(string telefono, int? excludeId = null) => clienteDAO.ExisteTelefono(telefono, excludeId);
    }
}
