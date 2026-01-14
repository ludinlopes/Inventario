namespace Inventario.Models
{
    public class MEmpleado
    {
        public string Cod_Emple { get; set; }
        public string Nombre { get; set; }
        public string Area { get; set; }
        public string Estado { get; set; }
        public string Sucursal { get; set; }
        public string Texto { get; set; }
        public string RespuestaSql { get; set; }
        public string? Nombre_Sucursal { get; set; }
        public string? Noidentificacion { get; set; }

        public List<MEmpleado> Empleados { get; set; }
    }
}
