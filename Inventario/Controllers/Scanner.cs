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

            MScanner a = mon.getEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        public IActionResult Actualizar(MScanner b)
        {

            ImScanner compu = new ImScanner();

            string a = compu.setScanner(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MScanner b)
        {

            return View(b);
        }


        public IActionResult Insert(MScanner b)
        {

            ImScanner compu = new ImScanner();
            MScanner c = new MScanner();
            c = b;
            c.RespuestaSql = compu.insert(b);

            return RedirectToAction("Nuevo", "Scanner", c);

        }
    }
}
