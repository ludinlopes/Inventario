using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Impresora : Controller
    {
        public IActionResult Editar(string b)
        {
            ImImpresora imp = new ImImpresora();

            MImpresora a = imp.getEmple(b);
            return View(a);
        }


        public IActionResult Actualizar(MImpresora b)
        {

            ImImpresora compu = new ImImpresora();

            string a = compu.setImpresora(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }
    }
}
