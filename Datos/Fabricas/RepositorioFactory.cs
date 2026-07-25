using SistemaVentas.Datos.Repositorios;

namespace SistemaVentas.Datos.Fabricas
{
    public static class RepositorioFactory
    {
        private const string CadenaConexion = "server=localhost;database=sistema_ventas;user=root;password=root123;";

        public static IClienteRepositorio CrearClienteRepositorio() => new ClienteRepositorio(CadenaConexion);

        public static IProductoRepositorio CrearProductoRepositorio() => new ProductoRepositorio();

        public static IVentaRepositorio CrearVentaRepositorio() => new VentaRepositorio();

        public static IDetalleVentaRepositorio CrearDetalleVentaRepositorio() => new DetalleVentaRepositorio();
    }
}
