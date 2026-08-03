using SistemaVentas.Datos.Repositorios;

namespace SistemaVentas.Datos.Fabricas
{
    /// <summary>
    /// Fábrica (patrón Factory) que crea instancias de los repositorios,
    /// centralizando la cadena de conexión a la base de datos para que la
    /// capa de Negocio no tenga que instanciar los repositorios concretos
    /// ni conocer la cadena de conexión directamente.
    /// </summary>
    public static class RepositorioFactory
    {
        private const string CadenaConexion = "server=localhost;database=sistema_ventas;user=root;password=root123;";

        /// <summary>
        /// Crea una instancia de <see cref="IClienteRepositorio"/> lista para usar.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="ClienteRepositorio"/>.</returns>
        public static IClienteRepositorio CrearClienteRepositorio() => new ClienteRepositorio(CadenaConexion);

        /// <summary>
        /// Crea una instancia de <see cref="IProductoRepositorio"/> lista para usar.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="ProductoRepositorio"/>.</returns>
        public static IProductoRepositorio CrearProductoRepositorio() => new ProductoRepositorio();

        /// <summary>
        /// Crea una instancia de <see cref="IVentaRepositorio"/> lista para usar.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="VentaRepositorio"/>.</returns>
        public static IVentaRepositorio CrearVentaRepositorio() => new VentaRepositorio();

        /// <summary>
        /// Crea una instancia de <see cref="IDetalleVentaRepositorio"/> lista para usar.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="DetalleVentaRepositorio"/>.</returns>
        public static IDetalleVentaRepositorio CrearDetalleVentaRepositorio() => new DetalleVentaRepositorio();

        /// <summary>
        /// Crea una instancia de <see cref="IUsuarioRepositorio"/> lista para usar.
        /// </summary>
        /// <returns>Una nueva instancia de <see cref="UsuarioRepositorio"/>.</returns>
        public static IUsuarioRepositorio CrearUsuarioRepositorio() => new UsuarioRepositorio(CadenaConexion);
    }
}
