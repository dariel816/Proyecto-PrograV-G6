using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Negocio;

namespace SistemaVentas.Pruebas
{
    [TestClass]
    public class UsuarioNegocioTests
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        [TestMethod]
        public void ValidarLogin_CredencialesCorrectas_RetornaElUsuario()
        {
            UsuarioDTO? usuario = usuarioNegocio.ValidarLogin("admin", "admin123");

            Assert.IsNotNull(usuario);
            Assert.AreEqual("admin", usuario.NombreUsuario);
            Assert.AreEqual("Administrador", usuario.Rol);
        }

        [TestMethod]
        public void ValidarLogin_ContraseñaIncorrecta_RetornaNull()
        {
            UsuarioDTO? usuario = usuarioNegocio.ValidarLogin("admin", "contraseñaIncorrecta");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void ValidarLogin_UsuarioInexistente_RetornaNull()
        {
            UsuarioDTO? usuario = usuarioNegocio.ValidarLogin("usuarioQueNoExiste", "cualquierClave");

            Assert.IsNull(usuario);
        }

        [TestMethod]
        public void ValidarLogin_CamposVacios_RetornaNull()
        {
            Assert.IsNull(usuarioNegocio.ValidarLogin("", ""));
            Assert.IsNull(usuarioNegocio.ValidarLogin("admin", ""));
            Assert.IsNull(usuarioNegocio.ValidarLogin("", "admin123"));
        }
    }
}
