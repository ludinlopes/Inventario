namespace Inventario.Models
{
    public class MUps
    {
        public string Cod_Emple { get; set; }
        public string Nombre { get; set; }
        public string No_Inventario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Estado { get; set; }
        public string Condicion { get; set; }
        public string Texto { get; set; }

        public string RespuestaSql { get; set; }
        public List<MEmpleado> Empleados { get; set; }
    }
}
