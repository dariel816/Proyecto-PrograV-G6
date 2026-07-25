using System.Collections.Generic;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public interface IClienteRepositorio
    {
        List<Cliente> ObtenerTodos();

        Cliente? ObtenerPorId(int id);

        bool Crear(Cliente cliente);

        bool Actualizar(Cliente cliente);

        bool Eliminar(int id);

        bool ExisteCorreo(string correo, int? excludeId = null);

        bool ExisteTelefono(string telefono, int? excludeId = null);
    }
}
