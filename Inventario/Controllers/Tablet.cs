using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Tablet : Controller
    {
        public IActionResult Editar(string b)
        {
            ImTablet mon = new ImTablet();

            MTablet a = mon.getTabletByIMEI(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        public IActionResult Actualizar(MTablet b)
        {

            ImTablet compu = new ImTablet();

            string a = compu.setTablet(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MTablet b)
        {
            var c = new ImEmpleado();
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        public IActionResult Insert(MTablet b)
        {

            ImTablet compu = new ImTablet();
            MTablet c = new MTablet();
            c = b;
            c.RespuestaSql = compu.insertTablet(b); ;

            return RedirectToAction("Nuevo", "Celular", c);

        }



        [HttpGet]
        public IActionResult GetNewItemView()
        {

            MTablet b = new MTablet();

            return PartialView("_Nuevo", b);
        }



        [HttpGet]
        public IActionResult GetNewNoInv()
        {
            var h = new ConsultasDB();
            var b = h.getNewNoInv("TBL", HttpContext.Session.GetString("Sucursal"));

            return Ok(b);
        }
    }
}
