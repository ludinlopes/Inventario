using Inventario.ConexionDB.Consultas;
using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Computadora : Controller
    {
        
        
        public IActionResult Editar(string b)
        {
            ImComputadora emple = new ImComputadora();

            MComputadora a = emple.getEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }

        public IActionResult Actualizar(MComputadora b)
        {
            
            ImComputadora compu = new ImComputadora();

            string a = compu.setComputadora(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }


        public IActionResult Nuevo(MComputadora b)
        {
            var h = new ConsultasDB();
            b.No_Inventario = h.getNewNoInv("CPU", HttpContext.Session.GetString("Sucursal"));

            var c = new ImEmpleado();
            
            b.Empleados = c.getEmpleados();
            return View(b);
        }


        public IActionResult Insert(MComputadora b)
        {

            ImComputadora compu = new ImComputadora();
            MComputadora c = new MComputadora();
            c = b;
            c.RespuestaSql = compu.insert(b);

            return RedirectToAction("Nuevo", "Computadora", c);

        }
        [HttpGet]
        public IActionResult GetNewItemView()
        {
            
            MComputadora b = new MComputadora();
            
            return PartialView("_Nuevo", b);
        }
    }
}
