using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.Fabricas;
using SistemaVentas.Datos.Repositorios;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    /// <summary>
    /// Reglas de negocio y validaciones para la gestión de clientes.
    /// Actúa como intermediario entre la capa de presentación y el repositorio de clientes,
    /// obtenido mediante <see cref="RepositorioFactory"/>, trabajando con <see cref="ClienteDTO"/>.
    /// </summary>
    public class ClienteNegocio
    {
        private readonly IClienteRepositorio clienteRepositorio;

        /// <summary>
        /// Crea una nueva instancia de <see cref="ClienteNegocio"/> y obtiene el repositorio
        /// de clientes a través de la fábrica de repositorios.
        /// </summary>
        public ClienteNegocio()
        {
            clienteRepositorio = RepositorioFactory.CrearClienteRepositorio();
        }

        /// <summary>
        /// Obtiene la lista completa de clientes registrados.
        /// </summary>
        /// <returns>Lista de clientes en formato <see cref="ClienteDTO"/>.</returns>
        public List<ClienteDTO> ObtenerClientes()
        {
            return clienteRepositorio.ObtenerTodos().Select(ADto).ToList();
        }

        /// <summary>
        /// Busca un cliente por su identificador.
        /// </summary>
        /// <param name="id">Identificador del cliente.</param>
        /// <returns>El <see cref="ClienteDTO"/> encontrado, o <c>null</c> si no existe.</returns>
        public ClienteDTO? ObtenerClientePorId(int id)
        {
            var cliente = clienteRepositorio.ObtenerPorId(id);
            return cliente == null ? null : ADto(cliente);
        }

        /// <summary>
        /// Valida y registra un nuevo cliente, verificando que el nombre no esté vacío
        /// y que el correo y el teléfono no estén ya registrados por otro cliente.
        /// </summary>
        /// <param name="clienteDto">Datos del cliente a insertar.</param>
        /// <returns><c>true</c> si el cliente fue insertado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool InsertarCliente(ClienteDTO clienteDto)
        {
            if (string.IsNullOrWhiteSpace(clienteDto.Nombre))
                throw new Exception("El nombre del cliente es requerido.");

            // Validaciones de unicidad
            if (clienteRepositorio.ExisteCorreo(clienteDto.Correo, null))
                throw new Exception("El correo electrónico ya está registrado.");

            if (clienteRepositorio.ExisteTelefono(clienteDto.Telefono, null))
                throw new Exception("El teléfono ya está registrado.");

            return clienteRepositorio.Crear(AEntidad(clienteDto));
        }

        /// <summary>
        /// Valida y actualiza los datos de un cliente existente, verificando que el nombre
        /// no esté vacío y que el correo y el teléfono no estén registrados por otro cliente.
        /// </summary>
        /// <param name="clienteDto">Datos actualizados del cliente.</param>
        /// <returns><c>true</c> si el cliente fue actualizado correctamente.</returns>
        /// <exception cref="Exception">Se lanza cuando alguna validación de negocio falla.</exception>
        public bool EditarCliente(ClienteDTO clienteDto)
        {
            if (string.IsNullOrWhiteSpace(clienteDto.Nombre))
                throw new Exception("El nombre del cliente es requerido.");

            // Validaciones de unicidad (excluir el propio registro)
            if (clienteRepositorio.ExisteCorreo(clienteDto.Correo, clienteDto.Id))
                throw new Exception("El correo electrónico ya está registrado por otro cliente.");

            if (clienteRepositorio.ExisteTelefono(clienteDto.Telefono, clienteDto.Id))
                throw new Exception("El teléfono ya está registrado por otro cliente.");

            return clienteRepositorio.Actualizar(AEntidad(clienteDto));
        }

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="id">Identificador del cliente a eliminar.</param>
        /// <returns><c>true</c> si el cliente fue eliminado correctamente.</returns>
        public bool EliminarCliente(int id)
        {
            return clienteRepositorio.Eliminar(id);
        }

        /// <summary>
        /// Función de mapeo: convierte una entidad <see cref="Cliente"/> en su <see cref="ClienteDTO"/> correspondiente.
        /// </summary>
        /// <param name="cliente">Entidad de cliente proveniente del repositorio.</param>
        /// <returns>El <see cref="ClienteDTO"/> equivalente.</returns>
        private static ClienteDTO ADto(Cliente cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono
            };
        }

        /// <summary>
        /// Función de mapeo: convierte un <see cref="ClienteDTO"/> en la entidad <see cref="Cliente"/> correspondiente.
        /// </summary>
        /// <param name="clienteDto">DTO de cliente proveniente de la capa de presentación.</param>
        /// <returns>La entidad <see cref="Cliente"/> equivalente.</returns>
        private static Cliente AEntidad(ClienteDTO clienteDto)
        {
            return new Cliente
            {
                Id = clienteDto.Id,
                Nombre = clienteDto.Nombre,
                Correo = clienteDto.Correo,
                Telefono = clienteDto.Telefono
            };
        }
    }
}
