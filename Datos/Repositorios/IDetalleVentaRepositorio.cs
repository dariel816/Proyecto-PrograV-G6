using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public interface IDetalleVentaRepositorio
    {
        List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId);

        DetalleVenta? ObtenerDetallePorId(int id);

        bool InsertarDetalleVenta(DetalleVenta detalle);

        bool InsertarDetalleVenta(DetalleVenta detalle, MySqlConnection conexion, MySqlTransaction transaccion);

        bool EditarDetalleVenta(DetalleVenta detalle);

        bool EliminarDetalleVenta(int id);

        bool EliminarDetallesPorVenta(int ventaId);
    }
}
