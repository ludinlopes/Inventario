using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImImpresora
    {

        private Conexion cn;
        public MImpresora getEmple(string NoInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MImpresora imp = new MImpresora();
            MySqlDataReader DR = null;
            string consulta = $"SELECT * FROM Impresoras WHERE No_Inventario = '{NoInv}'";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    imp.Cod_Emple = cod.ToString();
                    imp.Nombre = "Nombre";
                    imp.NoInventario = mySqlDataReader.GetString("No_Inventario");
                    imp.Marca = mySqlDataReader.GetString("Marca");
                    imp.Modelo = mySqlDataReader.GetString("Modelo");
                    imp.Serie = mySqlDataReader.GetString("Serie");
                    imp.Tipo = mySqlDataReader.GetString("Tipo");
                    imp.Estado = mySqlDataReader.GetString("Estado");


                    imp.Condicion = mySqlDataReader.GetString("Condicion");
                }
                mySqlCommand.Connection.Close();
            }
            return imp;
        }



        public string setImpresora(MImpresora modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Impresoras set " +
                $"  Cod_Empleado = '{modelo.Cod_Emple}'" +
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
                return $"Error: {ex.Message}";
            }
        }

        public string insert(MImpresora modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Impresoras  VALUES (" +
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
                return $"Error: {ex.Message} " + ex.Number;
            }
        }
    }
}
