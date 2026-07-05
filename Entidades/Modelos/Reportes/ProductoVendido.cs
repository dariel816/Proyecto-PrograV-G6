using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.Modelos.Reportes
{
    public class ProductoVendido
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public int CantidadVendida { get; set; }

        public decimal TotalVendido { get; set; }
    }
}
