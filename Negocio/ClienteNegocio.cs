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
    public class ClienteNegocio
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteNegocio()
        {
            clienteRepositorio = RepositorioFactory.CrearClienteRepositorio();
        }

        public List<ClienteDTO> ObtenerClientes()
        {
            return clienteRepositorio.ObtenerTodos().Select(ADto).ToList();
        }

        public ClienteDTO? ObtenerClientePorId(int id)
        {
            var cliente = clienteRepositorio.ObtenerPorId(id);
            return cliente == null ? null : ADto(cliente);
        }

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

        public bool EliminarCliente(int id)
        {
            return clienteRepositorio.Eliminar(id);
        }

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
