using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Inventario.Documents.UPS
{
    public class EnUPS : Controller
    {
        public IActionResult PdfEntrega(string codigo, string observaciones, string sucursal)
        {
            // 1. Obtención de datos (Mantenemos tu lógica de modelos)
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

                    // --- ENCABEZADO: Títulos y Tabla de Control (Respetando IT-FOR-UPS-01) ---
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

                            // Fila 1: Títulos
                            table.Cell().Border(0.5f).Padding(2).Text("Código del Documento:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Versión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Fecha de Emisión:").Bold().FontSize(8);
                            table.Cell().Border(0.5f).Padding(2).Text("Página:").Bold().FontSize(8);

                            // Fila 2: Valores específicos para UPS
                            table.Cell().Border(0.5f).Padding(2).Text("IT-FOR-UPS-01").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("01").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text("26/01/2026").FontSize(9);
                            table.Cell().Border(0.5f).Padding(2).Text(x => { x.CurrentPageNumber(); x.Span(" de "); x.TotalPages(); });
                        });
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

                        // --- 2. IDENTIFICACIÓN DEL ACTIVO (UPS) ---
                        column.Item().PaddingTop(15).Text("2. IDENTIFICACIÓN DEL ACTIVO (TRAZABILIDAD ISO)").Bold();
                        column.Item().PaddingTop(5).Text("En cumplimiento con el control de recursos de seguimiento y medición, se hace entrega del siguiente equipo:");

                        column.Item().PaddingLeft(15).Column(c =>
                        {
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Descripción: ").Bold(); t.Span("Unidad de Alimentación Ininterrumpida (UPS)"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Marca: ").Bold(); t.Span(item?.Marca ?? "__________________________ "); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Modelo: ").Bold(); t.Span(item?.Modelo ?? "_________________________"); });
                            });
                            c.Item().PaddingTop(5).Row(row => {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(t => { t.Span("Número de Serie: ").Bold(); t.Span(item?.Serie ?? "__________________ "); t.Span("No. de Inventario: ").Bold(); t.Span( "________________"); });
                            });
                        });

                        // --- 3. TÉRMINOS DE USO Y PRESERVACIÓN (UPS) ---
                        column.Item().PaddingTop(15).Text("3. TÉRMINOS DE USO Y PRESERVACIÓN (Cláusula de Protección de Activos)").Bold();
                        column.Item().PaddingTop(5).Text("Para garantizar la integridad del equipo y la continuidad de los procesos según la norma de calidad, el usuario acepta las siguientes restricciones:");

                        column.Item().PaddingLeft(15).Column(list => {
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Uso Previsto: ").Bold(); t.Span("El equipo está destinado únicamente para el respaldo de energía de computadora y cargadores."); });
                            list.Item().PaddingTop(3).Text(t => { t.Span("• Restricciones de Carga: ").Bold(); t.Span("Queda estrictamente prohibido el uso de periféricos de alta resistencia o carga inductiva, tales como:"); });

                            // Sub-lista de prohibiciones
                            list.Item().PaddingLeft(20).Column(subList => {
                                subList.Item().Text("- Equipos de cuidado personal: Secadoras y planchas de pelo.");
                                subList.Item().Text("- Equipos de climatización: Ventiladores o calefactores.");
                                subList.Item().Text("- Otros: Motores eléctricos o cafeteras.");
                            });
                        });

                        // --- 4. DECLARACIÓN DE CONFORMIDAD ---
                        column.Item().PaddingTop(15).Text("4. DECLARACIÓN DE CONFORMIDAD").Bold();
                        column.Item().Text("El receptor manifiesta que recibe el equipo en condiciones operativas y se hace responsable de su custodia. Cualquier falla técnica debe ser reportada inmediatamente al departamento de IT para su mantenimiento correctivo, evitando la manipulación por personal no autorizado.");

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
            return File(pdfBytes, "application/pdf", $"Acta_UPS_{codigo}.pdf");
        }
    }
}
