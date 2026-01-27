using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Inventario.Documents.Tablet
{
    public class EnTablet : Controller
    {
        public IActionResult PdfEntrega(string codigo, string observaciones, string sucursal)
        {
            // 1. Aquí simularías traer los datos de tu base de datos
            // var item = _db.Inventario.First(x => x.Codigo == codigo);
            ImComputadora Computadora = new ImComputadora();
            MComputadora item = Computadora.getComputadoraByNoInv(codigo);

            ImEmpleado emple = new ImEmpleado();
            MEmpleado empleado = emple.getEmple(item.Cod_Emple);
            byte[] pdfBytes = new byte[0];



            // Generación del documento ajustado a tamaño Carta
            var documento = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    // CAMBIO: Se define PageSizes.Letter (8.5 x 11 pulgadas) 
                    page.Size(PageSizes.Letter);

                    // Propiedades de Formato: Márgenes de 1 pulgada aprox (72 puntos)
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                    // --- ENCABEZADO (SISTEMA DE GESTIÓN DE CALIDAD) ---
                    page.Header().Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            // Títulos de Ricza S.A. [cite: 2, 27]
                            table.Cell().RowSpan(2).Border(1).Padding(5).Column(c =>
                            {
                                c.Item().Text("SISTEMA DE GESTIÓN DE CALIDAD").Bold().FontSize(11).AlignCenter();
                                c.Item().Text("RICZA S.A.").Bold().FontSize(11).AlignCenter();
                                c.Item().Text("ACTA DE ASIGNACIÓN Y RESPONSABILIDAD DE ACTIVOS").FontSize(9).AlignCenter();
                            });

                            // Datos de control: IT-FOR-TAB-02 
                            table.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(2).Text("Código").FontSize(8).AlignCenter();
                            table.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(2).Text("Versión").FontSize(8).AlignCenter();
                            table.Cell().Border(1).Background(Colors.Grey.Lighten3).Padding(2).Text("Fecha").FontSize(8).AlignCenter();

                            table.Cell().Border(1).AlignCenter().Text("IT-FOR-TAB-02");
                            table.Cell().Border(1).AlignCenter().Text("01");
                            table.Cell().Border(1).AlignCenter().Text("26/01/2026");
                        });
                    });

                    // --- CUERPO DEL DOCUMENTO ---
                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        // 1. Datos del Colaborador [cite: 5]
                        column.Item().PaddingTop(10).Text("1. DATOS DEL COLABORADOR / USUARIO").Bold().BackgroundColor(Colors.Grey.Lighten4);
                        column.Item().Text($"Nombre Completo: ___________________________________________________________"); // [cite: 6]
                        column.Item().Text($"Departamento / Área: ______________________  Puesto: ___________________"); // [cite: 7]

                        // 2. Identificación del Activo (Trazabilidad ISO) [cite: 8]
                        column.Item().PaddingTop(10).Text("2. IDENTIFICACIÓN DEL ACTIVO (TRAZABILIDAD ISO)").Bold().BackgroundColor(Colors.Grey.Lighten4);
                        column.Item().Text("Se hace entrega del dispositivo móvil tipo Tablet con las siguientes especificaciones:"); // [cite: 9]

                        column.Item().PaddingTop(5).Table(table => {
                            table.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                             table.Cell().Text("Marca: ____________________"); // [cite: 10]
                            table.Cell().Text("Modelo: ___________________"); // [cite: 10]
                            table.Cell().Text("Capacidad (GB): ___________"); // [cite: 11]
                            table.Cell().Text("Color: ____________________"); // [cite: 11]
                            table.Cell().Text("Número de Serie: __________"); // [cite: 12]
                            table.Cell().Text("No. de Inventario: ________"); // [cite: 12]
                        });

                        // Accesorios [cite: 13]
                        column.Item().PaddingTop(5).Text("Accesorios: ( ) Cubierta Protectora ( ) Cargador y Cable USB ( ) Lápiz Óptico");

                        // 3. Términos de Uso y Preservación [cite: 14, 15]
                        column.Item().PaddingTop(10).Text("3. TÉRMINOS DE USO Y PRESERVACIÓN").Bold().BackgroundColor(Colors.Grey.Lighten4);
                        //column.Item().BulletPoint().Text("Protección Física: Mantener en estuche y evitar objetos pesados."); // [cite: 16, 17]
                        //column.Item().BulletPoint().Text("Seguridad: Configurar PIN o Biometría y cerrar cuentas al devolver."); // [cite: 18, 19]
                        //column.Item().BulletPoint().Text("Cuidado: No exponer a altas temperaturas y usar cargador original."); // [cite: 20]
                        //column.Item().BulletPoint().Text("Extravío: Reportar inmediatamente a IT para bloqueo remoto."); // [cite: 21]


                        var normas = new[] {
                "Protección Física: Es obligatorio mantener el dispositivo en su estuche[cite: 16].",
                "Seguridad de Acceso: El usuario debe configurar PIN o Biometría[cite: 18].",
                "Cuidado de Batería: Utilizar únicamente el cargador original provisto por IT[cite: 20].",
                "Extravío o Robo: Reportar inmediatamente a IT para bloqueo remoto[cite: 21]."
            };

                        foreach (var norma in normas)
                        {
                            column.Item().Row(row =>
                            {
                                row.ConstantItem(15).AlignCenter().Text("•");
                                row.RelativeItem().Text(norma).FontSize(9);
                            });
                        }


                        // 4. Declaración de Conformidad [cite: 22, 23]
                        column.Item().PaddingTop(10).Border(0.5f).Padding(5).Column(c => {
                            c.Item().Text("4. DECLARACIÓN DE CONFORMIDAD").Bold();
                             c.Item().Text("El receptor recibe la Tablet en perfecto estado estético y funcional, comprometiéndose a devolverla libre de bloqueos de cuenta (iCloud/Google)."); // [cite: 24]
                        });

                        // 5. Firmas de Aceptación [cite: 29]
                        column.Item().PaddingTop(20).Text("En la Ciudad de Guatemala, el día ______ de _______________ de 2026."); // [cite: 30]

                        column.Item().PaddingTop(25).Table(table =>
                        {
                            table.ColumnsDefinition(c => { c.RelativeColumn(); c.ConstantColumn(40); c.RelativeColumn(); });

                            table.Cell().Column(f => {
                                f.Item().PaddingTop(30).LineHorizontal(1);
                                 f.Item().AlignCenter().Text("Entrega (IT/Administración)"); // [cite: 31]
                                f.Item().AlignCenter().Text("Nombre:");
                            });
                            table.Cell();
                            table.Cell().Column(f => {
                                f.Item().PaddingTop(30).LineHorizontal(1);
                                 f.Item().AlignCenter().Text("Recibe (Usuario)"); // [cite: 31]
                                f.Item().AlignCenter().Text("DPI/Identificación:");
                            });
                        });
                    });

                    
                    page.Footer().AlignCenter().Text(t => {
                        t.Span("Nota de Confidencialidad: Este documento es propiedad de Ricza S.A. ").FontSize(8).Italic();
                        t.Span("- Página ").FontSize(8);
                        t.CurrentPageNumber().FontSize(8);
                    });
                });
            });

            pdfBytes = documento.GeneratePdf();



            // Devolvemos el archivo como un stream para que el JS lo reciba
            return File(pdfBytes, "application/pdf");
        }
    }
}
