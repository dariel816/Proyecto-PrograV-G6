using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos.Reportes
{
    /// <summary>
    /// Resultado de una agregación de ventas por período (por ejemplo, por día, mes o año),
    /// usado por el módulo de Reportes para mostrar la evolución de las ventas en el tiempo.
    /// </summary>
    public class VentaPorPeriodo
    {
        /// <summary>Texto que identifica el período agregado (por ejemplo, "2026-08" o "Agosto 2026").</summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>Monto total de ventas correspondiente al período.</summary>
        public decimal Total { get; set; }
    }
}
