
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Inventario.ConexionDB.ConexionSSH;
using Inventario.Implement; // Importar el servicio SSH

namespace Inventario.Controllers
{
    public class Home : Controller
    {



        public IActionResult Menu() {
            string sucursal = HttpContext.Session.GetString("Sucursal");
            if (sucursal == "RZ")
            {
                sucursal = "Ricza";
            }
            else if (sucursal == "INM")
            {
                sucursal = "Inmepro";
            }
            else if (sucursal == "SC")
            {
                sucursal = "Servicocinas";
            }
            else if (sucursal == "FES")
            {
                sucursal = "FES";
            }
            ViewBag.Sucursal = sucursal;

            return View(); 
        }
        public IActionResult Home_()
        {
            string sucursal = HttpContext.Session.GetString("Sucursal");
            MDocument a = new MDocument
            {
                controlador = "Home",
                metodo = "Consulta",
                Empleado = "chequed",
                Texto = "prueba"
            };

            if (sucursal == "RZ")
            {
                a.Sucursal = "Ricza";
            }else if (sucursal == "INM")
            {
                a.Sucursal = "Inmepro";
            }
            else if (sucursal == "SC")
            {
                a.Sucursal = "Servicocinas";
            }
            else if (sucursal == "FES")
            {
                a.Sucursal = "FES";
            }

            return View(a);
        }









        public IActionResult MenuInv_()
        {
            var cn = new ImInvGeneral();
                        
            String Id = HttpContext.Session.GetString("SucursalID");
                        
            var a = cn.getListInv(Id, "Todo");

            var o = new MInvListado();

            o.Items = a;

            return View(o);

        }





        [HttpPost]
        public ActionResult Consulta(string fav_language/*,string Sucursal*/, string Texto)
        {

            //HttpContext.Session.SetString("Sucursal", Sucursal);
            MDocument a = new MDocument();
            MDocument c= new MDocument();
            if (fav_language == "Empleado") 
            {
                a.Texto = "unchecked";
                return RedirectToAction("editar", "Empleado", new { b = Texto });


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

            else if (fav_language == "Tablet")
            {
                return RedirectToAction("Editar", "Tablet", new { b = Texto });
            }


            else
            {
                return View("Home_", c);
            }


            //return RedirectToAction("OtraAccion");
            
        }

        public void  GetSucursal() {
            string sucursal = HttpContext.Session.GetString("Sucursal");
            sucursal = "No definido";
            if (sucursal == "RZ")
            {
                sucursal = "Ricza";
            }
            else if (sucursal == "INM")
            {
                sucursal = "Inmepro";
            }
            else if (sucursal == "SC")
            {
                sucursal = "Servicocinas";
            }
            else if (sucursal == "FES")
            {
                sucursal = "FES";
            }
            ViewBag.Sucursal = sucursal;
        }

        [HttpGet]
        public IActionResult GetFilasGeneral(string sucursal, string tipo)
        {
            // 1. Usar tu método existente para obtener los datos
            var cn = new ImInvGeneral();
            var items = cn.getListInv(sucursal,tipo); // Usa la lógica que ya tienes

            // 2. Devolver una vista parcial con el modelo
            // La vista parcial solo contendrá el ciclo @foreach
            return PartialView("_FilasInventarioGeneral", items);
        }

        
    }
}
