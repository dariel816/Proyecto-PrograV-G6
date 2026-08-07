using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Abstracción sobre el acceso a datos de DetalleVenta, usada por la capa de
    /// Negocio para no depender directamente del DAO concreto (<see cref="SistemaVentas.Datos.DAO.DetalleVentaDAO"/>).
    /// </summary>
    public interface IDetalleVentaRepositorio
    {
        /// <summary>
        /// Obtiene todos los detalles (líneas de producto) asociados a una venta.
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean obtener.</param>
        /// <returns>Lista de detalles de la venta (puede estar vacía).</returns>
        List<DetalleVenta> ObtenerDetallesPorVenta(int ventaId);

        /// <summary>
        /// Obtiene y bloquea los detalles de una venta dentro de una transacción existente.
        /// </summary>
        List<DetalleVenta> ObtenerDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion);

        /// <summary>
        /// Busca un detalle de venta por su identificador.
        /// </summary>
        /// <param name="id">Id del detalle a buscar.</param>
        /// <returns>El detalle encontrado, o <c>null</c> si no existe.</returns>
        DetalleVenta? ObtenerDetallePorId(int id);

        /// <summary>
        /// Inserta un nuevo detalle de venta, abriendo su propia conexión.
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool InsertarDetalleVenta(DetalleVenta detalle);

        /// <summary>
        /// Variante transaccional de <see cref="InsertarDetalleVenta(DetalleVenta)"/>, para
        /// usarse dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="detalle">Datos del detalle de venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool InsertarDetalleVenta(DetalleVenta detalle, MySqlConnection conexion, MySqlTransaction transaccion);

        /// <summary>
        /// Actualiza los datos de un detalle de venta existente.
        /// </summary>
        /// <param name="detalle">Detalle con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EditarDetalleVenta(DetalleVenta detalle);

        /// <summary>
        /// Elimina un detalle de venta por su Id.
        /// </summary>
        /// <param name="id">Id del detalle a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EliminarDetalleVenta(int id);

        /// <summary>
        /// Elimina todos los detalles asociados a una venta (por ejemplo, al eliminar la venta completa).
        /// </summary>
        /// <param name="ventaId">Id de la venta cuyos detalles se desean eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EliminarDetallesPorVenta(int ventaId);

        /// <summary>
        /// Elimina todos los detalles de una venta dentro de una transacción existente.
        /// </summary>
        bool EliminarDetallesPorVenta(
            int ventaId,
            MySqlConnection conexion,
            MySqlTransaction transaccion);
    }
}