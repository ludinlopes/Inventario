using Microsoft.AspNetCore.Mvc;
using Inventario.Models;
using Renci.SshNet;

namespace Inventario.Controllers
{
    public class SshController : Controller
    {
        public ActionResult Index()
        {
            return View(new SshConnectionModel());
        }

        [HttpPost]
        public JsonResult EjecutarComando([FromBody] SshConnectionModel data)
        {
            var resultado = "";

            // Validar que los valores no sean nulos o vacíos
            if (string.IsNullOrEmpty(data.IP) || data.Puerto <= 0 || string.IsNullOrEmpty(data.Usuario) || string.IsNullOrEmpty(data.Clave) || string.IsNullOrEmpty(data.Comando))
            {
                return Json(new { resultado = "Error: Uno o más parámetros están vacíos o nulos." });
            }

            try
            {
                using (var client = new SshClient(data.IP, data.Puerto, data.Usuario, data.Clave))
                {
                    client.Connect();

                    if (client.IsConnected)
                    {
                        var cmd = client.RunCommand(data.Comando);
                        resultado = cmd.Result;
                        client.Disconnect();
                    }
                    else
                    {
                        resultado = "No se pudo conectar al servidor SSH.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = $"Error: {ex.Message}";
            }

            return Json(new { resultado });
        }

    }
}
