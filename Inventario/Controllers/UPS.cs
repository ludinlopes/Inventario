using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

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
    }
}
