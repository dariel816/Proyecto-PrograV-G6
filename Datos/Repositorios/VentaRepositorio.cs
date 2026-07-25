using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly VentaDAO ventaDAO = new VentaDAO();

        public List<Venta> ObtenerVentas() => ventaDAO.ObtenerVentas();

        public Venta? ObtenerVentaPorId(int id) => ventaDAO.ObtenerVentaPorId(id);

        public int InsertarVenta(Venta venta) => ventaDAO.InsertarVenta(venta);

        public int InsertarVenta(Venta venta, MySqlConnection conexion, MySqlTransaction transaccion)
            => ventaDAO.InsertarVenta(venta, conexion, transaccion);

        public bool EditarVenta(Venta venta) => ventaDAO.EditarVenta(venta);

        public bool EliminarVenta(int id) => ventaDAO.EliminarVenta(id);
    }
}
