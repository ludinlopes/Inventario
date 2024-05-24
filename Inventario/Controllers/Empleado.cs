using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Empleado : Controller
    {
        public IActionResult Editar(string b)
        {
            ImEmpleado emple = new ImEmpleado();
            
            MEmpleado a = emple.getEmple(b);
            var c = new ImEmpleado();
            a.Empleados = c.getEmpleados();
            return View(a);
        }


        public IActionResult Actualizar(MEmpleado b)
        {

            ImEmpleado compu = new ImEmpleado();

            string a = compu.setEmpleado(b);
            MRespuestaDB resp = new MRespuestaDB();
            resp.respuesta = a;
            return View(resp);
        }

        public IActionResult Nuevo(MEmpleado b)
        {

            return View(b);
        }


        public IActionResult Insert(MEmpleado b)
        {

            ImEmpleado compu = new ImEmpleado();
            MEmpleado c = new MEmpleado();
            c = b;
            c.RespuestaSql = compu.insert(b);

            return RedirectToAction("Nuevo", "Empleado", c);

        }
    }
}
