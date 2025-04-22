namespace Inventario.Models
{
    public class SshConnectionModel
    {
        public string? IP { get; set; }
        public int Puerto { get; set; } = 5512;
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public string? Comando { get; set; }
        public string? Resultado { get; set; }
    }
}
