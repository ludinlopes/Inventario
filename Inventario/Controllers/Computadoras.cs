using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Computadoras : Controller
    {
        public IActionResult Editar(string b)
        {
            ImComputadora emple = new ImComputadora();

            MComputadora a = emple.getEmple(b);
            return View(a);
        }

        public IActionResult Actualizar(MComputadora b)
        {
            
            ImComputadora compu = new ImComputadora();

            string a = compu.setComputadora(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }


        public IActionResult Nuevo(MComputadora b)
        {

            return View(b);
        }


        public IActionResult Insert(MComputadora b)
        {

            ImComputadora compu = new ImComputadora();
            MComputadora c = new MComputadora();
            c = b;
            c.RespuestaSql = compu.insert(b);

            return RedirectToAction("Nuevo", "Computadora", c);

        }
    }
}
