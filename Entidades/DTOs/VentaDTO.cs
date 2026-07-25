using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }

        public string ClienteNombre { get; set; }

        public decimal Total { get; set; }

        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }
}
