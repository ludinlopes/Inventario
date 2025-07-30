/*using Inventario.Models;
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
*/

// ... (tu código ImInvGeneral.cs) ...

using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType

namespace Inventario.ConexionDB.Consultas
{
    public class ConsultasDB
    {
        public string getNewNoInv(string tabla, string sucursal)
        {
            string NoInv = "";
            string procedimiento = "getNoInventario"; // Nombre del procedimiento almacenado

            try
            {
                using (Conexion cn = new Conexion())
                {
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            // Los nombres de los parámetros deben coincidir con los del SP
                            cmd.Parameters.AddWithValue("tabla", tabla);    // Usar 'p_tabla' como en el SP
                            cmd.Parameters.AddWithValue("sucursal", sucursal); // Usar 'p_sucursal' como en el SP

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    NoInv = reader.GetString("No_Inventario");
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error de base de datos al obtener nuevo número de inventario: {ex.Message} (Código: {ex.Number})");
                NoInv = "ERROR_DB";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al obtener nuevo número de inventario: {ex.Message}");
                NoInv = "ERROR_GENERAL";
            }

            return NoInv;
        }
    }
}