using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Datos.DAO;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Implementación de <see cref="IDetalleVentaRepositorio"/> que delega en <see cref="DetalleVentaDAO"/>.
    /// </summary>
    public class DetalleVentaRepositorio : IDetalleVentaRepositorio
    {
        private readonly DetalleVentaDAO detalleVentaDAO = new DetalleVentaDAO();

        /// <summary>
        /// Obtiene todos los detalles (líneas de producto) asociados a una venta.
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean obtener.</param>
        /// <returns>Lista de detalles de la venta (puede estar vacía).</returns>
        public List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId) => detalleVentaDAO.ObtenerDetallesPorVenta(ventaId);

        /// <summary>
        /// Obtiene y bloquea los detalles de una venta dentro de una transacción existente.
        /// </summary>
        public List<DetalleVenta> ObtenerDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
            => detalleVentaDAO.ObtenerDetallesPorVenta(ventaId, conexion, transaccion);

        /// <summary>
        /// Busca un detalle de venta por su identificador.
        /// </summary>
        /// <param name="id">Id del detalle a buscar.</param>
        /// <returns>El detalle encontrado, o <c>null</c> si no existe.</returns>
        public DetalleVenta? ObtenerDetallePorId(int id) => detalleVentaDAO.ObtenerDetallePorId(id);

        /// <summary>
        /// Inserta un nuevo detalle de venta, abriendo su propia conexión.
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool InsertarDetalleVenta(DetalleVenta detalle) => detalleVentaDAO.InsertarDetalleVenta(detalle);

        /// <summary>
        /// Variante transaccional de <see cref="InsertarDetalleVenta(DetalleVenta)"/>, para
        /// usarse dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool InsertarDetalleVenta(DetalleVenta detalle, MySqlConnection conexion, MySqlTransaction transaccion)
            => detalleVentaDAO.InsertarDetalleVenta(detalle, conexion, transaccion);

        /// <summary>
        /// Actualiza los datos de un detalle de venta existente.
        /// </summary>
        /// <param name="detalle">Detalle con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EditarDetalleVenta(DetalleVenta detalle) => detalleVentaDAO.EditarDetalleVenta(detalle);

        /// <summary>
        /// Elimina un detalle de venta por su Id.
        /// </summary>
        /// <param name="id">Id del detalle a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EliminarDetalleVenta(int id) => detalleVentaDAO.EliminarDetalleVenta(id);

        /// <summary>
        /// Elimina todos los detalles asociados a una venta (por ejemplo, al eliminar la venta completa).
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        public bool EliminarDetallesPorVenta(int ventaId) => detalleVentaDAO.EliminarDetallesPorVenta(ventaId);

        /// <summary>
        /// Elimina todos los detalles de una venta dentro de una transacción existente.
        /// </summary>
        public bool EliminarDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
            => detalleVentaDAO.EliminarDetallesPorVenta(ventaId, conexion, transaccion);
    }
}