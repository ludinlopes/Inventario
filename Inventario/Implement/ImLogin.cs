using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImLogin
    {
        private Conexion cn;
        public Boolean getUsuario(string us, string pass)
        {
            cn = new Conexion();
            var respuesta = false;


            MySqlDataReader mySqlDataReader;

            string consulta = $"SELECT * FROM Usuario WHERE Usuario = '{us}' AND Password = '{pass}';";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    var g = new MLogin();

                    g.Usuario = mySqlDataReader.GetString("Usuario");
                    g.Password = mySqlDataReader.GetString("Password");

                    if (g.Usuario == us && g.Password == pass)
                    {
                        respuesta = true;
                    } else
                    {
                        respuesta = false;
                    }
                    
                }
                mySqlCommand.Connection.Close();
            }
            return respuesta;
        }


    }
}
