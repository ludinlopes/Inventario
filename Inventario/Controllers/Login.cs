using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Login : Controller
    {
        public IActionResult VLogin()
        {
            //var a = new MLogin();
            //var b = new ImLogin();
            //a.Usuario = b.ConvertToMD5("Clipp3r##");
            return View();
        }

        public IActionResult Acceso(string Usuario, string Contraseña, string Sucursal)
        {
            HttpContext.Session.SetString("Sucursal", Sucursal);
            // Obtener la sucursal desde la sesión
            string sucursal = HttpContext.Session.GetString("Sucursal");
            // Pasar la sucursal como parámetro
           
            var a = new ImLogin();
            var b = a.getUsuario(Usuario,Contraseña);
            if(b == true)
            {
                return RedirectToAction("Menu", "Home");
            } else
            {
                Console.WriteLine("Hola");
                return RedirectToAction("VLogin", "Login");
                
            }
            
        }
    }
}

//pueba de comentario
