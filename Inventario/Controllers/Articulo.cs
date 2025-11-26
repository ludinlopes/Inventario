using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Articulo : Controller
    {
        public IActionResult Articulo_()
        {
            //return RedirectToAction("VLogin", "Login");
            return View();
        }

        public IActionResult viewItemEditIt(string idItem, string Tipo) {

            switch (Tipo)
            {
                case "Computadora":

                    ImComputadora emple = new ImComputadora();

                    MComputadora a = emple.getEmple(b);
                    var c = new ImEmpleado();
                    a.Empleados = c.getEmpleados();


                    break;
                case "2":
                    ViewBag.Tipo = "Servicio";
                    break;
                case "3":
                    ViewBag.Tipo = "Paquete";
                    break;
                default:
                    ViewBag.Tipo = "Articulo";
                    break;
            }
            return RedirectToAction("VLogin", "Login");
        }

    }
}
