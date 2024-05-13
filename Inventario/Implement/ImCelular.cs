using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImCelular
    {
        private Conexion cn;
        public MCelular getEmple(string IMEI)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MCelular cel = new MCelular();
            MySqlDataReader DR = null;
            string consulta = $"SELECT * FROM Celulares WHERE IMEI = '{IMEI}'";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    cel.Cod_Emple = cod.ToString();
                    cel.Nombre = "Nombre";
                    cel.Marca = mySqlDataReader.GetString("Marca");
                    cel.Modelo = mySqlDataReader.GetString("Modelo");
                    cel.Imei = mySqlDataReader.GetString("IMEI");
                    cel.Estado = mySqlDataReader.GetString("Estado");
                    cel.Condicion = mySqlDataReader.GetString("Condicion");
                    //if (mySqlDataReader.GetString("Estado") == "A")
                    //{
                    //    cel.Estado = "Activo";
                    //}
                    //else
                    //{
                    //    cel.Estado = "Inactivo";
                    //}

                    //cel.Condicion = mySqlDataReader.GetString("Condicion");

                }
                mySqlCommand.Connection.Close();
            }
            return cel;
        }


        public string setCelular(MCelular modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Celulares set " +
                $"  Cod_Empleado = '{modelo.Cod_Emple}'" +
                $", Marca = '{modelo.Marca}'" +
                $", Modelo = '{modelo.Modelo}'" +
                $", IMEI = '{modelo.Imei}'" +
                $", Estado = '{modelo.Estado}'" +
                $", Condicion = '{modelo.Condicion}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $"WHERE IMEI = '{modelo.Imei}'";
                
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
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string insert(MCelular modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Celulares  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.Marca}'" +
                    $", '{modelo.Modelo}'" +
                    $", '{modelo.Imei}'" +
                    $", '{modelo.Estado}'" +
                    $", '{modelo.Condicion}'" +
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
                return $"Error: {ex.Message} " + ex.Number;
            }
        }
    }
}
