using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Negocio
{
    public class ClienteNegocio
    {
        private string _connectionString = "server=localhost;database=sistema_ventas;user=root;password=root123;";
        ClienteDAO clienteDAO;

        public ClienteNegocio()
        {
            clienteDAO = new ClienteDAO(_connectionString);
        }

        public List<Cliente> ObtenerClientes()
        {
            return clienteDAO.ObtenerTodos();
        }

        public Cliente? ObtenerClientePorId(int id)
        {
            return clienteDAO.ObtenerPorId(id);
        }

        public bool InsertarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception("El nombre del cliente es requerido.");

            return clienteDAO.Crear(cliente);
        }

        public bool EditarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception("El nombre del cliente es requerido.");

            return clienteDAO.Actualizar(cliente);
        }

        public bool EliminarCliente(int id)
        {
            return clienteDAO.Eliminar(id);
        }
    }
}
