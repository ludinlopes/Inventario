using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    public class Pedido : Controller
    {
        // GET: Pedido
        public ActionResult Editar()
        {
            return View();
        }

    }
}
