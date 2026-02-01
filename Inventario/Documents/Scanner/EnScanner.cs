using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Inventario.Documents.Scanner
{
    public class EnScanner : Controller
    {
        public IActionResult PdfEntrega(string codigo, string observaciones, string sucursal)
        {
            // 1. Obtención de datos de tus modelos
            ImComputadora Computadora = new ImComputadora();
            MComputadora item = Computadora.getComputadoraByNoInv(codigo);

            ImEmpleado emple = new ImEmpleado();
            MEmpleado empleado = emple.getEmple(item.Cod_Emple);

            // 2. Generación del documento (Réplica exacta del formato Ricza S.A.)
            var documento = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Letter);
                    page.Margin(35);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                    // --- ENCABEZADO: Títulos y Tabla de Control (Respetando IT-FOR-ESC-02) ---
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

                            // Fila 1: Títulos de la tabla [cite: 169]
                            table.Cell().Border(0.5f).Padding(2).Text("Código del Documento:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Versión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Fecha de Emisión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Página:").Bold().FontSize(8);

                            // Fila 2: Valores específicos para ESCÁNER [cite: 169]
                            table.Cell().Border(0.5f).Padding(2).Text("IT-FOR-ESC-02").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("01").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("26/01/2026").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text(x => { x.CurrentPageNumber(); x.Span(" de "); x.TotalPages(); });
                        });
                    });

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        // --- 1. DATOS DEL COLABORADOR --- [cite: 170]
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

                        // --- 2. IDENTIFICACIÓN DEL ACTIVO (ESCÁNER) --- [cite: 173]
                        column.Item().PaddingTop(15).Text("2. IDENTIFICACIÓN DEL ACTIVO (TRAZABILIDAD ISO)").Bold();
                        column.Item().PaddingTop(5).Text("En cumplimiento con el control de recursos de seguimiento y medición, se hace entrega del siguiente equipo de digitalización:");

                        column.Item().PaddingLeft(15).Column(c =>
                        {
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Descripción: ").Bold(); t.Span("Escáner de Documentos"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => {
                                    t.Span("Marca: ").Bold(); t.Span(item?.Marca ?? "__________________________ ");
                                    t.Span("Modelo: ").Bold(); t.Span(item?.Modelo ?? "_________________________");
                                });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Tipo: ").Bold(); t.Span("( ) Cama Plana  ( ) Con Alimentador (ADF)  ( ) Portátil"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => {
                                    t.Span("Número de Serie: ").Bold(); t.Span(item?.Serie ?? "__________________ ");
                                    t.Span("No. de Inventario: ").Bold(); t.Span("________________");
                                });
                            });
                        });

                        // --- 3. TÉRMINOS DE USO Y PRESERVACIÓN (ESCÁNER) --- [cite: 181]
                        column.Item().PaddingTop(15).Text("3. TÉRMINOS DE USO Y PRESERVACIÓN (Cláusula de Protección de Activos)").Bold();
                        column.Item().PaddingTop(5).Text("Para garantizar la integridad del equipo y la calidad de los procesos de digitalización según la norma ISO, el usuario acepta las siguientes normas de cuidado:");

                        column.Item().PaddingLeft(15).Column(list => {
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Protección del Cristal: ").Bold(); t.Span("No colocar objetos pesados sobre el cristal ni utilizar grapas, clips o elementos punzantes que puedan rayar la superficie de escaneo."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Cuidado del ADF (Alimentador): ").Bold(); t.Span("Antes de escanear, verificar que las hojas estén libres de pegamentos, cintas adhesivas o grapas para evitar atascos y daños en los rodillos de tracción."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Limpieza: ").Bold(); t.Span("No aplicar líquidos directamente sobre el equipo. Utilizar únicamente materiales de limpieza autorizados por el departamento de IT para evitar empañamiento o daño en los sensores ópticos."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Uso Previsto: ").Bold(); t.Span("Digitalización exclusiva de documentos relacionados con la operación de la empresa."); });
                        });

                        // --- 4. DECLARACIÓN DE CONFORMIDAD --- [cite: 191]
                        column.Item().PaddingTop(15).Text("4. DECLARACIÓN DE CONFORMIDAD").Bold();
                        column.Item().Text("El receptor manifiesta que recibe el equipo en óptimas condiciones, con sus cables de conexión y software operativo. Se hace responsable de su custodia y del reporte inmediato de cualquier anomalía técnica.");

                        // --- 5. FIRMAS DE ACEPTACIÓN --- [cite: 194]
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

                    // Pie de página [cite: 197]
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
            return File(pdfBytes, "application/pdf", $"Acta_Escaner_{codigo}.pdf");
        }
    }
}
