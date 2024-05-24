using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Monitor : Controller
    {
        public IActionResult Editar(string b)
        {
            ImMonitor mon = new ImMonitor();

            MMonitor a = mon.getEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        public IActionResult Actualizar(MMonitor b)
        {

            ImMonitor compu = new ImMonitor();

            string a = compu.setMonitor(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MMonitor b)
        {

            return View(b);
        }


        public IActionResult Insert(MMonitor b)
        {

            ImMonitor compu = new ImMonitor();
            MMonitor c = new MMonitor();
            c = b;
            c.RespuestaSql = compu.insert(b);

            return RedirectToAction("Nuevo", "Monitor", c);

        }
    }
}
