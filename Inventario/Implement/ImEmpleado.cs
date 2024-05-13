using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImEmpleado
    {
        private Conexion cn;
        public ImEmpleado() 
        {
           
        }

        public MEmpleado getEmple(int codEmple) 
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MEmpleado emple = new MEmpleado();
            MySqlDataReader DR = null;
            string consulta = $"SELECT * FROM Empleado WHERE Cod_Empleado = {codEmple}";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int no = mySqlDataReader.GetInt32("Cod_Empleado");
                    emple.Cod_Emple = no.ToString();
                    emple.Nombre = mySqlDataReader.GetString("Nombre");
                    emple.Area = mySqlDataReader.GetString("Area");
                    emple.Estado = mySqlDataReader.GetString("Estado");
                    
                    
                    emple.Sucursal = mySqlDataReader.GetString("Sucursal");
                }
                mySqlCommand.Connection.Close();
            }
            return emple;
        }


        public string setEmpleado(MEmpleado modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Empleado set " +
                $"Nombre = '{modelo.Nombre}'" +
                $", AREA = '{modelo.Area}'" +
                $", Estado = '{modelo.Estado}'" +
                $", Sucursal = '{modelo.Sucursal}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $" WHERE Cod_Empleado = '{modelo.Cod_Emple}'";
                
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

        public string insert(MEmpleado modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Empleado  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.Nombre}'" +
                    $", '{modelo.Area}'" +
                    $", '{modelo.Estado}'" +
                    $", '{modelo.Sucursal}'" +
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
