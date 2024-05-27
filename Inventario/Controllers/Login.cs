using Inventario.Implement;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Login : Controller
    {
        public IActionResult VLogin()
        {

            return View();
        }

        public IActionResult Acceso(string Usuario, string Contraseña, string Sucursal)
        {
            HttpContext.Session.SetString("Sucursal", Sucursal);
            var a = new ImLogin();
            var b = a.getUsuario(Usuario,Contraseña);
            if(b == true)
            {
                return RedirectToAction("Home_", "Home");
            } else
            {
                return RedirectToAction("VLogin", "Login");
            }
            
        }
    }
}
