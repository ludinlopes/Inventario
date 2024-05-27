using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImUps
    {
        private Conexion cn;
        public MUps getEmple(string NoInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MUps ups = new MUps();
            MySqlDataReader DR = null;
            string consulta = $"CALL getUPS('{NoInv}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    ups.Cod_Emple = cod.ToString();
                    ups.Nombre = mySqlDataReader.GetString("Nombre");
                    ups.NoInventario = mySqlDataReader.GetString("No_Inventario");
                    ups.Marca = mySqlDataReader.GetString("Marca");
                    ups.Modelo = mySqlDataReader.GetString("Modelo");
                    ups.Serie = mySqlDataReader.GetString("Serie");
                    ups.Estado = mySqlDataReader.GetString("Estado");

                    ups.Condicion = mySqlDataReader.GetString("Condicion");
                }
                mySqlCommand.Connection.Close();
            }
            return ups;
        }






        public string setUps(MUps modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE UPS set " +
                $"  Cod_Empleado = '{modelo.Cod_Emple}'" +
                $", Marca = '{modelo.Marca.ToUpper()}'" +
                $", Modelo = '{modelo.Modelo.ToUpper()}'" +
                $", Serie = '{modelo.Serie.ToUpper()}'" +
                $", Estado = '{modelo.Estado.ToUpper()}'" +
                $", Condicion = '{modelo.Condicion.ToUpper()}'" +
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


        public string insert(MUps modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO UPS  VALUES (" +
                     $"'{modelo.Cod_Emple}', " +
                     $"'{modelo.NoInventario.ToUpper()}', " +
                     $"'{modelo.Marca.ToUpper()}', " +
                     $"'{modelo.Modelo.ToUpper()}', " +
                     $"'{modelo.Serie.ToUpper()}', " +
                     $"'{modelo.Estado.ToUpper()}', " +
                     $"'{modelo.Condicion.ToUpper()}', " +
                     $"CURDATE())";


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
