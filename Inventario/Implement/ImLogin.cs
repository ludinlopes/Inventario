using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data; // Necesario para CommandType

namespace Inventario.Implement
{
    public class ImLogin
    {
        // El campo 'private Conexion cn;' se elimina, cada método gestionará su propia conexión.

        public bool getUsuario(string us, string pass)
        {
            var respuesta = false;
            string hashedPassword = ConvertToMD5(pass); // La contraseña ya viene hasheada

            // Nombre del procedimiento almacenado que crearemos en MySQL
            string procedimiento = "AuthenticateUser";

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión explícitamente

                        // Envuelve el MySqlCommand en un 'using'.
                        using (MySqlCommand mySqlCommand = new MySqlCommand(procedimiento, conn))
                        {
                            // Indicamos que estamos ejecutando un procedimiento almacenado
                            mySqlCommand.CommandType = CommandType.StoredProcedure;

                            // AÑADIR PARÁMETROS DE FORMA SEGURA
                            // Los nombres de los parámetros deben coincidir con los del procedimiento almacenado
                            mySqlCommand.Parameters.AddWithValue("_username", us);
                            mySqlCommand.Parameters.AddWithValue("_password", hashedPassword);

                            // Envuelve el MySqlDataReader en un 'using'.
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            {
                                // Si mySqlDataReader.Read() devuelve true, significa que se encontró una fila
                                // con el usuario y la contraseña hasheada, por lo tanto, la autenticación fue exitosa.
                                if (mySqlDataReader.Read())
                                {
                                    respuesta = true;
                                }
                                else
                                {
                                    respuesta = false; // No se encontraron coincidencias
                                }

                            } // mySqlDataReader se cierra y se dispone aquí.
                        } // mySqlCommand se cierra y se dispone aquí.
                    } // conn (MySqlConnection) se cierra y se dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar iniciar sesión: {ex.Message}");
                // En un escenario real, podrías registrar este error.
                respuesta = false;
            }
            return respuesta;
        }

        public string ConvertToMD5(string input)
        {
            // Este método está correctamente implementado y ya usa 'using' para MD5.
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}