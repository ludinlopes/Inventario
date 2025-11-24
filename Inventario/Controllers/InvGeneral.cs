using Inventario.Implement;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class InvGeneral : Controller
    {
        public IActionResult Vista()
        {
            string sucursal = HttpContext.Session.GetString("SucursalAbrev");

            if (sucursal == "RZ")
            {
                sucursal = "RICZA";
            }
            else if (sucursal == "INM")
            {
                sucursal = "INMEPRO";
            }
            else if (sucursal == "SC")
            {
                sucursal = "SERVICOCINAS";
            }
            else if (sucursal == "FES")
            {
                sucursal = "FES";
            }
            // Ya tienes la sucursal disponible aquí
            Console.WriteLine("Sucursal: " + sucursal);

            //string consulta = $"CALL getInventarioPorSucursal('{sucursal}')";
            var cn = new ImInvGeneral();
            var a = cn.getInventario(sucursal);
            return View(a);
        }
    }
}
