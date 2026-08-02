using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos.Reportes
{
    /// <summary>
    /// Resultado de una agregación de ventas por cliente, usado por el módulo de Reportes
    /// para mostrar cuánto ha comprado cada cliente.
    /// </summary>
    public class ClienteCompra
    {
        /// <summary>Identificador del cliente.</summary>
        public int ClienteId { get; set; }

        /// <summary>Nombre del cliente.</summary>
        public string Nombre { get; set; }

        /// <summary>Cantidad total de ventas realizadas al cliente.</summary>
        public int CantidadVentas { get; set; }

        /// <summary>Monto total comprado por el cliente.</summary>
        public decimal TotalComprado { get; set; }
    }
}
