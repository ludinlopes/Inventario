using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Scanner : Controller
    {
        public IActionResult Editar(string b)
        {
            ImScanner mon = new ImScanner();

            MScanner a = mon.getScannerByNoInv(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        //public IActionResult Actualizar(MScanner b)
        //{

        //    ImScanner compu = new ImScanner();

        //    string a = compu.setScanner(b);
        //    MRespuestaDB resp = new MRespuestaDB();
        //    resp.respuesta = a;
        //    return View(resp);
        //}
        [HttpPost]
        public IActionResult Update([FromBody] MScanner b)
        {

            ImScanner compu = new ImScanner();
            string a = compu.setScanner(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return Ok(resp.respuesta);

        }

        public IActionResult Nuevo(MScanner b)
        {
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("SCN", HttpContext.Session.GetString("Sucursal"));
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        //public IActionResult Insert(MScanner b)
        //{

        //    ImScanner compu = new ImScanner();
        //    MScanner c = new MScanner();
        //    c = b;
        //    c.RespuestaSql = compu.insert(b);

        //    return RedirectToAction("Nuevo", "Scanner", c);

        //}

        [HttpPost]
        public IActionResult Insert([FromBody] MScanner b)
        {

            ImScanner Scanner = new ImScanner();
            MScanner c = new MScanner();
            c = b;
            c.RespuestaSql = Scanner.insert(b);
            MInvListado inv = new MInvListado();

            var g = c.RespuestaSql;


            return Ok(g);
        }


        [HttpGet]
        public IActionResult GetNewItemView()
        {

            MScanner b = new MScanner();
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("SCN", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();
            ViewBag.accionScn = "Save()";
            b.Empleados = c.getEmpleados();
            return PartialView("_Nuevo", b);
        }



        [HttpGet]
        public IActionResult GetEditItemView(string noInventario)
        {

            ImScanner Scanner = new ImScanner();
            MScanner b = Scanner.getScannerByNoInv(noInventario);
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            ViewBag.accionScn = "Update()";
            return PartialView("_Nuevo", b);

        }



        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("SCN", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }
    }
}
