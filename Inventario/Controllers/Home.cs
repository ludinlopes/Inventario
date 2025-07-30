
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Inventario.ConexionDB.ConexionSSH; // Importar el servicio SSH

namespace Inventario.Controllers
{
    public class Home : Controller
    {
        /*private readonly SshService _sshService;

        public Home(SshService sshService) // Inyección de dependencias
        {
            _sshService = sshService;
        }

        public IActionResult EjecutarComandoSSH()
        {
            string resultado = _sshService.EjecutarComando("dir"); // Comando en Windows
            ViewBag.Resultado = resultado;
            //  return Content("La acción está funcionando, pero la vista no se encontró.");
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
        ////desde aca estaba activado lo comente el 28 -7 -25 a las 13:54
        
         private static SshService sshService = new SshService();

        public IActionResult IniciarSesionSSH()
        {
            sshService.Conectar();
            ViewBag.Mensaje = "Conectado a PowerShell.";
            return View();
        }

        public IActionResult EjecutarComando(string comando)
        {
            if (string.IsNullOrEmpty(comando))
                return Json(new { resultado = "Ingrese un comando válido." });

            string resultado = sshService.EjecutarComandoInteractivo(comando);
            return Json(new { resultado });
        }

        public IActionResult CerrarSesionSSH()
        {
            sshService.Desconectar();
            ViewBag.Mensaje = "Desconectado de PowerShell.";
            return View();
        }

        */


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


    }
}
