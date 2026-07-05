using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos.Reportes
{
    public class ClienteCompra
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; }

        public int CantidadVentas { get; set; }

        public decimal TotalComprado { get; set; }
    }
}
