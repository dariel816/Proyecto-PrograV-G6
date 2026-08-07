using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SistemaVentas.Entidades.Modelos;

namespace SistemaVentas.Datos.Repositorios
{
    /// <summary>
    /// Abstracción sobre el acceso a datos de Venta, usada por la capa de
    /// Negocio para no depender directamente del DAO concreto (<see cref="SistemaVentas.Datos.DAO.VentaDAO"/>).
    /// </summary>
    public interface IVentaRepositorio
    {
        /// <summary>
        /// Obtiene todas las ventas registradas.
        /// </summary>
        /// <returns>Lista de todas las ventas encontradas (puede estar vacía).</returns>
        List<Venta> ObtenerVentas();

        /// <summary>
        /// Busca una venta por su identificador.
        /// </summary>
        /// <param name="id">Id de la venta a buscar.</param>
        /// <returns>La venta encontrada, o <c>null</c> si no existe.</returns>
        Venta? ObtenerVentaPorId(int id);

        /// <summary>
        /// Busca y bloquea una venta dentro de una transacción existente.
        /// </summary>
        Venta? ObtenerVentaPorId(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion);

        /// <summary>
        /// Inserta una nueva venta, abriendo su propia conexión.
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        int InsertarVenta(Venta venta);

        /// <summary>
        /// Variante transaccional de <see cref="InsertarVenta(Venta)"/>, para usarse
        /// dentro de una transacción ya existente (por ejemplo, la de <c>VentaNegocio.CrearVenta</c>).
        /// </summary>
        /// <param name="venta">Datos de la venta a insertar.</param>
        /// <param name="conexion">Conexión MySQL abierta de la transacción en curso.</param>
        /// <param name="transaccion">Transacción MySQL en curso.</param>
        /// <returns>El Id autogenerado de la venta insertada.</returns>
        int InsertarVenta(Venta venta, MySqlConnection conexion, MySqlTransaction transaccion);

        /// <summary>
        /// Actualiza los datos de una venta existente.
        /// </summary>
        /// <param name="venta">Venta con los datos actualizados (incluye el Id).</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EditarVenta(Venta venta);

        /// <summary>
        /// Elimina una venta por su Id.
        /// </summary>
        /// <param name="id">Id de la venta a eliminar.</param>
        /// <returns><c>true</c> si la operación fue exitosa.</returns>
        bool EliminarVenta(int id);

        /// <summary>
        /// Elimina una venta dentro de una transacción existente.
        /// </summary>
        bool EliminarVenta(
            int id,
            MySqlConnection conexion,
            MySqlTransaction transaccion);
    }
}