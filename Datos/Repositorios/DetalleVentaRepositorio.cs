using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public class DetalleVentaRepositorio : IDetalleVentaRepositorio
    {
        private readonly DetalleVentaDAO detalleVentaDAO = new DetalleVentaDAO();

        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId) => detalleVentaDAO.ObtenerDetallesPorVenta(ventaId);

        public DetalleVenta? ObtenerDetallePorId(int id) => detalleVentaDAO.ObtenerDetallePorId(id);

        public bool InsertarDetalleVenta(DetalleVenta detalle) => detalleVentaDAO.InsertarDetalleVenta(detalle);

        public bool InsertarDetalleVenta(DetalleVenta detalle, MySqlConnection conexion, MySqlTransaction transaccion)
            => detalleVentaDAO.InsertarDetalleVenta(detalle, conexion, transaccion);

        public bool EditarDetalleVenta(DetalleVenta detalle) => detalleVentaDAO.EditarDetalleVenta(detalle);

        public bool EliminarDetalleVenta(int id) => detalleVentaDAO.EliminarDetalleVenta(id);

        public bool EliminarDetallesPorVenta(int ventaId) => detalleVentaDAO.EliminarDetallesPorVenta(ventaId);
    }
}
