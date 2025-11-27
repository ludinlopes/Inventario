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

        public IActionResult viewItemEditIt(string idItem, string Tipo)
        {
            // 1. Determinar el controlador de destino (el que tiene la vista de edición)
            string controladorDestino = "Home"; // Valor predeterminado

            switch (Tipo)
            {
                case "Computadora":
                    controladorDestino = "Computadoras";
                    break;

                case "Celular":
                    controladorDestino = "Celular"; // Asumiendo que tienes un CelularesController
                    break;

                case "Impresora":
                    controladorDestino = "Impresora"; 
                    break;

                case "Monitor":
                    controladorDestino = "Monitor"; 
                    break;
                case "Scanner":
                    controladorDestino = "Scanner"; 
                    break;
                case "Tablet":
                    controladorDestino = "Tablet"; 
                    break;
                case "Telefono":
                    controladorDestino = "Telefono"; 
                    break;
                case "UPS":
                    controladorDestino = "UPS"; 
                    break;
                // Puedes agregar más casos aquí

                default:
                    // Si el tipo no se reconoce, podrías enviarlo a una página de error o al Home
                    controladorDestino = "Home";
                    break;
            }

            // 2. Redireccionar a la acción 'Editar' del controlador de destino
            // 🚨 CRUCIAL: Pasamos el ID del ítem (idItem) como parámetro de ruta llamado 'b'.
            // Esto hace que la URL sea: /Computadoras/Editar?b={idItem}
            return RedirectToAction("Editar", controladorDestino, new { b = idItem });
        }

    }
}
