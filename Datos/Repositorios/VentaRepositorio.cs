using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Implementación de <see cref="IVentaRepositorio"/> que delega en <see cref="VentaDAO"/>.
    /// </summary>
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly VentaDAO ventaDAO = new VentaDAO();

        /// <summary>
        /// Obtiene todas las ventas registradas.
        /// </summary>
        /// <returns>Lista de todas las ventas encontradas (puede estar vacía).</returns>
        public List<Venta> ObtenerVentas() => ventaDAO.ObtenerVentas();

        /// <summary>
        /// Busca una venta por su identificador.
        /// </summary>
        /// <param name="id">Id de la venta a buscar.</param>
        /// <returns>La venta encontrada, o <c>null</c> si no existe.</returns>
        public Venta? ObtenerVentaPorId(int id) => ventaDAO.ObtenerVentaPorId(id);

        /// <summary>
        /// Busca y bloquea una venta dentro de una transacción existente.
        /// </summary>
        public Venta? ObtenerVentaPorId(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
            => ventaDAO.ObtenerVentaPorId(id, conexion, transaccion);

        /// <summary>
        /// Inserta una nueva venta, abriendo su propia conexión.
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        public int InsertarVenta(Venta venta) => ventaDAO.InsertarVenta(venta);

        /// <summary>
        /// Variante transaccional de <see cref="InsertarVenta(Venta)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        public int InsertarVenta(Venta venta, MySqlConnection conexion, MySqlTransaction transaccion)
            => ventaDAO.InsertarVenta(venta, conexion, transaccion);

        /// <summary>
        /// Actualiza los datos de una venta existente.
        /// </summary>
        /// <param name="venta">Venta con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EditarVenta(Venta venta) => ventaDAO.EditarVenta(venta);

        /// <summary>
        /// Elimina una venta por su Id.
        /// </summary>
        /// <param name="id">Id de la venta a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EliminarVenta(int id) => ventaDAO.EliminarVenta(id);

        /// <summary>
        /// Elimina una venta dentro de una transacción existente.
        /// </summary>
        public bool EliminarVenta(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
            => ventaDAO.EliminarVenta(id, conexion, transaccion);
    }
}