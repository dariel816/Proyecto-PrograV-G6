using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }

        public int VentaId { get; set; }

        public int ProductoId { get; set; }

        public string ProductoNombre { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
