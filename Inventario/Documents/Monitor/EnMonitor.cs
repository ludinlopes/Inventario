using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Inventario.Documents.Monitor
{
    public class EnMonitor : Controller
    {
        public IActionResult PdfEntrega(string codigo, string observaciones, string sucursal)
        {
            // 1. Obtención de datos de tus modelos
            ImComputadora Computadora = new ImComputadora();
            MComputadora item = Computadora.getComputadoraByNoInv(codigo);

            ImEmpleado emple = new ImEmpleado();
            MEmpleado empleado = emple.getEmple(item.Cod_Emple);

            // 2. Generación del documento (Réplica exacta del Word/Imagen)
            var documento = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Letter);
                    page.Margin(35);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                    // --- ENCABEZADO: Títulos y Tabla de Control ---
                    page.Header().Column(header =>
                    {
                        header.Item().AlignCenter().Column(col =>
                        {
                            col.Item().AlignCenter().Text("SISTEMA DE GESTIÓN DE CALIDAD").Bold().FontSize(12);
                            col.Item().AlignCenter().Text("RICZA S.A.").Bold().FontSize(12);
                            col.Item().AlignCenter().Text("ACTA DE ASIGNACIÓN Y RESPONSABILIDAD DE ACTIVOS").Bold().FontSize(10);
                        });

                        header.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            // Fila 1: Títulos de la tabla
                            table.Cell().Border(0.5f).Padding(2).Text("Código del Documento:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Versión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Fecha de Emisión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Página:").Bold().FontSize(8);

                            // Fila 2: Valores
                            table.Cell().Border(0.5f).Padding(2).Text("IT-FOR-TAB-02").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("01").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("26/01/2026").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text(x => { x.CurrentPageNumber(); x.Span(" de "); x.TotalPages(); });
                        });

                        //header.Item().PaddingVertical(5).LineHorizontal(1f).Color(Colors.Grey.Medium);
                    });

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        // --- 1. DATOS DEL COLABORADOR ---
                        column.Item().Text("1. DATOS DEL COLABORADOR / USUARIO").Bold();
                        column.Item().PaddingLeft(15).Column(c =>
                        {
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Nombre Completo: ").Bold(); t.Span(empleado?.Nombre ?? "___________________________________________________________"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => {
                                    t.Span("Departamento / Área: ").Bold(); t.Span($"{sucursal ?? "________________"} ");
                                    t.Span("Puesto: ").Bold(); t.Span("___________________");
                                });
                            });
                        });

                        // --- 2. IDENTIFICACIÓN DEL ACTIVO ---
                        column.Item().PaddingTop(15).Text("2. IDENTIFICACIÓN DEL ACTIVO (TRAZABILIDAD ISO)").Bold();
                        column.Item().PaddingTop(5).Text("Se hace entrega del dispositivo móvil tipo Tablet con las siguientes especificaciones:");

                        column.Item().PaddingLeft(15).Column(c =>
                        {
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Marca: ").Bold(); t.Span(item?.Marca ?? "__________________________ "); t.Span("Modelo: ").Bold(); t.Span(item?.Modelo ?? "_________________________"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Capacidad (GB): ").Bold(); t.Span("_________________ "); t.Span("Color: ").Bold(); t.Span("__________________________"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Número de Serie: ").Bold(); t.Span(item?.Serie ?? "__________________ "); t.Span("No. de Inventario: ").Bold(); t.Span("________________"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Accesorios incluidos: ").Bold(); t.Span("( ) Cubierta Protectora (Case) ( ) Cargador y Cable USB ( ) Lápiz Óptico (Stylus)"); });
                            });
                        });

                        // --- 3. TÉRMINOS DE USO ---
                        column.Item().PaddingTop(15).Text("3. TÉRMINOS DE USO Y PRESERVACIÓN (Cláusula de Protección de Activos)").Bold();
                        column.Item().PaddingTop(5).Text("El colaborador acepta la responsabilidad del dispositivo y se compromete a cumplir las siguientes normas de preservación:");

                        column.Item().PaddingLeft(15).Column(list => {
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Protección Física: ").Bold(); t.Span("Es obligatorio mantener el dispositivo dentro de su estuche o funda protectora en todo momento. Se debe evitar colocar objetos pesados sobre la pantalla."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Seguridad de Acceso: ").Bold(); t.Span("El usuario debe configurar un método de bloqueo (PIN o Biometría). En caso de usar cuentas personales o corporativas para la descarga de apps, estas deben ser cerradas al devolver el equipo."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Cuidado de Batería: ").Bold(); t.Span("No dejar el dispositivo expuesto a altas temperaturas (dentro de vehículos, bajo el sol, etc.) y utilizar únicamente el cargador original provisto por IT."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Extravío o Robo: ").Bold(); t.Span("En caso de pérdida o robo, el colaborador debe reportarlo inmediatamente al departamento de IT para realizar el bloqueo remoto y proceder con la denuncia legal correspondiente."); });
                        });

                        // --- 4. DECLARACIÓN DE CONFORMIDAD ---
                        column.Item().PaddingTop(15).Text("4. DECLARACIÓN DE CONFORMIDAD").Bold();
                        column.Item().Text("El receptor manifiesta que recibe la Tablet en perfecto estado estético (sin rayones en pantalla ni golpes) y funcional. Se compromete a devolver el equipo libre de bloqueos de cuenta (iCloud/Google) al finalizar su asignación.");

                        // --- 5. FIRMAS DE ACEPTACIÓN ---
                        column.Item().PaddingTop(20).Text("5. FIRMAS DE ACEPTACIÓN").Bold();
                        column.Item().PaddingTop(5).Text("En la Ciudad de Guatemala, el día ______ de _______________ de 2026.");

                        column.Item().PaddingTop(40).Table(table => {
                            table.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(60); c.RelativeColumn(); });

                            table.Cell().Column(f => {
                                f.Item().AlignCenter().Text("Entrega (IT/Administración):").Bold();
                                f.Item().PaddingTop(30).LineHorizontal(0.5f);
                                f.Item().Text("Nombre: __________________________");
                            });

                            table.Cell();

                            table.Cell().Column(f => {
                                f.Item().AlignCenter().Text("Recibe (Usuario):").Bold();
                                f.Item().PaddingTop(30).Text("f. __________________________");
                                f.Item().Text($"DPI/Identificación: {"________________"}");
                            });
                        });
                    });

                    // Pie de página
                    page.Footer().PaddingTop(10).Column(footer => {
                        footer.Item().LineHorizontal(0.5f);
                        footer.Item().Text(t => {
                            t.Span("Nota de Confidencialidad: ").Bold().FontSize(8);
                            t.Span("Este documento es propiedad de Ricza S.A. y forma parte de sus registros de calidad.").FontSize(8);
                        });
                    });
                });
            });

            byte[] pdfBytes = documento.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Acta_Tablet_{codigo}.pdf");
        }
    }
}
