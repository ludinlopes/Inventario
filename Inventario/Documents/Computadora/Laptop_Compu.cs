using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Inventario.Documents.Entregas
{
    public class Laptop_Compu : Controller
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
  

                    var documento = QuestPDF.Fluent.Document.Create(container =>
                    {

                        switch (item.Tipo)
                        {
                            case "LAPTOP":
                                container.Page(page =>
                                {
                                    // Márgenes: Sup/Inf 2.5cm, Izq/Der 3cm
                                    page.MarginTop(2.5f, Unit.Centimetre);
                                    page.MarginBottom(2.5f, Unit.Centimetre);
                                    page.MarginLeft(3, Unit.Centimetre);
                                    page.MarginRight(3, Unit.Centimetre);

                                    page.PageColor(Colors.White);

                                    // Estilo: Calibri, Tamaño 11, Interlineado 1.15
                                    page.DefaultTextStyle(x => x
                                        .FontSize(11)
                                        .FontFamily("Calibri")
                                        .LineHeight(1.15f)
                                    );

                                    // --- ENCABEZADO ---
                                    page.Content().PaddingVertical(10).Column(col =>
                                    {
                                        col.Item().AlignCenter().Text("ENTREGA DE EQUIPO Y POLÍTICAS DE USO").FontSize(12).ExtraBold().Underline();

                                        col.Item().PaddingTop(10).Text(t =>
                                        {
                                            t.Justify();
                                            t.Span("Yo ");
                                            t.Span(empleado.Nombre).Bold();
                                            t.Span(" me identifico con el documento personal de identificación con código único de identificación número ");
                                            t.Span(empleado.Noidentificacion).Bold();
                                            t.Span(" extendido por el Registro Nacional de las Personas, como empleado de ");
                                            t.Span(sucursal).Bold();
                                            t.Span(" estoy de acuerdo con las siguientes declaraciones relacionadas con la prestación de una computadora portátil para uso laboral y así mismo he aceptado seguir las Políticas de uso siguientes:");
                                        });
                                        //});

                                        // --- CUERPO ---

                                        col.Spacing(2);

                                        col.Item().Text("1. La computadora portátil es propiedad de " + sucursal + " todos los derechos del equipo y del software.").Justify();
                                        col.Spacing(10);
                                        col.Item().Text("2. Acepto que soy completamente responsable de la computadora portátil mientras está a mi cargo.").Justify();
                                        col.Item().Text("3. Yo estoy de acuerdo con seguir todas las regulaciones y políticas internas de " + sucursal + " con respecto al uso de las computadoras portátiles, el uso correcto de software y la información.").Justify();
                                        col.Item().Text("4. Yo estoy de acuerdo con no remover o alterar ninguna etiqueta de identificación que estén adjuntas o se muestren en la computadora portátil o con no cambiar ninguna identificación propia del equipo.").Justify();
                                        col.Item().Text("5. Yo estoy de acuerdo con no eliminar ningún archivo, cotizaciones, bases de datos, carteras de clientes o software que puedan ser útiles para " + sucursal).Justify();
                                        col.Item().Text("6. Yo estoy de acuerdo con mantener la computadora portátil y la información en un lugar seguro (Ejemplo: no dejar la computadora portátil a la vista de todos en un auto con seguro, ni en áreas de extrema temperatura o humedad).").Justify();
                                        col.Item().Text("7. Yo estoy de acuerdo con reportar inmediatamente al área correspondiente en caso de robo, pérdida o daño a la computadora portátil.").Justify();
                                        col.Item().Text("8. Estoy de acuerdo con devolver el equipo al momento que me retire de la empresa, deberé devolver la computadora portátil con su cargador o accesorios proporcionados al momento de la entrega.").Justify();
                                        col.Item().Text("9. Yo estoy de acuerdo con que todo el trabajo de reparación será completado por el Departamento de Informática autorizado por " + sucursal).Justify();
                                        col.Item().Text("10. Los costos asociados debido al abuso o uso indebido del equipo será mi responsabilidad.").Justify();
                                        col.Item().Text("11. No instalar programas no autorizados por Gerencia. Todo programa autorizado deberá ser instalado por el departamento de Informática.").Justify();
                                        col.Item().Text("12. No guardar archivos personales tales como imágenes, videos, música o documentos ajenos a " + sucursal + " informática tiene autorizado borrar tal información de ser necesario.").Justify();
                                        col.Item().PageBreak();
                                        col.Item().Text("13. De ser necesario el reemplazo por robo, pérdida o daño, el equipo a comprar, deberá tener las mismas características al equipo anterior (acorde a la versión tecnológica en el momento del reemplazo).").Justify();
                                        col.Item().Text("14. Tendré limpio el equipo, en todo momento, para evitar daños ocasionados por el medio ambiente.").Justify();

                                        col.Item().PaddingTop(10).Text("Yo he leído, entendido y acepto íntegramente todos los términos de este contrato, en utilizar la computadora portátil como una herramienta estrictamente laboral.").Bold().Justify();

                                        // --- DATOS DEL EQUIPO ---
                                        col.Item().PaddingTop(15).Column(dataCol =>
                                        {
                                            dataCol.Item().Text("Los datos del equipo a recibir son:").Bold();
                                            dataCol.Item().Text(t => {
                                                t.Span("Marca: ").Bold(); t.Span("DELL  ");
                                                t.Span("Modelo: ").Bold(); t.Span("PRO 16  ");
                                                t.Span("Serie: ").Bold(); t.Span("98SD694");
                                            });
                                        });

                                        // --- OBSERVACIONES ---
                                        col.Item().PaddingTop(10).Text(t =>
                                        {
                                            t.Span("Observaciones: ").Bold();
                                            t.Span(observaciones ?? "_____________________________________________________________");
                                        });
                                        col.Item().Text("(Esto es por si el equipo tiene un defecto o daño antes de entregar)").FontSize(9).Italic();
                                    });

                                    // --- FIRMAS ---
                                    page.Footer().Row(row =>
                                    {
                                        row.RelativeItem().Column(sig =>
                                        {
                                            sig.Item().AlignCenter().Text("f._____________________________________");
                                            sig.Item().AlignCenter().Text("Firma del Empleado").Bold();
                                            sig.Item().AlignCenter().Text("William Rolando Recinos Pérez").FontSize(9);
                                        });

                                        row.ConstantItem(40);

                                        row.RelativeItem().Column(sig =>
                                        {
                                            sig.Item().AlignCenter().Text("f._____________________________________");
                                            sig.Item().AlignCenter().Text("Recibido / Informática").Bold();
                                            sig.Item().AlignCenter().Text(sucursal).FontSize(9);
                                        });
                                    });
                                });
                                break;
                            default:
                                container.Page(page =>
                                {
                                    // Márgenes: Sup/Inf 2.5cm, Izq/Der 3cm
                                    page.MarginTop(2.5f, Unit.Centimetre);
                                    page.MarginBottom(2.5f, Unit.Centimetre);
                                    page.MarginLeft(3, Unit.Centimetre);
                                    page.MarginRight(3, Unit.Centimetre);

                                    page.PageColor(Colors.White);

                                    // Estilo: Calibri, Tamaño 11, Interlineado 1.15
                                    page.DefaultTextStyle(x => x
                                        .FontSize(11)
                                        .FontFamily("Calibri")
                                        .LineHeight(1.15f)
                                    );

                                    // --- ENCABEZADO ---
                                    page.Content().PaddingVertical(10).Column(col =>
                                    {
                                        col.Item().AlignCenter().Text("ENTREGA DE EQUIPO Y POLÍTICAS DE USO").FontSize(12).ExtraBold().Underline();

                                        col.Item().PaddingTop(10).Text(t =>
                                        {
                                            t.Justify();
                                            t.Span("Yo ");
                                            t.Span(empleado.Nombre).Bold();
                                            t.Span(" me identifico con el documento personal de identificación con código único de identificación número ");
                                            t.Span(empleado.Noidentificacion).Bold();
                                            t.Span(" extendido por el Registro Nacional de las Personas, como empleado de ");
                                            t.Span(sucursal).Bold();
                                            t.Span(" estoy de acuerdo con las siguientes declaraciones relacionadas con la prestación de una computadora para uso laboral y así mismo he aceptado seguir las Políticas de uso siguientes:");
                                        });
                                        //});

                                        // --- CUERPO ---

                                        col.Spacing(2);

                                        col.Item().Text("1. La computadora es propiedad de " + sucursal + " todos los derechos del equipo y del software.").Justify();
                                        col.Spacing(10);
                                        col.Item().Text("2. Acepto que soy completamente responsable de la computadora mientras está a mi cargo.").Justify();
                                        col.Item().Text("3. Yo estoy de acuerdo con seguir todas las regulaciones y políticas internas de " + sucursal + " con respecto al uso de las computadoras, el uso correcto de software y la información.").Justify();
                                        col.Item().Text("4. Yo estoy de acuerdo con no remover o alterar ninguna etiqueta de identificación que estén adjuntas o se muestren en la computadora o con no cambiar ninguna identificación propia del equipo.").Justify();
                                        col.Item().Text("5. Yo estoy de acuerdo con no eliminar ningún archivo, cotizaciones, bases de datos, carteras de clientes o software que puedan ser útiles para " + sucursal).Justify();
                                        col.Item().Text("6. Yo estoy de acuerdo con mantener la computadora y la información en un lugar seguro.").Justify();
                                        col.Item().Text("7. Yo estoy de acuerdo con reportar inmediatamente al área correspondiente en caso de daño de la computadora.").Justify();
                                        col.Item().Text("8. Estoy de acuerdo con devolver el equipo al momento que me retire de la empresa, deberé la computadora con su accesorios proporcionados al momento de la entrega.").Justify();
                                        col.Item().Text("9. Yo estoy de acuerdo con que todo el trabajo de reparación será completado por el Departamento de Informática autorizado por " + sucursal).Justify();
                                        col.Item().Text("10. Los costos asociados debido al abuso o uso indebido del equipo será mi responsabilidad.").Justify();
                                        col.Item().Text("11. No instalar programas no autorizados por Gerencia. Todo programa autorizado deberá ser instalado por el departamento de Informática.").Justify();
                                        col.Item().Text("12. No guardar archivos personales tales como imágenes, videos, música o documentos ajenos a " + sucursal + " informática tiene autorizado borrar tal información de ser necesario.").Justify();
                                        col.Item().PageBreak();
                                        col.Item().Text("13. De ser necesario el reemplazo por daño, el equipo a comprar, deberá tener las mismas características al equipo anterior (acorde a la versión tecnológica en el momento del reemplazo).").Justify();
                                        col.Item().Text("14. Tendré limpio el equipo, en todo momento, para evitar daños ocasionados por el medio ambiente.").Justify();

                                        col.Item().PaddingTop(10).Text("Yo he leído, entendido y acepto íntegramente todos los términos de este contrato, en utilizar la computadora como una herramienta estrictamente laboral.").Bold().Justify();

                                        // --- DATOS DEL EQUIPO ---
                                        col.Item().PaddingTop(15).Column(dataCol =>
                                        {
                                            dataCol.Item().Text("Los datos del equipo a recibir son:").Bold();
                                            dataCol.Item().Text(t => {
                                                t.Span("Marca: ").Bold(); t.Span("DELL  ");
                                                t.Span("Modelo: ").Bold(); t.Span("PRO 16  ");
                                                t.Span("Serie: ").Bold(); t.Span("98SD694");
                                            });
                                        });

                                        // --- OBSERVACIONES ---
                                        col.Item().PaddingTop(10).Text(t =>
                                        {
                                            t.Span("Observaciones: ").Bold();
                                            t.Span(observaciones ?? "_____________________________________________________________");
                                        });
                                        col.Item().Text("(Esto es por si el equipo tiene un defecto o daño antes de entregar)").FontSize(9).Italic();
                                    });

                                    // --- FIRMAS ---
                                    page.Footer().Row(row =>
                                    {
                                        row.RelativeItem().Column(sig =>
                                        {
                                            sig.Item().AlignCenter().Text("f._____________________________________");
                                            sig.Item().AlignCenter().Text("Firma del Empleado").Bold();
                                            sig.Item().AlignCenter().Text("William Rolando Recinos Pérez").FontSize(9);
                                        });

                                        row.ConstantItem(40);

                                        row.RelativeItem().Column(sig =>
                                        {
                                            sig.Item().AlignCenter().Text("f._____________________________________");
                                            sig.Item().AlignCenter().Text("Recibido / Informática").Bold();
                                            sig.Item().AlignCenter().Text(sucursal).FontSize(9);
                                        });
                                    });
                                });
                                break;
                        }

                        
                    });

                    pdfBytes = documento.GeneratePdf();



            // Devolvemos el archivo como un stream para que el JS lo reciba
            return File(pdfBytes, "application/pdf");
        }
    }
}
