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
    }
}
