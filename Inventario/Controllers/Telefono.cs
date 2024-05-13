using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Telefono : Controller
    {
        public IActionResult Editar(string b)
        {
            ImTelefono ups = new ImTelefono();

            MTelefono a = ups.getEmple(b);
            return View(a);
        }


        public IActionResult Actualizar(MTelefono b)
        {

            ImTelefono compu = new ImTelefono();

            string a = compu.setTelefono(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MTelefono b)
        {

            return View(b);
        }


        public IActionResult Insert(MTelefono b)
        {

            ImTelefono compu = new ImTelefono();
            MTelefono c = new MTelefono();
            c = b;
            c.RespuestaSql = compu.insert(b); ;

            return RedirectToAction("Nuevo", "Telefono", c);

        }
    }
}
