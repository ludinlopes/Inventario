/*using System;
using System.Text;
using Renci.SshNet;

namespace Inventario.ConexionDB.ConexionSSH
{
    public class SshService
    {
        private string servidor = "192.168.5.204"; // IP del servidor
        private string usuario = "MANAGER";
        private string clave = "Clipp3r";
        private int puerto = 3223;
     
        public string EjecutarComando(string comando)
        {
            using (var client = new SshClient(servidor, puerto, usuario, clave))
            {
                try
                {
                    client.Connect();
                    var cmd = client.RunCommand(comando);
                    client.Disconnect();
                    return cmd.Result;
                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message;
                }
            }
        }
    }
}
*/

using System;
using System.Text;
using Renci.SshNet;

namespace Inventario.ConexionDB.ConexionSSH
{
    public class SshService
    {
        private string servidor = "192.168.5.204";
        private string usuario = "MANAGER";
        private string clave = "Clipp3r";
        private int puerto = 3223;
        private SshClient client;
        private ShellStream shellStream;

        public SshService()
        {
            client = new SshClient(servidor, puerto, usuario, clave);
        }

        public void Conectar()
        {
            try
            {
                client.Connect();
                shellStream = client.CreateShellStream("PowerShell", 80, 24, 800, 600, 1024);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar: " + ex.Message);
            }
        }

        public string EjecutarComandoInteractivo(string comando)
        {
            if (shellStream == null)
                throw new Exception("Debe conectar primero.");

            shellStream.WriteLine(comando);
            shellStream.Flush();
            return LeerRespuesta();
        }

        private string LeerRespuesta()
        {
            StringBuilder resultado = new StringBuilder();
            string respuesta;
            while ((respuesta = shellStream.ReadLine()) != null)
            {
                resultado.AppendLine(respuesta);
                if (respuesta.Trim().EndsWith(">")) // Indica que PowerShell está esperando comando
                    break;
            }
            return resultado.ToString();
        }

        public void Desconectar()
        {
            if (client.IsConnected)
            {
                shellStream.Close();
                client.Disconnect();
            }
        }
    }
}
