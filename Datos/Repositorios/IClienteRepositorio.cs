using System.Collections.Generic;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Abstracción sobre el acceso a datos de Cliente, usada por la capa de
    /// Negocio para no depender directamente del DAO concreto (<see cref="SistemaVentas.Datos.DAO.ClienteDAO"/>).
    /// </summary>
    public interface IClienteRepositorio
    {
        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de todos los clientes encontrados (puede estar vacía).</returns>
        List<Cliente> ObtenerTodos();

        /// <summary>
        /// Busca un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente a buscar.</param>
        /// <returns>El cliente encontrado, o <c>null</c> si no existe.</returns>
        Cliente? ObtenerPorId(int id);

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="cliente">Datos del cliente a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool Crear(Cliente cliente);

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="cliente">Cliente con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool Actualizar(Cliente cliente);

        /// <summary>
        /// Elimina un cliente por su Id.
        /// </summary>
        /// <param name="id">Id del cliente a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool Eliminar(int id);

        /// <summary>   
        /// Verifica si un cliente tiene ventas asociadas
        /// </summary>
        /// <param name="id">Id del cliente a verificar.</param>
        /// <returns><c>true</c> si el cliente tiene ventas asociadas.</returns>  
        /// 

        bool TieneVentas(int id);

        /// <summary>
        /// Verifica si ya existe un cliente con ese correo.
        /// </summary>
        /// <param name="correo">Correo a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese correo.</returns>
        /// 


        bool ExisteCorreo(string correo, int? excludeId = null);

        /// <summary>
        /// Verifica si ya existe un cliente con ese teléfono.
        /// </summary>
        /// <param name="telefono">Teléfono a verificar.</param>
        /// <param name="excludeId">Id de cliente a excluir de la búsqueda (opcional, útil al actualizar).</param>
        /// <returns><c>true</c> si ya existe un cliente con ese teléfono.</returns>
        bool ExisteTelefono(string telefono, int? excludeId = null);
    }
}
