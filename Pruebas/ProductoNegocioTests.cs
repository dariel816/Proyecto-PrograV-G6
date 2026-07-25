using System;
using SistemaVentas.Entidades.Modelos;
using SistemaVentas.Negocio;

namespace SistemaVentas.Pruebas
{
    [TestClass]
    public class ProductoNegocioTests
    {
        private readonly ProductoNegocio productoNegocio = new ProductoNegocio();

        [TestMethod]
        public void InsertarProducto_NombreVacio_LanzaExcepcion()
        {
            var producto = new Producto { Codigo = "P-000", Nombre = "", Precio = 10, Stock = 5 };

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_PrecioCero_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new Producto { Codigo = "P-" + sufijo, Nombre = "Producto Prueba " + sufijo, Precio = 0, Stock = 5 };

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_PrecioNegativo_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new Producto { Codigo = "P-" + sufijo, Nombre = "Producto Prueba " + sufijo, Precio = -5, Stock = 5 };

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_Valido_PermiteConsultarloYQuedaDisponibleParaEliminar()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new Producto
            {
                Codigo = "P-" + sufijo,
                Nombre = "Producto Prueba " + sufijo,
                Descripcion = "Creado por pruebas unitarias",
                Precio = 15.5m,
                Stock = 20
            };

            bool creado = productoNegocio.InsertarProducto(producto);
            Assert.IsTrue(creado);

            var productos = productoNegocio.ObtenerProductos();
            var encontrado = productos.Find(p => p.Codigo == producto.Codigo);
            Assert.IsNotNull(encontrado);
            Assert.AreEqual(producto.Nombre, encontrado.Nombre);
            Assert.AreEqual(producto.Stock, encontrado.Stock);

            bool eliminado = productoNegocio.EliminarProducto(encontrado.Id);
            Assert.IsTrue(eliminado);
        }

        [TestMethod]
        public void InsertarProducto_CodigoDuplicado_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var original = new Producto
            {
                Codigo = "P-" + sufijo,
                Nombre = "Producto Original " + sufijo,
                Precio = 10,
                Stock = 5
            };

            productoNegocio.InsertarProducto(original);
            var productos = productoNegocio.ObtenerProductos();
            int idCreado = productos.Find(p => p.Codigo == original.Codigo)!.Id;

            try
            {
                var duplicado = new Producto
                {
                    Codigo = original.Codigo,
                    Nombre = "Producto Duplicado " + sufijo,
                    Precio = 20,
                    Stock = 3
                };

                Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(duplicado));
            }
            finally
            {
                productoNegocio.EliminarProducto(idCreado);
            }
        }
    }
}
