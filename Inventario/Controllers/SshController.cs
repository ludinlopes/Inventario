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
            var resultado = new Dictionary<string, string>();

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

                    //if (client.IsConnected)
                    //{
                    //    var cmd = client.RunCommand(data.Comando);
                    //    resultado = cmd.Result;
                    //    client.Disconnect();
                    //}
                    if (client.IsConnected)
                    {
                        resultado["MarcaModelo"] = client.RunCommand("wmic computersystem get manufacturer, model").Result;
                        resultado["Serie"] = client.RunCommand("wmic bios get serialnumber").Result;
                        resultado["Procesador"] = client.RunCommand("wmic cpu get name").Result;
                        resultado["Discos"] = client.RunCommand("wmic diskdrive get model, mediaType, size").Result;
                        resultado["RAM"] = client.RunCommand("systeminfo | findstr /C:\"Total Physical Memory\"").Result;
                        resultado["MAC"] = client.RunCommand("getmac").Result;
                        resultado["NombrePC"] = client.RunCommand("hostname").Result;
                        resultado["Dominio"] = client.RunCommand("systeminfo | findstr /C:\"Domain\"").Result;
                        resultado["Teclado"] = client.RunCommand("wmic path Win32_Keyboard get DeviceID").Result;
                        resultado["Mouse"] = client.RunCommand("wmic path Win32_PointingDevice get DeviceID").Result;

                        client.Disconnect();
                    }
                    else
                    {
                        return Json(new { error = "No se pudo conectar al servidor SSH." });
                    }
                }
            }
            catch (Exception ex)
            {
                resultado["Error"] = $"Error: {ex.Message}";
            }

            return Json(new { resultado });
        }

    }
}
