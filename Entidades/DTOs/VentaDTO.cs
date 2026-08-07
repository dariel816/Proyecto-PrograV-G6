using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades.DTOs
{
    /// <summary>
    /// DTO (Data Transfer Object) que aplanea los datos de una venta (incluyendo el nombre
    /// del cliente) para transferirse entre las capas de Negocio y Presentación, sin
    /// propiedades de navegación ni depender directamente de la entidad Venta.
    /// </summary>
    public class VentaDTO
    {
        /// <summary>Identificador único de la venta.</summary>
        public int Id { get; set; }

        /// <summary>Fecha en que se realizó la venta.</summary>
        public DateTime Fecha { get; set; }

        /// <summary>Identificador del cliente al que pertenece la venta.</summary>
        public int ClienteId { get; set; }

        /// <summary>Nombre del cliente, incluido para evitar consultas adicionales en la capa de Presentación.</summary>
        public string ClienteNombre { get; set; } = string.Empty;

        /// <summary>Monto total de la venta.</summary>
        public decimal Total { get; set; }

        /// <summary>Lista de detalles (productos y cantidades) que componen la venta.</summary>
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }
}
