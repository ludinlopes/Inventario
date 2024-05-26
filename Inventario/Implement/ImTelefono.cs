using Inventario.Controllers;
using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImTelefono
    {
        private Conexion cn;
        public MTelefono getEmple(string CodEmple)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MTelefono tel = new MTelefono();
            MySqlDataReader DR = null;
            string consulta = $"CALL getTelefono('{CodEmple}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    tel.Cod_Emple = cod.ToString();
                    tel.Nombre = mySqlDataReader.GetString("Nombre");
                    tel.NoInventario = mySqlDataReader.GetString("No_Inventario");
                    tel.Marca = mySqlDataReader.GetString("Marca");
                    tel.Modelo = mySqlDataReader.GetString("Modelo");
                    tel.Serie = mySqlDataReader.GetString("Serie");
                    tel.Tipo = mySqlDataReader.GetString("Tipo");
                    tel.Estado = mySqlDataReader.GetString("Estado");
                    


                    tel.Condicion = mySqlDataReader.GetString("Condicion");
                }
                mySqlCommand.Connection.Close();
            }
            return tel;
        }




        public string setTelefono(MTelefono modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Telefono set " +
                $"Cod_Empleado = {modelo.Cod_Emple}" +
                $", No_Inventario = '{modelo.NoInventario}'" +
                $", Marca = '{modelo.Marca}'" +
                $", Modelo = '{modelo.Modelo}'" +
                $", Serie = '{modelo.Serie}'" +
                $", Tipo = '{modelo.Tipo}'" +
                $", Estado = '{modelo.Estado}'" +
                $", Condicion = '{modelo.Condicion}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $"WHERE No_Inventario = '{modelo.NoInventario}'";


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


        public string insert(MTelefono modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Telefono  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.NoInventario}'" +
                    $", '{modelo.Marca}'" +
                    $", '{modelo.Modelo}'" +
                    $", '{modelo.Serie}'" +
                    $", '{modelo.Tipo}'" +
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
                cn.CloseConnection();
                return $"Error: {ex.Message} " + ex.Number;
            }
        }
    }
}
