using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

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


        public IActionResult Actualizar(MUps b)
        {

            ImUps compu = new ImUps();

            string a = compu.setUps(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MUps b)
        {
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("UPS", HttpContext.Session.GetString("Sucursal"));
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        public IActionResult Insert (MUps b)
        {

            ImUps compu = new ImUps();
            MUps c = new MUps();
            c = b;
            c.RespuestaSql = compu.insertUps(b);
            
            return RedirectToAction("Nuevo", "UPS",c);

        }


        [HttpGet]
        public IActionResult GetNewItemView()
        {

            MUps b = new MUps();

            return PartialView("_Nuevo", b);
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
