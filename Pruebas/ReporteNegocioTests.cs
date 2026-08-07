using System;
using SistemaVentas.Negocio.Reportes;

namespace SistemaVentas.Pruebas
{
    [TestClass]
    public class ReporteNegocioTests
    {
        private readonly ReporteNegocio reporteNegocio = new ReporteNegocio();

        [TestMethod]
        public void ObtenerProductosBajoStock_TodosLosResultados_CumplenElUmbral()
        {
            int umbral = 5;
            var resultado = reporteNegocio.ObtenerProductosBajoStock(umbral);

            foreach (var producto in resultado)
            {
                Assert.IsLessThanOrEqualTo(umbral, producto.Stock);
            }
        }

        [TestMethod]
        public void ObtenerProductosMasVendidos_QuedaOrdenadoDescendentementePorCantidad()
        {
            var resultado = reporteNegocio.ObtenerProductosMasVendidos(5);

            for (int i = 1; i < resultado.Count; i++)
            {
                Assert.IsGreaterThanOrEqualTo( resultado[i].CantidadVendida,resultado[i - 1].CantidadVendida);
            }
        }

        [TestMethod]
        public void ObtenerClientesConMasCompras_QuedaOrdenadoDescendentementePorTotal()
        {
            var resultado = reporteNegocio.ObtenerClientesConMasCompras(5);

            for (int i = 1; i < resultado.Count; i++)
            {
                Assert.IsGreaterThanOrEqualTo(resultado[i].TotalComprado,resultado[i - 1].TotalComprado);
            }
        }

        [TestMethod]
        public void ObtenerVentasPorRango_RangoSinVentas_RetornaListaVacia()
        {
            var desde = new DateTime(1990, 1, 1);
            var hasta = new DateTime(1990, 1, 31);

            var resultado = reporteNegocio.ObtenerVentasPorRango(desde, hasta);

            Assert.IsEmpty(resultado);
        }

        [TestMethod]
        public void ObtenerVentasPorRango_TodasLasVentasCaenDentroDelRangoSolicitado()
        {
            var desde = DateTime.Now.AddYears(-10);
            var hasta = DateTime.Now.AddDays(1);

            var resultado = reporteNegocio.ObtenerVentasPorRango(desde, hasta);

            foreach (var venta in resultado)
            {
                Assert.IsTrue(venta.Fecha.Date >= desde.Date && venta.Fecha.Date <= hasta.Date);
            }
        }
    }
}
