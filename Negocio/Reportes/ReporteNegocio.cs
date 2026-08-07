using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos.Reportes;

namespace SistemaVentas.Negocio.Reportes
{
    /// <summary>
    /// Genera las agregaciones (mediante LINQ) usadas por los reportes del sistema:
    /// ventas por rango de fechas, ventas por mes, productos más vendidos, productos con
    /// bajo stock y clientes con más compras. No accede directamente a la base de datos,
    /// sino que reutiliza <see cref="VentaNegocio"/>, <see cref="ProductoNegocio"/> y
    /// <see cref="ClienteNegocio"/>.
    /// </summary>
    public class ReporteNegocio
    {
        VentaNegocio ventaNegocio = new VentaNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();
        ClienteNegocio clienteNegocio = new ClienteNegocio();

        /// <summary>
        /// Obtiene las ventas cuya fecha está dentro del rango indicado (inclusive), ordenadas
        /// por fecha ascendente.
        /// </summary>
        /// <param name="desde">Fecha inicial del rango (inclusive).</param>
        /// <param name="hasta">Fecha final del rango (inclusive).</param>
        /// <returns>Lista de ventas dentro del rango de fechas.</returns>
        public List<VentaDTO> ObtenerVentasPorRango(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
            {
                throw new Exception(
                    "La fecha inicial no puede ser mayor que la fecha final.");
            }

            return ventaNegocio.ObtenerVentas()
                .Where(v => v.Fecha.Date >= desde.Date && v.Fecha.Date <= hasta.Date)
                .OrderBy(v => v.Fecha)
                .ToList();
        }

        /// <summary>
        /// Agrupa el total de ventas por periodo (año-mes) para armar el reporte de ventas mensuales.
        /// </summary>
        /// <returns>Lista de totales agrupados por periodo ("yyyy-MM"), ordenada cronológicamente.</returns>
        public List<VentaPorPeriodo> ObtenerVentasPorMes( DateTime desde,DateTime hasta)
        {
            return ObtenerVentasPorRango(desde, hasta)
                .GroupBy(v => v.Fecha.ToString("yyyy-MM"))
                .Select(g => new VentaPorPeriodo
                {
                    Periodo = g.Key,
                    Total = g.Sum(v => v.Total)
                })
                .OrderBy(p => p.Periodo)
                .ToList();
        }

        /// <summary>
        /// Obtiene todos los productos registrados, ordenados alfabéticamente por nombre.
        /// </summary>
        /// <returns>Lista completa de productos.</returns>
        public List<ProductoDTO> ObtenerTodosLosProductos()
        {
            return productoNegocio.ObtenerProductos()
                .OrderBy(p => p.Nombre)
                .ToList();
        }

        /// <summary>
        /// Calcula los productos más vendidos agrupando y sumando las cantidades vendidas
        /// en todos los detalles de todas las ventas.
        /// </summary>
        /// <param name="top">Cantidad máxima de productos a incluir en el resultado (por defecto 5).</param>
        /// <returns>Lista de los productos más vendidos, ordenada por cantidad vendida descendente.</returns>
        public List<ProductoVendido> ObtenerProductosMasVendidos(int top = 5)
        {
            return ventaNegocio.ObtenerVentas()
                .SelectMany(v => v.Detalles)
                .GroupBy(d => new { d.ProductoId, d.ProductoNombre })
                .Select(g => new ProductoVendido
                {
                    ProductoId = g.Key.ProductoId,
                    Nombre = g.Key.ProductoNombre,
                    CantidadVendida = g.Sum(d => d.Cantidad),
                    TotalVendido = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(p => p.CantidadVendida)
                .Take(top)
                .ToList();
        }

        /// <summary>
        /// Obtiene los productos cuyo stock está por debajo (o igual) del umbral indicado.
        /// </summary>
        /// <param name="umbral">Cantidad de stock límite para considerar un producto con bajo stock (por defecto 5).</param>
        /// <returns>Lista de productos con bajo stock, ordenada por stock ascendente.</returns>
        public List<ProductoDTO> ObtenerProductosBajoStock(int umbral = 5)
        {
            return productoNegocio.ObtenerProductos()
                .Where(p => p.Stock <= umbral)
                .OrderBy(p => p.Stock)
                .ToList();
        }

        /// <summary>
        /// Calcula los clientes con más compras, agrupando las ventas por cliente y sumando
        /// la cantidad de ventas y el monto total comprado.
        /// </summary>
        /// <param name="top">Cantidad máxima de clientes a incluir en el resultado (por defecto 5).</param>
        /// <returns>Lista de los clientes con más compras, ordenada por total comprado descendente.</returns>
        public List<ClienteCompra> ObtenerClientesConMasCompras(int top = 5)
        {
            return ventaNegocio.ObtenerVentas()
                .GroupBy(v => new { v.ClienteId, v.ClienteNombre })
                .Select(g => new ClienteCompra
                {
                    ClienteId = g.Key.ClienteId,
                    Nombre = g.Key.ClienteNombre,
                    CantidadVentas = g.Count(),
                    TotalComprado = g.Sum(v => v.Total)
                })
                .OrderByDescending(c => c.TotalComprado)
                .Take(top)
                .ToList();
        }

        /// <summary>
        /// Obtiene todos los clientes registrados junto con la cantidad
        /// de ventas y el total comprado por cada uno.
        /// </summary>
        public List<ClienteCompra> ObtenerTodosLosClientesConCompras()
        {
            var ventas = ventaNegocio.ObtenerVentas();

            return clienteNegocio.ObtenerClientes()
                .GroupJoin(
                    ventas,
                    cliente => cliente.Id,
                    venta => venta.ClienteId,
                    (cliente, ventasCliente) => new ClienteCompra
                    {
                        ClienteId = cliente.Id,
                        Nombre = cliente.Nombre,
                        CantidadVentas = ventasCliente.Count(),
                        TotalComprado = ventasCliente.Sum(
                            venta => venta.Total)
                    })
                .OrderBy(cliente => cliente.Nombre)
                .ToList();
        }
    }
}
