using System;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Negocio;

namespace SistemaVentas.Pruebas
{
    [TestClass]
    public class ClienteNegocioTests
    {
        private readonly ClienteNegocio clienteNegocio = new ClienteNegocio();

        [TestMethod]
        public void InsertarCliente_NombreVacio_LanzaExcepcion()
        {
            var cliente = new ClienteDTO { Nombre = "", Correo = "test@correo.com", Telefono = "00000000" };

            Assert.ThrowsExactly<Exception>(() => clienteNegocio.InsertarCliente(cliente));
        }

        [TestMethod]
        public void InsertarCliente_NombreConEspacios_LanzaExcepcion()
        {
            var cliente = new ClienteDTO { Nombre = "   ", Correo = "test2@correo.com", Telefono = "00000001" };

            Assert.ThrowsExactly<Exception>(() => clienteNegocio.InsertarCliente(cliente));
        }

        [TestMethod]
        public void InsertarCliente_Valido_PermiteConsultarloYQuedaDisponibleParaEliminar()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var cliente = new ClienteDTO
            {
                Nombre = "Cliente Prueba " + sufijo,
                Correo = "prueba" + sufijo + "@test.com",
                Telefono = "8" + sufijo.Substring(0, 7)
            };

            bool creado = clienteNegocio.InsertarCliente(cliente);
            Assert.IsTrue(creado);

            var clientes = clienteNegocio.ObtenerClientes();
            var encontrado = clientes.Find(c => c.Correo == cliente.Correo);
            Assert.IsNotNull(encontrado);
            Assert.AreEqual(cliente.Nombre, encontrado.Nombre);

            bool eliminado = clienteNegocio.EliminarCliente(encontrado.Id);
            Assert.IsTrue(eliminado);
        }

        [TestMethod]
        public void InsertarCliente_CorreoDuplicado_LanzaExcepcion()
        {
            string sufijo = Guid.NewGuid().ToString("N").Substring(0, 8);
            var original = new ClienteDTO
            {
                Nombre = "Cliente Original " + sufijo,
                Correo = "duplicado" + sufijo + "@test.com",
                Telefono = "8" + sufijo.Substring(0, 7)
            };

            clienteNegocio.InsertarCliente(original);
            var clientes = clienteNegocio.ObtenerClientes();
            int idCreado = clientes.Find(c => c.Correo == original.Correo)!.Id;

            try
            {
                var duplicado = new ClienteDTO
                {
                    Nombre = "Cliente Duplicado " + sufijo,
                    Correo = original.Correo,
                    Telefono = "7" + sufijo.Substring(0, 7)
                };

                Assert.ThrowsExactly<Exception>(() => clienteNegocio.InsertarCliente(duplicado));
            }
            finally
            {
                clienteNegocio.EliminarCliente(idCreado);
            }
        }
    }
}
