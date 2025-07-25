using Inventario.Implement;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class InvGeneral : Controller
    {
        public IActionResult Vista()
        {
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
            // Ya tienes la sucursal disponible aquí
            Console.WriteLine("Sucursal: " + sucursal);

            //string consulta = $"CALL getInventarioPorSucursal('{sucursal}')";
            var cn = new ImInvGeneral();
            var a = cn.getInventario();
            return View(a);
        }
    }
}
