using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Usuarios : Controller
    {
        public IActionResult Editar()
        {
            return View();
        }
    }
}
