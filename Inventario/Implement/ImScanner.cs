using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImScanner
    {

        private Conexion cn;
        public MScanner getEmple(string NoInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MScanner scn = new MScanner();
            MySqlDataReader DR = null;
            string consulta = $"CALL getScanner('{NoInv}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    scn.Cod_Emple = cod.ToString();
                    scn.Nombre = mySqlDataReader.GetString("Nombre");
                    scn.NoImventario = mySqlDataReader.GetString("No_Inventario");
                    scn.Marca = mySqlDataReader.GetString("Marca");
                    scn.Modelo = mySqlDataReader.GetString("Modelo");
                    scn.Serie = mySqlDataReader.GetString("Serie");
                    
                    scn.Estado = mySqlDataReader.GetString("Estado");

                    scn.Condicion = mySqlDataReader.GetString("Condicion");
                }
                mySqlCommand.Connection.Close();
            }
            return scn;
        }




        public string setScanner(MScanner modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Scanner set " +
                $"Cod_Empleado = {modelo.Cod_Emple}" +
                $",Marca = '{modelo.Marca.ToUpper()}'" +
                $",Modelo = '{modelo.Modelo.ToUpper()}'" +
                $",Serie = '{modelo.Serie.ToUpper()}'" +
                $", Estado = '{modelo.Estado.ToUpper()}'" +
                $", Condicion = '{modelo.Condicion.ToUpper()}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $"WHERE No_Inventario = '{modelo.NoImventario}'";
                
               


            try
            {
                if (cn.OpenConnection() != null)
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(consulta, cn.OpenConnection());
                    mySqlCommand.Connection.Open();
                    // Ejecutar la consulta de actualización
                    int rowsAffected = mySqlCommand.ExecuteNonQuery();

                    // Cerrar la conexión
                    cn.CloseConnection();

                    // Devolver el resultado
                    return $"Filas afectadas: {rowsAffected}";
                }
                else
                {
                    return "No se pudo abrir la conexión a la base de datos";
                }
            }
            catch (Exception ex)
            {
                cn.CloseConnection();
                return $"Error: {ex.Message}";
            }
        }

        public string insert(MScanner modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Scanner VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.NoImventario.ToUpper()}'" +
                    $", '{modelo.Marca.ToUpper()}'" +
                    $", '{modelo.Modelo.ToUpper()}'" +
                    $", '{modelo.Serie.ToUpper()}'" +
                    $", '{modelo.Estado.ToUpper()}'" +
                    $", '{modelo.Condicion.ToUpper()}'" +
                    $", CURDATE() )";


            Console.WriteLine(consulta);
            try
            {
                if (cn.OpenConnection() != null)
                {
                    MySqlCommand mySqlCommand = new MySqlCommand(consulta, cn.OpenConnection());
                    mySqlCommand.Connection.Open();
                    // Ejecutar la consulta de actualización
                    int rowsAffected = mySqlCommand.ExecuteNonQuery();

                    // Cerrar la conexión
                    cn.CloseConnection();

                    // Devolver el resultado
                    return $"Filas afectadas: {rowsAffected}";
                }
                else
                {
                    return "No se pudo abrir la conexión a la base de datos";
                }
            }
            //catch (Exception ex)
            catch (MySqlException ex)
            {
                cn.CloseConnection();
                return $"Error: {ex.Message} " + ex.Number;
            }
        }
    }
}
