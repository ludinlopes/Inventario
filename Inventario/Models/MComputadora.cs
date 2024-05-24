namespace Inventario.Models
{
    public class MComputadora
    {
        public string Cod_Emple { get; set; }
        public string Nombre { get; set; }
        public string NoInmentario { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Procesador { get; set; }
        public string Generacion { get; set; }
        public string TipoDisco { get; set; }
        public string CapacidadDisco { get; set; }
        public string Ram { get; set; }
        public string MacAddress { get; set; }
        public string NoIp { get; set; }
        public string Teclado { get; set; }
        public string Mouse { get; set; }
        public string NombrePc { get; set; }
        public string Dominio { get; set; }
        public string Usuario { get; set; }
        public string Contra { get; set; }
        public string Estado { get; set; }
        public string Condicion { get; set; }
        public string RespuestaSql { get; set; }
        /// <summary>
        /// ///
        /// </summary>
        public string Texto { get; set; }

        public List<MEmpleado> Empleados { get; set; }



    }
}
