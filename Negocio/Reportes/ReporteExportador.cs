using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaVentas.Entidades.DTOs;
using SistemaVentas.Entidades.Modelos.Reportes;

namespace SistemaVentas.Negocio.Reportes
{
    /// <summary>
    /// Genera los archivos de salida de los reportes del sistema, exportando a PDF mediante
    /// QuestPDF y a Excel mediante ClosedXML, a partir de los datos ya calculados por
    /// <see cref="ReporteNegocio"/>.
    /// </summary>
    public class ReporteExportador
    {
        /// <summary>
        /// Configura la licencia de la comunidad de QuestPDF antes de generar cualquier documento.
        /// </summary>
        static ReporteExportador()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        /// <summary>
        /// Genera un reporte en PDF con el listado de ventas (fecha, cliente y total) y un
        /// pie con la cantidad de ventas y el monto total.
        /// </summary>
        /// <param name="ventas">Lista de ventas a incluir en el reporte.</param>
        /// <param name="rutaArchivo">Ruta del archivo PDF de destino.</param>
        public void GenerarPdfVentas(List<VentaDTO> ventas, string rutaArchivo)
        {
            Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Margin(30);
                    pagina.Header().Text("Reporte de Ventas").FontSize(18).Bold();
                    pagina.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            columnas.RelativeColumn();
                            columnas.RelativeColumn(2);
                            columnas.RelativeColumn();
                        });

                        tabla.Header(encabezado =>
                        {
                            encabezado.Cell().Text("Fecha").Bold();
                            encabezado.Cell().Text("Cliente").Bold();
                            encabezado.Cell().Text("Total").Bold();
                        });

