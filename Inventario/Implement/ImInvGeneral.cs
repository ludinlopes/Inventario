using Inventario.Controllers;
using Inventario.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImInvGeneral
    {

        private Conexion cn;
        public List<MInvGeneral> getInventario()
        {
            cn = new Conexion();
            var inv = new List<MInvGeneral>();
            

            MySqlDataReader mySqlDataReader;
            
            string consulta = $"call getInventario";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    var minv = new MInvGeneral();
                    

                    minv.Linea = mySqlDataReader.GetString("Linea");
                    minv.Cod_Empleado = mySqlDataReader.GetString("Cod_Empleado");
                    minv.Nombre = mySqlDataReader.GetString("Nombre");
                    minv.Area = mySqlDataReader.GetString("Area");
                    minv.Estado = mySqlDataReader.GetString("Estado");
                    minv.Sucursal = mySqlDataReader.GetString("Sucursal");
                    minv.Cod_Empleado_Com = mySqlDataReader.GetString("Cod_Empleado_Com");
                    minv.Estado_Com = mySqlDataReader.GetString("Estado_Com");
                    minv.No_Inventario_Com = mySqlDataReader.GetString("No_Inventario_Com");
                    minv.Tipo_Com = mySqlDataReader.GetString("Tipo_Com");
                    minv.Dominio_Com = mySqlDataReader.GetString("Dominio_Com");
                    minv.Usuario_Com = mySqlDataReader.GetString("Usuario_Com");
                    minv.Marca_Com = mySqlDataReader.GetString("Marca_Com");
                    minv.Modelo_Com = mySqlDataReader.GetString("Modelo_Com");
                    minv.Serie_Com = mySqlDataReader.GetString("Serie_Com");
                    minv.Procesador_Com = mySqlDataReader.GetString("Procesador_Com");
                    minv.Generacion_Com = mySqlDataReader.GetString("Generacion_Com");
                    minv.Tipo_Disco_Com = mySqlDataReader.GetString("Tipo_Disco_Com");
                    minv.Capacidad_Disco_Com = mySqlDataReader.GetString("Capacidad_Disco_Com");
                    minv.Ram_Com = mySqlDataReader.GetString("Ram_Com");
                    minv.Mac_Address_Com = mySqlDataReader.GetString("Mac_Address_Com");
                    minv.No_IP_Com = mySqlDataReader.GetString("No_IP_Com");
                    minv.Mouse_Com = mySqlDataReader.GetString("Mouse_Com");
                    minv.Teclado_Com = mySqlDataReader.GetString("Teclado_Com");
                    minv.Condicion_Com = mySqlDataReader.GetString("Condicion_Com");
                    minv.Fecha_Actualizacion_Com = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Mon = mySqlDataReader.GetString("Cod_Empleado_Mon");
                    minv.No_Inventario_Mon = mySqlDataReader.GetString("No_Inventario_Mon");
                    minv.Marca_Mon = mySqlDataReader.GetString("Marca_Mon");
                    minv.Modelo_Mon = mySqlDataReader.GetString("Modelo_Mon");
                    minv.Serie_Mon = mySqlDataReader.GetString("Serie_Mon");
                    minv.Estado_Mon = mySqlDataReader.GetString("Estado_Mon");
                    minv.Condicion_Mon = mySqlDataReader.GetString("Condicion_Mon");
                    minv.Fecha_Actualizacion_Mon = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Imp = mySqlDataReader.GetString("Cod_Empleado_Imp");
                    minv.No_Inventario_Imp = mySqlDataReader.GetString("No_Inventario_Imp");
                    minv.Marca_Imp = mySqlDataReader.GetString("Marca_Imp");
                    minv.Modelo_Imp = mySqlDataReader.GetString("Modelo_Imp");
                    minv.Serie_Imp = mySqlDataReader.GetString("Serie_Imp");
                    minv.Tipo_Imp = mySqlDataReader.GetString("Tipo_Imp");
                    minv.Estado_Imp = mySqlDataReader.GetString("Estado_Imp");
                    minv.Condicion_Imp = mySqlDataReader.GetString("Condicion_Imp");
                    minv.Fecha_Actualizacion_Imp = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Scn = mySqlDataReader.GetString("Cod_Empleado_Scn");
                    minv.No_Inventario_Scn = mySqlDataReader.GetString("No_Inventario_Scn");
                    minv.Marca_Scn = mySqlDataReader.GetString("Marca_Scn");
                    minv.Modelo_Scn = mySqlDataReader.GetString("Modelo_Scn");
                    minv.Serie_Scn = mySqlDataReader.GetString("Serie_Scn");
                    minv.Estado_Scn = mySqlDataReader.GetString("Estado_Scn");
                    minv.Condicion_Scn = mySqlDataReader.GetString("Condicion_Scn");
                    minv.Fecha_Actualizacion_Scn = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Ups = mySqlDataReader.GetString("Cod_Empleado_Ups");
                    minv.No_Inventario_Ups = mySqlDataReader.GetString("No_Inventario_Ups");
                    minv.Marca_Ups = mySqlDataReader.GetString("Marca_Ups");
                    minv.Estado_Ups = mySqlDataReader.GetString("Estado_Ups");
                    minv.Condicion_Ups = mySqlDataReader.GetString("Condicion_Ups");
                    minv.Fecha_Actualizacion_Ups = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Tel = mySqlDataReader.GetString("Cod_Empleado_Tel");
                    minv.Marca_Tel = mySqlDataReader.GetString("Marca_Tel");
                    minv.Modelo_Tel = mySqlDataReader.GetString("Modelo_Tel");
                    minv.Tipo_Tel = mySqlDataReader.GetString("Tipo_Tel");
                    minv.Estado_Tel = mySqlDataReader.GetString("Estado_Tel");
                    minv.Condicion_Tel = mySqlDataReader.GetString("Condicion_Tel");
                    minv.Fecha_Actualizacion_Tel = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Cel = mySqlDataReader.GetString("Cod_Empleado_Cel");
                    minv.Marca_Cel = mySqlDataReader.GetString("Marca_Cel");
                    minv.Modelo_Cel = mySqlDataReader.GetString("Modelo_Cel");
                    minv.IMEI_Cel = mySqlDataReader.GetString("IMEI_Cel");
                    minv.Estado_Cel = mySqlDataReader.GetString("Estado_Cel");
                    minv.Condicion_Cel = mySqlDataReader.GetString("Condicion_Cel");
                    minv.Fecha_Actualizacion_Cel = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                    minv.Cod_Empleado_Tab = mySqlDataReader.GetString("Cod_Empleado_Tab");
                    minv.Marca_Tab = mySqlDataReader.GetString("Marca_Tab");
                    minv.Modelo_Tab = mySqlDataReader.GetString("Modelo_Tab");
                    minv.IMEI_Tab = mySqlDataReader.GetString("IMEI_Tab");
                    minv.Estado_Tab = mySqlDataReader.GetString("Estado_Tab");
                    minv.Condicion_Tab = mySqlDataReader.GetString("Condicion_Tab");
                    minv.Fecha_Actualizacion_Tab = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();







                    inv.Add(minv);

                }
                mySqlCommand.Connection.Close();
            }
            return inv;
        }

    }

    
}
