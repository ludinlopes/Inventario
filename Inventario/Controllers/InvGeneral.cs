using Inventario.Implement;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class InvGeneral : Controller
    {
        public IActionResult Vista()
        {
            var cn = new ImInvGeneral();
            var a = cn.getInventario();
            return View(a);
        }
    }
}
