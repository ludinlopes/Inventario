
using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Celular : Controller
    {
        public IActionResult Editar(string b)
        {
            ImCelular mon = new ImCelular();

            MCelular a = mon.getEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }

        
        public IActionResult Actualizar(MCelular b)
        {

            ImCelular compu = new ImCelular();

            string a = compu.setCelular(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MCelular b)
        {
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        public IActionResult Insert(MCelular b)
        {

            ImCelular compu = new ImCelular();
            MCelular c = new MCelular();
            c = b;
            c.RespuestaSql = compu.insert(b); ;

            return RedirectToAction("Nuevo", "Celular", c);

        }


        [HttpGet]
        public IActionResult GetNewItemView()
        {

            MCelular b = new MCelular();
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("CEL", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();

            b.Empleados = c.getEmpleados();
            return PartialView("_Nuevo", b);
        }


        


        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("CEL", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }

    }
}
