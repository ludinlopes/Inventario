using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home_()
        {
            MDocument a = new MDocument();
            a.controlador = "Home";
            a.metodo = "Consulta";
            a.Empleado = "chequed";
            a.Texto = "prueba";
            return View(a);
        }


        [HttpPost]
        public ActionResult Consulta(string fav_language, string Texto)
        {
            MDocument a = new MDocument();
            MDocument c= new MDocument();
            if (fav_language == "Empleado") 
            {
                a.Texto = "unchecked";
                return RedirectToAction("editar", "Empleado", new { b = int.Parse(Texto) });


            } else if (fav_language == "Inventario General")
            {
                return RedirectToAction("Vista", "InvGeneral");
            }


            else if (fav_language == "Computadora")
            {
                
                return RedirectToAction("editar", "Computadoras", new { b = Texto });
            }


            else if (fav_language == "Monitor")
            {
                Monitor mon = new Monitor();

                return RedirectToAction("editar", "Monitor", new { b = Texto });
                //return RedirectToAction();
            }


            else if (fav_language == "Impresora")
            {
                return RedirectToAction("editar", "Impresora", new { b = Texto });
            }


            else if (fav_language == "Scanner")
            {
                return RedirectToAction("editar", "Scanner", new { b = Texto });
            }


            else if (fav_language == "UPS")
            {
                return RedirectToAction("editar", "UPS", new { b = Texto });
            }


            else if (fav_language == "Telefono")
            {
                return RedirectToAction("editar", "Telefono", new { b = Texto });
            }


            else if (fav_language == "Celular")
            {
                return RedirectToAction("editar", "Celular", new { b = Texto });
            }


            else
            {
                return View("Home_", c);
            }


            //return RedirectToAction("OtraAccion");
            
        }




    }
}
