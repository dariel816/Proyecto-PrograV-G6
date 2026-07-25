using System;
using System.Collections.Generic;
using SistemaVentas.Entidades.Modelos;
using SistemaVentas.Negocio;

namespace SistemaVentas.Pruebas
{
    [TestClass]
    public class VentaNegocioTests
    {
        private readonly VentaNegocio ventaNegocio = new VentaNegocio();
        private readonly ClienteNegocio clienteNegocio = new ClienteNegocio();
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        [TestMethod]
        public void CrearVenta_VentaNula_LanzaExcepcion()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => ventaNegocio.CrearVenta(null!, new List<DetalleVenta>()));
        }

        [TestMethod]
        public void CrearVenta_SinDetalles_LanzaExcepcion()
        {
            var venta = new Venta { ClienteId = 1 };

            Assert.ThrowsExactly<Exception>(() => ventaNegocio.CrearVenta(venta, new List<DetalleVenta>()));
        }

        [TestMethod]
        public void CrearVenta_CantidadCero_LanzaExcepcion()
        {
            var venta = new Venta { ClienteId = 1 };
            var detalles = new List<DetalleVenta>
            {
                new DetalleVenta { ProductoId = 1, Cantidad = 0 }
            };

            Assert.ThrowsExactly<Exception>(() => ventaNegocio.CrearVenta(venta, detalles));
        }

        [TestMethod]
        public void CrearVenta_ProductoInexistente_LanzaExcepcion()
        {
            var venta = new Venta { ClienteId = 1 };
            var detalles = new List<DetalleVenta>
            {
                new DetalleVenta { ProductoId = -999, Cantidad = 1 }
            };

            Assert.ThrowsExactly<Exception>(() => ventaNegocio.CrearVenta(venta, detalles));
        }

        [TestMethod]
        public void CrearVenta_StockInsuficiente_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var cliente = new Cliente { Nombre = "Cliente Venta " + sufijo, Correo = "venta" + sufijo + "@test.com", Telefono = "8" + sufijo.Substring(0, 7) };
            clienteNegocio.InsertarCliente(cliente);
            int idCliente = clienteNegocio.ObtenerClientes().Find(c => c.Correo == cliente.Correo)!.Id;

            var producto = new Producto { Codigo = "P-" + sufijo, Nombre = "Producto Venta " + sufijo, Precio = 10, Stock = 1 };
            productoNegocio.InsertarProducto(producto);
            int idProducto = productoNegocio.ObtenerProductos().Find(p => p.Codigo == producto.Codigo)!.Id;

            try
            {
                var venta = new Venta { ClienteId = idCliente };
                var detalles = new List<DetalleVenta>
                {
                    new DetalleVenta { ProductoId = idProducto, Cantidad = 5 }
                };

                Assert.ThrowsExactly<Exception>(() => ventaNegocio.CrearVenta(venta, detalles));
            }
            finally
            {
                productoNegocio.EliminarProducto(idProducto);
                clienteNegocio.EliminarCliente(idCliente);
            }
        }

        [TestMethod]
        public void CrearVenta_StockSuficiente_RegistraLaVentaYDescuentaElStock()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var cliente = new Cliente { Nombre = "Cliente Venta " + sufijo, Correo = "venta2" + sufijo + "@test.com", Telefono = "7" + sufijo.Substring(0, 7) };
            clienteNegocio.InsertarCliente(cliente);
            int idCliente = clienteNegocio.ObtenerClientes().Find(c => c.Correo == cliente.Correo)!.Id;

            var producto = new Producto { Codigo = "PV-" + sufijo, Nombre = "Producto Venta " + sufijo, Precio = 10, Stock = 10 };
            productoNegocio.InsertarProducto(producto);
            int idProducto = productoNegocio.ObtenerProductos().Find(p => p.Codigo == producto.Codigo)!.Id;

            int? idVentaCreada = null;

            try
            {
                var venta = new Venta { ClienteId = idCliente };
                var detalles = new List<DetalleVenta>
                {
                    new DetalleVenta { ProductoId = idProducto, Cantidad = 3 }
                };

                bool resultado = ventaNegocio.CrearVenta(venta, detalles);
                Assert.IsTrue(resultado);

                var productoActualizado = productoNegocio.ObtenerProductoPorId(idProducto);
                Assert.IsNotNull(productoActualizado);
                Assert.AreEqual(7, productoActualizado.Stock);

                var ventaCreada = ventaNegocio.ObtenerVentas().Find(v => v.ClienteId == idCliente);
                Assert.IsNotNull(ventaCreada);
                Assert.AreEqual(30m, ventaCreada.Total);
                idVentaCreada = ventaCreada.Id;
            }
            finally
            {
                if (idVentaCreada.HasValue)
                    ventaNegocio.EliminarVenta(idVentaCreada.Value);

                productoNegocio.EliminarProducto(idProducto);
                clienteNegocio.EliminarCliente(idCliente);
            }
        }
    }
}
