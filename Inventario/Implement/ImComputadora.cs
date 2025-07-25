using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;

namespace Inventario.Implement
{

    public class ImComputadora
    {
        private Conexion? cn;

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
                    compu.No_Inventario = mySqlDataReader.GetString("No_Inventario");
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
            if (string.IsNullOrWhiteSpace(compu.No_Inventario))
            {
                return "Error: El número de inventario no puede estar vacío.";
            }

            cn = new Conexion();
            string procedimiento = "UpdateComputadora"; // Nombre del procedimiento almacenado

            try
            {
                MySqlConnection conn = cn.OpenConnection();
                if (conn != null)
                {
                    conn.Open(); // <-- Abrimos explícitamente la conexión

                    using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros
                        
                        cmd.Parameters.AddWithValue("_Estado", compu.Estado.ToUpper());
                        cmd.Parameters.AddWithValue("_No_Inventario", compu.No_Inventario);
                        cmd.Parameters.AddWithValue("_Tipo", compu.Tipo.ToUpper());
                        cmd.Parameters.AddWithValue("_NombrePc", compu.NombrePc.ToUpper());
                        cmd.Parameters.AddWithValue("_Dominio", compu.Dominio.ToUpper());
                        cmd.Parameters.AddWithValue("_Usuario", compu.Usuario.ToUpper());
                        cmd.Parameters.AddWithValue("_Contra", compu.Contra);
                        cmd.Parameters.AddWithValue("_Marca", compu.Marca.ToUpper());
                        cmd.Parameters.AddWithValue("_Modelo", compu.Modelo.ToUpper());
                        cmd.Parameters.AddWithValue("_Serie", compu.Serie.ToUpper());
                        cmd.Parameters.AddWithValue("_Procesador", compu.Procesador.ToUpper());
                        cmd.Parameters.AddWithValue("_Generacion", compu.Generacion.ToUpper());
                        cmd.Parameters.AddWithValue("_Tipo_Disco", compu.TipoDisco.ToUpper());
                        cmd.Parameters.AddWithValue("_Capacidad_Disco", compu.CapacidadDisco.ToUpper());
                        cmd.Parameters.AddWithValue("_Ram", compu.Ram.ToUpper());
                        cmd.Parameters.AddWithValue("_Mac_Address", compu.MacAddress.ToUpper());
                        cmd.Parameters.AddWithValue("_No_IP", compu.NoIp.ToUpper());
                        cmd.Parameters.AddWithValue("_Mouse", compu.Mouse.ToUpper());
                        cmd.Parameters.AddWithValue("_Teclado", compu.Teclado.ToUpper());
                        cmd.Parameters.AddWithValue("_Condicion", compu.Condicion.ToUpper());
                        cmd.Parameters.AddWithValue("_Cod_Empleado", compu.Cod_Emple);
                        cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // o compu.FechaActualizacion si viene del modelo


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return $"Filas afectadas: {rowsAffected}";
                    }
                }
                else
                {
                    return "No se pudo abrir la conexión a la base de datos";
                }
            }
            catch (Exception ex)
            {
                return $"Error:  {ex.Message}"+ compu.No_Inventario;
            }
        }


        public string insert(MComputadora modelo)
        {
            cn = new Conexion();
            string mensaje = "Error al insertar";
            string procedimiento = "InsertComputadora"; // Nombre del procedimiento almacenado

            try
            {
                MySqlConnection conn = cn.OpenConnection();
                if (conn != null)
                {
                    conn.Open(); // <-- Abrimos explícitamente la conexión

                    using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros (con guion bajo, igual que en el procedimiento)
                        cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                        cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                        cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario);
                        cmd.Parameters.AddWithValue("_Tipo", modelo.Tipo.ToUpper());
                        cmd.Parameters.AddWithValue("_NombrePc", modelo.NombrePc.ToUpper());
                        cmd.Parameters.AddWithValue("_Dominio", modelo.Dominio.ToUpper());
                        cmd.Parameters.AddWithValue("_Usuario", modelo.Usuario.ToUpper());
                        cmd.Parameters.AddWithValue("_Contra", modelo.Contra); // No convertir si puede incluir caracteres especiales
                        cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                        cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                        cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                        cmd.Parameters.AddWithValue("_Procesador", modelo.Procesador.ToUpper());
                        cmd.Parameters.AddWithValue("_Generacion", modelo.Generacion.ToUpper());
                        cmd.Parameters.AddWithValue("_Tipo_Disco", modelo.TipoDisco.ToUpper());
                        cmd.Parameters.AddWithValue("_Capacidad_Disco", modelo.CapacidadDisco.ToUpper());
                        cmd.Parameters.AddWithValue("_Ram", modelo.Ram.ToUpper());
                        cmd.Parameters.AddWithValue("_Mac_Address", modelo.MacAddress.ToUpper());
                        cmd.Parameters.AddWithValue("_No_IP", modelo.NoIp.ToUpper());
                        cmd.Parameters.AddWithValue("_Mouse", modelo.Mouse.ToUpper());
                        cmd.Parameters.AddWithValue("_Teclado", modelo.Teclado.ToUpper());
                        cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                        cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now);

                        cmd.ExecuteNonQuery();
                        mensaje = "Inserción exitosa";
                    }
                }
                
            }
            catch (MySqlException ex)
            {
                // El error 1062 es el de entrada duplicada para una clave única
                if (ex.Number == 1062)
                {
                    mensaje = $"Error de duplicado: La serie '{modelo.Serie}' ya existe. Por favor, ingrese una serie diferente.";
                    // Opcional: podrías loggear el error completo para depuración
                    Console.WriteLine($"Error MySql (1062): {ex.Message}");
                }
                else
                {
                    // Otros errores de MySQL (conexión, sintaxis, etc.)
                    mensaje = $"Error de base de datos inesperado: {ex.Message}";
                    Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex;
            }

            return mensaje;
        }


    }

}

    
    

