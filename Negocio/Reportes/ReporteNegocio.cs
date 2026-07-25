using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos.Reportes;

namespace SistemaVentas.Negocio.Reportes
{
    public class ReporteNegocio
    {
        VentaNegocio ventaNegocio = new VentaNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();
        ClienteNegocio clienteNegocio = new ClienteNegocio();

        public List<VentaDTO> ObtenerVentasPorRango(DateTime desde, DateTime hasta)
        {
            return ventaNegocio.ObtenerVentas()
                .Where(v => v.Fecha.Date >= desde.Date && v.Fecha.Date <= hasta.Date)
                .OrderBy(v => v.Fecha)
                .ToList();
        }

        public List<VentaPorPeriodo> ObtenerVentasPorMes()
        {
            return ventaNegocio.ObtenerVentas()
                .GroupBy(v => v.Fecha.ToString("yyyy-MM"))
                .Select(g => new VentaPorPeriodo
                {
                    Periodo = g.Key,
                    Total = g.Sum(v => v.Total)
                })
                .OrderBy(p => p.Periodo)
                .ToList();
        }

        public List<ProductoDTO> ObtenerTodosLosProductos()
        {
            return productoNegocio.ObtenerProductos()
                .OrderBy(p => p.Nombre)
                .ToList();
        }

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

        public List<ProductoDTO> ObtenerProductosBajoStock(int umbral = 5)
        {
            return productoNegocio.ObtenerProductos()
                .Where(p => p.Stock <= umbral)
                .OrderBy(p => p.Stock)
                .ToList();
        }

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
    }
}
