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

        public List<MEmpleado> getEmpleados()
        {
            cn = new Conexion();
            var emple = new List<MEmpleado>();


            MySqlDataReader mySqlDataReader;

            string consulta = $"SELECT * FROM Empleado";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    var g = new MEmpleado();


                    
                    
                    int no = mySqlDataReader.GetInt32("Cod_Empleado");
                    g.Cod_Emple = no.ToString();
                    g.Nombre = mySqlDataReader.GetString("Nombre");
                    g.Area = mySqlDataReader.GetString("Area");
                    g.Estado = mySqlDataReader.GetString("Estado");
                    g.Sucursal = mySqlDataReader.GetString("Sucursal");
                   


                    emple.Add(g);

                }
                mySqlCommand.Connection.Close();
            }
            return emple;

        }
        public MEmpleado getEmple(string codEmple) 
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MEmpleado emple = new MEmpleado();
            MySqlDataReader DR = null;
            string consulta = $"SELECT * FROM Empleado WHERE Cod_Empleado = '{codEmple}'";
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
                $"Nombre = '{modelo.Nombre.ToUpper()}'" +
                $", AREA = '{modelo.Area.ToUpper()}'" +
                $", Estado = '{modelo.Estado.ToUpper()}'" +
                $", Sucursal = '{modelo.Sucursal.ToUpper()}'" +
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
                cn.CloseConnection();
                return $"Error: {ex.Message}";
            }
        }

        public string insert(MEmpleado modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Empleado  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.Nombre.ToUpper()}'" +
                    $", '{modelo.Area.ToUpper()}'" +
                    $", '{modelo.Estado.ToUpper()}'" +
                    $", '{modelo.Sucursal.ToUpper()}'" +
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
