using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Login : Controller
    {
        public IActionResult VLogin()
        {
            return View();
        }
    }
}
