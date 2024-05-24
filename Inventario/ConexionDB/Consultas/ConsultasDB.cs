using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.ConexionDB.Consultas
{
    public class ConsultasDB
    {


        private Conexion cn;
        public string getNewNoInv(string tabla, string sucursal)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            var NoInv = "";
            
            string consulta = $"CALL getNoInventario('{tabla}','{sucursal}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    
                    NoInv = mySqlDataReader.GetString("No_Inventario");
                   

                }
                mySqlCommand.Connection.Close();
            }
            return NoInv;
        }


    }

    


}
