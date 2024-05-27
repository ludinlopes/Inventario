using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Inventario.Implement
{

    public class ImComputadora
    {
        private Conexion cn;

        public MComputadora getEmple(string noInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MComputadora compu = new MComputadora();
            MySqlDataReader DR = null;
            string consulta = $"CALL getComputadora('{noInv}');";
                //SELECT * FROM Computadoras WHERE No_Inventario = '{noInv}'";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    /////////////////////llenado de modelo/////////////////////////////////////////////
                    int no = mySqlDataReader.GetInt32("Cod_Empleado");
                    compu.Cod_Emple = no.ToString();
                    compu.Nombre = mySqlDataReader.GetString("Nombre");
                    compu.NoInmentario = mySqlDataReader.GetString("No_Inventario");
                    compu.Tipo = mySqlDataReader.GetString("Tipo");
                    compu.Marca = mySqlDataReader.GetString("Marca");
                    compu.Modelo = mySqlDataReader.GetString("Modelo");
                    compu.Serie = mySqlDataReader.GetString("Serie");
                    compu.Procesador = mySqlDataReader.GetString("Procesador");
                    compu.Generacion = mySqlDataReader.GetString("Generacion");
                    compu.TipoDisco = mySqlDataReader.GetString("Tipo_Disco");
                    compu.CapacidadDisco = mySqlDataReader.GetString("Capacidad_Disco");
                    compu.Ram = mySqlDataReader.GetString("Ram");
                    compu.MacAddress = mySqlDataReader.GetString("Mac_Address");
                    compu.NoIp = mySqlDataReader.GetString("No_IP");
                    compu.Teclado = mySqlDataReader.GetString("Teclado");
                    compu.Mouse = mySqlDataReader.GetString("Mouse");
                    compu.NombrePc = mySqlDataReader.GetString("NombrePc");
                    compu.Dominio = mySqlDataReader.GetString("Dominio");
                    compu.Usuario = mySqlDataReader.GetString("Usuario");
                    compu.Contra = mySqlDataReader.GetString("Contra");
                    compu.Estado = mySqlDataReader.GetString("Estado");
                    compu.Condicion = mySqlDataReader.GetString("Condicion");

                    ////////////////////////////////////////////////////////////////////////////////////////////
                }
                mySqlCommand.Connection.Close();
            }
            return compu;
        }

        public string setComputadora(MComputadora compu)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Computadoras set " +
                $"  Cod_Empleado = '{compu.Cod_Emple}'" +
                $", Estado = '{compu.Estado.ToUpper()}'" +
                $", Tipo = '{compu.Tipo.ToUpper()}'" +
                $", NombrePc = '{compu.NombrePc.ToUpper()}'" +
                $", Dominio = '{compu.Dominio.ToUpper()}'" +
                $", Usuario = '{compu.Usuario.ToUpper()}'" +
                $", Contra = '{compu.Contra}'" +
                $", Marca = '{compu.Marca.ToUpper()}'" +
                $", Modelo = '{compu.Modelo.ToUpper()}'" +
                $", Serie = '{compu.Serie.ToUpper()}'" +
                $", Procesador = '{compu.Procesador.ToUpper()}'" +
                $", Generacion = '{compu.Generacion.ToUpper()}'" +
                $", Tipo_Disco = '{compu.TipoDisco.ToUpper()}'" +
                $", Capacidad_Disco = '{compu.CapacidadDisco.ToUpper()}'" +
                $", Ram = '{compu.Ram.ToUpper()}'" +
                $", Mac_Address = '{compu.MacAddress.ToUpper()}'" +
                $", No_IP = '{compu.NoIp.ToUpper()}'" +
                $", Mouse = '{compu.Mouse.ToUpper()}'" +
                $", Teclado = '{compu.Teclado.ToUpper()}'" +
                $", Condicion = '{compu.Condicion.ToUpper()}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $" WHERE No_Inventario = '{compu.NoInmentario}'";
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
                return $"Error: {ex.Message}           "+consulta;

            }
        }

        public string insert(MComputadora modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Computadoras  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.Estado.ToUpper()}'" +
                    $", '{modelo.NoInmentario.ToUpper()}'" +
                    $", '{modelo.Tipo.ToUpper()}'" +
                    $", '{modelo.NombrePc.ToUpper()}'" +
                    $", '{modelo.Dominio.ToUpper()}'" +
                    $", '{modelo.Usuario.ToUpper()}'" +
                    $", '{modelo.Contra}'" +
                    $", '{modelo.Marca.ToUpper()}'" +
                    $", '{modelo.Modelo.ToUpper()}'" +
                    $", '{modelo.Serie.ToUpper()}'" +
                    $", '{modelo.Procesador.ToUpper()}'" +
                    $", '{modelo.Generacion.ToUpper()}'" +
                    $", '{modelo.TipoDisco.ToUpper()}'" +
                    $", '{modelo.CapacidadDisco.ToUpper()}'" +
                    $", '{modelo.Ram.ToUpper()}'" +
                    $", '{modelo.MacAddress.ToUpper()}'" +
                    $", '{modelo.NoIp.ToUpper()}'" +
                    $", '{modelo.Mouse.ToUpper()}'" +
                    $", '{modelo.Teclado.ToUpper()}'" +
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

    
    