                        foreach (var venta in ventas)
                        {
                            tabla.Cell().Text(venta.Fecha.ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(venta.ClienteNombre ?? "N/A");
                            tabla.Cell().Text(venta.Total.ToString("C2"));
                        }
                    });
                    pagina.Footer().AlignRight().Text($"Total de ventas: {ventas.Count}   Monto total: {ventas.Sum(v => v.Total):C2}");
                });
            }).GeneratePdf(rutaArchivo);
        }

        /// <summary>
        /// Genera un reporte en PDF con tres secciones: el listado completo de productos,
        /// los productos más vendidos y los productos con bajo stock.
        /// </summary>
        /// <param name="productos">Listado completo de productos.</param>
        /// <param name="productosBajoStock">Productos cuyo stock está por debajo del umbral definido.</param>
        /// <param name="masVendidos">Productos más vendidos con su cantidad y total vendido.</param>
        /// <param name="rutaArchivo">Ruta del archivo PDF de destino.</param>
        public void GenerarPdfProductos( List<ProductoDTO> productos, List<ProductoDTO> productosBajoStock, List<ProductoVendido> masVendidos, string rutaArchivo)
        {
            Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Margin(30);
                    pagina.Header().Text("Reporte de Productos").FontSize(18).Bold();
                    pagina.Content().Column(columna =>
                    {
                        columna.Item().Text("Listado completo de productos")
                            .FontSize(14)
                            .Bold();

                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.RelativeColumn();
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn();
                                columnas.RelativeColumn();
                            });

                            tabla.Header(encabezado =>
                            {
                                encabezado.Cell().Text("Código").Bold();
                                encabezado.Cell().Text("Nombre").Bold();
                                encabezado.Cell().Text("Descripción").Bold();
                                encabezado.Cell().Text("Precio").Bold();
                                encabezado.Cell().Text("Stock").Bold();
                            });

                            foreach (var producto in productos)
                            {
                                tabla.Cell().Text(producto.Codigo);
                                tabla.Cell().Text(producto.Nombre);
                                tabla.Cell().Text(producto.Descripcion);
                                tabla.Cell().Text(producto.Precio.ToString("C2"));
                                tabla.Cell().Text(producto.Stock.ToString());
                            }
                        });

                        columna.Item().PaddingTop(20);

                        columna.Item().Text("Productos mas vendidos").FontSize(14).Bold();
                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn();
                                columnas.RelativeColumn();
                            });

                            tabla.Header(encabezado =>
                            {
                                encabezado.Cell().Text("Producto").Bold();
                                encabezado.Cell().Text("Cantidad vendida").Bold();
                                encabezado.Cell().Text("Total vendido").Bold();
                            });

                            foreach (var producto in masVendidos)
                            {
                                tabla.Cell().Text(producto.Nombre);
                                tabla.Cell().Text(producto.CantidadVendida.ToString());
                                tabla.Cell().Text(producto.TotalVendido.ToString("C2"));
                            }
                        });

                        columna.Item().PaddingTop(20).Text("Productos con bajo stock").FontSize(14).Bold();
                        columna.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columnas =>
                            {
                                columnas.RelativeColumn();
                                columnas.RelativeColumn(2);
                                columnas.RelativeColumn();
                            });

                            tabla.Header(encabezado =>
                            {
                                encabezado.Cell().Text("Codigo").Bold();
                                encabezado.Cell().Text("Producto").Bold();
                                encabezado.Cell().Text("Stock").Bold();
                            });

                            foreach (var producto in productosBajoStock)
                            {
                                tabla.Cell().Text(producto.Codigo);
                                tabla.Cell().Text(producto.Nombre);
                                tabla.Cell().Text(producto.Stock.ToString());
                            }
                        });
                    });
                });
            }).GeneratePdf(rutaArchivo);
        }

        /// <summary>
        /// Genera un reporte en PDF con el listado de clientes, su cantidad de ventas y el
        /// total comprado por cada uno.
        /// </summary>
        /// <param name="clientes">Lista de clientes con sus estadísticas de compra.</param>
        /// <param name="rutaArchivo">Ruta del archivo PDF de destino.</param>
        public void GenerarPdfClientes(List<ClienteCompra> clientes, string rutaArchivo)
        {
            Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Margin(30);
                    pagina.Header().Text("Reporte de Clientes").FontSize(18).Bold();
                    pagina.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            columnas.RelativeColumn(2);
                            columnas.RelativeColumn();
                            columnas.RelativeColumn();
                        });

                        tabla.Header(encabezado =>
                        {
                            encabezado.Cell().Text("Cliente").Bold();
                            encabezado.Cell().Text("Cantidad de ventas").Bold();
                            encabezado.Cell().Text("Total comprado").Bold();
                        });

                        foreach (var cliente in clientes)
                        {
                            tabla.Cell().Text(cliente.Nombre);
                            tabla.Cell().Text(cliente.CantidadVentas.ToString());
                            tabla.Cell().Text(cliente.TotalComprado.ToString("C2"));
                        }
                    });
                });
            }).GeneratePdf(rutaArchivo);
        }

        /// <summary>
        /// Genera un libro de Excel con una hoja "Ventas" que contiene el listado de ventas
        /// (fecha, cliente y total), con las columnas ajustadas al contenido.
        /// </summary>
        /// <param name="ventas">Lista de ventas a incluir en el reporte.</param>
        /// <param name="rutaArchivo">Ruta del archivo Excel de destino.</param>
        public void GenerarExcelVentas(List<VentaDTO> ventas, string rutaArchivo)
        {
            using (var libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add("Ventas");
                hoja.Cell(1, 1).Value = "Fecha";
                hoja.Cell(1, 2).Value = "Cliente";
                hoja.Cell(1, 3).Value = "Total";
                hoja.Row(1).Style.Font.Bold = true;

                int fila = 2;
                foreach (var venta in ventas)
                {
                    hoja.Cell(fila, 1).Value = venta.Fecha;
                    hoja.Cell(fila, 2).Value = venta.ClienteNombre ?? "N/A";
                    hoja.Cell(fila, 3).Value = venta.Total;
                    fila++;
                }

                hoja.Columns().AdjustToContents();
                libro.SaveAs(rutaArchivo);
            }
        }

        /// <summary>
        /// Genera un libro de Excel con tres hojas: el listado completo de productos, los
        /// productos más vendidos y los productos con bajo stock.
        /// </summary>
        /// <param name="productos">Listado completo de productos.</param>
        /// <param name="productosBajoStock">Productos cuyo stock está por debajo del umbral definido.</param>
        /// <param name="masVendidos">Productos más vendidos con su cantidad y total vendido.</param>
        /// <param name="rutaArchivo">Ruta del archivo Excel de destino.</param>
        public void GenerarExcelProductos( List<ProductoDTO> productos, List<ProductoDTO> productosBajoStock, List<ProductoVendido> masVendidos, string rutaArchivo)
        {
            using (var libro = new XLWorkbook())
            {

                var hojaProductos = libro.Worksheets.Add("Todos los productos");

                hojaProductos.Cell(1, 1).Value = "Código";
                hojaProductos.Cell(1, 2).Value = "Nombre";
                hojaProductos.Cell(1, 3).Value = "Descripción";
                hojaProductos.Cell(1, 4).Value = "Precio";
                hojaProductos.Cell(1, 5).Value = "Stock";

                hojaProductos.Row(1).Style.Font.Bold = true;

                int filaProductos = 2;

                foreach (var producto in productos)
                {
                    hojaProductos.Cell(filaProductos, 1).Value = producto.Codigo;
                    hojaProductos.Cell(filaProductos, 2).Value = producto.Nombre;
                    hojaProductos.Cell(filaProductos, 3).Value = producto.Descripcion;
                    hojaProductos.Cell(filaProductos, 4).Value = producto.Precio;
                    hojaProductos.Cell(filaProductos, 5).Value = producto.Stock;

                    filaProductos++;
                }

                hojaProductos.Columns().AdjustToContents();


                var hojaVendidos = libro.Worksheets.Add("Mas vendidos");
                hojaVendidos.Cell(1, 1).Value = "Producto";
                hojaVendidos.Cell(1, 2).Value = "Cantidad vendida";
                hojaVendidos.Cell(1, 3).Value = "Total vendido";
                hojaVendidos.Row(1).Style.Font.Bold = true;

                int fila = 2;
                foreach (var producto in masVendidos)
                {
                    hojaVendidos.Cell(fila, 1).Value = producto.Nombre;
                    hojaVendidos.Cell(fila, 2).Value = producto.CantidadVendida;
                    hojaVendidos.Cell(fila, 3).Value = producto.TotalVendido;
                    fila++;
                }
                hojaVendidos.Columns().AdjustToContents();

                var hojaStock = libro.Worksheets.Add("Bajo stock");
                hojaStock.Cell(1, 1).Value = "Codigo";
                hojaStock.Cell(1, 2).Value = "Producto";
                hojaStock.Cell(1, 3).Value = "Stock";
                hojaStock.Row(1).Style.Font.Bold = true;

                fila = 2;
                foreach (var producto in productosBajoStock)
                {
                    hojaStock.Cell(fila, 1).Value = producto.Codigo;
                    hojaStock.Cell(fila, 2).Value = producto.Nombre;
                    hojaStock.Cell(fila, 3).Value = producto.Stock;
                    fila++;
                }
                hojaStock.Columns().AdjustToContents();

                libro.SaveAs(rutaArchivo);
            }
        }

        /// <summary>
        /// Genera un libro de Excel con una hoja "Clientes" que contiene el listado de clientes,
        /// su cantidad de ventas y el total comprado por cada uno.
        /// </summary>
        /// <param name="clientes">Lista de clientes con sus estadísticas de compra.</param>
        /// <param name="rutaArchivo">Ruta del archivo Excel de destino.</param>
        public void GenerarExcelClientes(List<ClienteCompra> clientes, string rutaArchivo)
        {
            using (var libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add("Clientes");
                hoja.Cell(1, 1).Value = "Cliente";
                hoja.Cell(1, 2).Value = "Cantidad de ventas";
                hoja.Cell(1, 3).Value = "Total comprado";
                hoja.Row(1).Style.Font.Bold = true;

                int fila = 2;
                foreach (var cliente in clientes)
                {
                    hoja.Cell(fila, 1).Value = cliente.Nombre;
                    hoja.Cell(fila, 2).Value = cliente.CantidadVentas;
                    hoja.Cell(fila, 3).Value = cliente.TotalComprado;
                    fila++;
                }

                hoja.Columns().AdjustToContents();
                libro.SaveAs(rutaArchivo);
            }
        }
    }
}
