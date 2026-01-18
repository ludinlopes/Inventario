using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
using System.Reflection.Metadata;



using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Inventario.Controllers
{
    public class UPS : Controller
    {
        public IActionResult Editar(string b)
        {
            ImUps ups = new ImUps();

            MUps a = ups.getUpsByNoInv(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        

        public IActionResult Nuevo(MUps b)
        {
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("UPS", HttpContext.Session.GetString("Sucursal"));
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        //public IActionResult Insert (MUps b)
        //{

        //    ImUps compu = new ImUps();
        //    MUps c = new MUps();
        //    c = b;
        //    c.RespuestaSql = compu.insertUps(b);
            
        //    return RedirectToAction("Nuevo", "UPS",c);

        //}



        [HttpPost]
        public IActionResult Insert([FromBody] MUps b)
        {

            ImUps Ups = new ImUps();
            MUps c = new MUps();
            c = b;
            c.RespuestaSql = Ups.insertUps(b);
            MInvListado inv = new MInvListado();

            var g = c.RespuestaSql;


            return Ok(g);
        }


        [HttpGet]
        public IActionResult GetNewItemView()
        {

            MUps b = new MUps();
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("UPS", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();

            b.Empleados = c.getEmpleados();
            ViewBag.accionUps = "Save()";
            return PartialView("_Nuevo", b);
        }
        //GetEditItemView


        [HttpGet]
        public IActionResult GetEditItemView(string noInventario)
        {

            ImUps ups = new ImUps();
            MUps b = ups.getUpsByNoInv(noInventario);
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            ViewBag.accionUps = "Update()";
            return PartialView("_Nuevo", b);

        }




        //public IActionResult Actualizar(MUps b)
        //{

        //    ImUps compu = new ImUps();

        //    string a = compu.setUps(b);
        //    MRespuestaDB resp = new MRespuestaDB();
        //    resp.respuesta = a;
        //    return View(resp);
        //}


        [HttpPost]
        public IActionResult Update([FromBody] MUps b)
        {

            ImUps UPS = new ImUps();
            MRespuestaDB resp = new MRespuestaDB();

            string a = UPS.setUps(b);
            resp.respuesta = a;
            return Ok(resp.respuesta);

        }

        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("UPS", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }





        [HttpGet]
        public IActionResult GenerarPdfEntrega(string codigo, string observaciones)
        {
            // 1. Aquí simularías traer los datos de tu base de datos
            // var item = _db.Inventario.First(x => x.Codigo == codigo);

            // 2. Crear el PDF
            var documento = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(1, Unit.Centimetre);
                    page.Header().Text("COMPROBANTE DE ENTREGA").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Código de Inventario: {codigo}").Bold();
                        col.Item().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}");
                        col.Item().PaddingTop(10).LineHorizontal(1);

                        col.Item().PaddingTop(10).Text("Observaciones del Técnico:").Underline();
                        col.Item().Text(observaciones ?? "Sin observaciones.");

                        col.Item().PaddingTop(50).AlignRight().Text("_______________________");
                        col.Item().AlignRight().PaddingRight(20).Text("Firma de Recibido");
                    });
                });
            });

            byte[] pdfBytes = documento.GeneratePdf();

            // Devolvemos el archivo como un stream para que el JS lo reciba
            return File(pdfBytes, "application/pdf");
        }
    



}
}
