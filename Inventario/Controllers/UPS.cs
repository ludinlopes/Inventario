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

            MUps a = ups.getEmple(b);
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
            
            return View(b);
        }


        public IActionResult Insert (MUps b)
        {

            ImUps compu = new ImUps();
            MUps c = new MUps();
            c = b;
            c.RespuestaSql = compu.insert(b);
            
            return RedirectToAction("Nuevo", "UPS",c);

        }
    }
}
