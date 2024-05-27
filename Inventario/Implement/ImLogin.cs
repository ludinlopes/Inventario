using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Inventario.Implement
{
    public class ImLogin
    {
        private Conexion cn;
        public Boolean getUsuario(string us, string pass)
        {
            cn = new Conexion();
            var respuesta = false;
            string p = ConvertToMD5(pass);
            
            MySqlDataReader mySqlDataReader;

            string consulta = $"SELECT * FROM Usuario WHERE Usuario = '{us}' AND Password = '{p}';";
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

                    if (g.Usuario == us && g.Password == p)
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

        public string ConvertToMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertir los bytes del hash a un string hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // X2 para formato hexadecimal en mayúsculas
                }
                return sb.ToString();
            }
        }


    }
}
