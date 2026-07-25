using System.Collections.Generic;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ClienteDAO clienteDAO;

        public ClienteRepositorio(string connectionString)
        {
            clienteDAO = new ClienteDAO(connectionString);
        }

        public List<Cliente> ObtenerTodos() => clienteDAO.ObtenerTodos();

        public Cliente? ObtenerPorId(int id) => clienteDAO.ObtenerPorId(id);

        public bool Crear(Cliente cliente) => clienteDAO.Crear(cliente);

        public bool Actualizar(Cliente cliente) => clienteDAO.Actualizar(cliente);

        public bool Eliminar(int id) => clienteDAO.Eliminar(id);

        public bool ExisteCorreo(string correo, int? excludeId = null) => clienteDAO.ExisteCorreo(correo, excludeId);

        public bool ExisteTelefono(string telefono, int? excludeId = null) => clienteDAO.ExisteTelefono(telefono, excludeId);
    }
}
