using System;
using System.IO;
using SistemaVentas.Entidades.DTOs;
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
            var producto = new ProductoDTO { Codigo = "P-000", Nombre = "", Precio = 10, Stock = 5 };

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_PrecioCero_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new ProductoDTO{Codigo = "P-" + sufijo, Nombre = "Producto Prueba " + sufijo, Descripcion = "Producto para validar precio cero", Precio = 0,Stock = 5};

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_PrecioNegativo_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new ProductoDTO{ Codigo = "P-" + sufijo, Nombre = "Producto Prueba " + sufijo,Descripcion = "Producto para validar precio negativo",Precio = -5, Stock = 5};

            Assert.ThrowsExactly<Exception>(() => productoNegocio.InsertarProducto(producto));
        }

        [TestMethod]
        public void InsertarProducto_Valido_PermiteConsultarloYQuedaDisponibleParaEliminar()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new ProductoDTO
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
            var original = new ProductoDTO
            {
                Codigo = "P-" + sufijo,
                Nombre = "Producto Original " + sufijo,
                Descripcion = "Producto original para probar código duplicado",
                Precio = 10,
                Stock = 5
            };

            productoNegocio.InsertarProducto(original);
            var productos = productoNegocio.ObtenerProductos();
            int idCreado = productos.Find(p => p.Codigo == original.Codigo)!.Id;

            try
            {
                var duplicado = new ProductoDTO
                {
                    Codigo = original.Codigo,
                    Nombre = "Producto Duplicado " + sufijo,
                    Descripcion = "Producto duplicado para realizar la prueba",
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

        [TestMethod]
        public void ExportarEImportarCatalogoJson_RecuperaElProductoEliminado()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var producto = new ProductoDTO
            {
                Codigo = "PJ-" + sufijo,
                Nombre = "Producto JSON " + sufijo,
                Descripcion = "Prueba de exportacion e importacion JSON",
                Precio = 12.5m,
                Stock = 8
            };

            productoNegocio.InsertarProducto(producto);
            int idOriginal = productoNegocio.ObtenerProductos().Find(p => p.Codigo == producto.Codigo)!.Id;

            string rutaTemp = Path.Combine(Path.GetTempPath(), "catalogo_prueba_" + sufijo + ".json");

            try
            {
                productoNegocio.ExportarCatalogoJson(rutaTemp);
                Assert.IsTrue(File.Exists(rutaTemp));

                // Se elimina el producto para simular la perdida de datos que el import debe recuperar
                productoNegocio.EliminarProducto(idOriginal);

                var resultado = productoNegocio.ImportarCatalogoJson(rutaTemp);
                Assert.IsGreaterThanOrEqualTo(1, resultado.Importados);

                var reimportado = productoNegocio.ObtenerProductos().Find(p => p.Codigo == producto.Codigo);
                Assert.IsNotNull(reimportado);
                Assert.AreEqual(producto.Nombre, reimportado.Nombre);
                Assert.AreEqual(producto.Stock, reimportado.Stock);

                productoNegocio.EliminarProducto(reimportado.Id);
            }
            finally
            {
                if (File.Exists(rutaTemp))
                    File.Delete(rutaTemp);
            }
        }
    }
}
