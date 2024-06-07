using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Articulo : Controller
    {
        public IActionResult Articulo_()
        {
            return View();
        }
    }
}
