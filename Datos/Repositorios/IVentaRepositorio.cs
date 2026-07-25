using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public interface IVentaRepositorio
    {
        List<Venta> ObtenerVentas();

        Venta? ObtenerVentaPorId(int id);

        int InsertarVenta(Venta venta);

        int InsertarVenta(Venta venta, MySqlConnection conexion, MySqlTransaction transaccion);

        bool EditarVenta(Venta venta);

        bool EliminarVenta(int id);
    }
}
