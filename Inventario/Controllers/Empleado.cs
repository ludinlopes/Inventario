using Inventario.Implement;
using Inventario.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Empleado : Controller
    {
        public IActionResult Editar(int b)
        {
            ImEmpleado emple = new ImEmpleado();
            
            MEmpleado a = emple.getEmple(b);
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
    }
}
