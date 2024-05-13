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

    }
}
