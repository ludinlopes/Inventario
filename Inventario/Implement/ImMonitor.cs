/*using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImMonitor
    {
        private Conexion cn;
        public MMonitor getEmple(string NoInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MMonitor mon = new MMonitor();
            MySqlDataReader DR = null;
            string consulta = $"CALL getMonitores('{NoInv}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    mon.Cod_Emple = cod.ToString();
                    mon.Nombre = mySqlDataReader.GetString("Nombre");
                    mon.No_Inventario = mySqlDataReader.GetString("No_Inventario");
                    mon.Marca = mySqlDataReader.GetString("Marca");
                    mon.Modelo = mySqlDataReader.GetString("Modelo");
                    mon.Serie = mySqlDataReader.GetString("Serie");
                    mon.Estado = mySqlDataReader.GetString("Estado");
                   

                    mon.Condicion = mySqlDataReader.GetString("Condicion");
                }
                mySqlCommand.Connection.Close();
            }
            return mon;
        }



        public string setMonitor(MMonitor modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Monitores set " +
                $"  Cod_Empleado = '{modelo.Cod_Emple}'" +
                $", Marca = '{modelo.Marca.ToUpper()}'" +
                $", Modelo = '{modelo.Modelo.ToUpper()}'" +
                $", Serie = '{modelo.Serie.ToUpper()}'" +
                $", Estado = '{modelo.Estado.ToUpper()}'" +
                $", Condicion = '{modelo.Condicion.ToUpper()}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $"WHERE No_Inventario = '{modelo.No_Inventario}'";


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

        public string insert(MMonitor modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Monitores  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.No_Inventario.ToUpper()}'" +
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
}*/

using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType

namespace Inventario.Implement
{
    public class ImMonitor
    {
        // Eliminamos 'private Conexion cn;' para usar bloques 'using' en cada método.

        public MMonitor getMonitorByNoInv(string NoInv) // Renombramos para mayor claridad
        {
            MMonitor mon = new MMonitor();
            // Nombre del procedimiento almacenado que obtendrá un monitor por su número de inventario
            string procedimiento = "GetMonitorByNoInv"; // Sugerencia de nombre

            try
            {
                using (Conexion cn = new Conexion())
                {
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            // Añadimos el parámetro para el procedimiento almacenado
                            cmd.Parameters.AddWithValue("_No_Inventario", NoInv);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Si se encuentra un registro
                                {
                                    mon.Cod_Emple = reader.GetInt32("Cod_Empleado").ToString();
                                    mon.Nombre = reader.GetString("Nombre"); // Asumo que el SP puede unir con Empleado para obtener el nombre
                                    mon.No_Inventario = reader.GetString("No_Inventario");
                                    mon.Marca = reader.GetString("Marca");
                                    mon.Modelo = reader.GetString("Modelo");
                                    mon.Serie = reader.GetString("Serie");
                                    mon.Estado = reader.GetString("Estado");
                                    mon.Condicion = reader.GetString("Condicion");
                                    // Considera añadir Fecha_Actualizacion si la traes del SP
                                    // if (!reader.IsDBNull(reader.GetOrdinal("Fecha_Actualizacion")))
                                    // {
                                    //     mon.Fecha_Actualizacion = reader.GetDateTime("Fecha_Actualizacion").ToString();
                                    // }
                                }
                            } // reader se cierra y dispone aquí.
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener monitor por No_Inventario: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MMonitor vacío.
            }
            return mon;
        }



        public string setMonitor(MMonitor modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.No_Inventario))
            {
                return "Error: El número de inventario del monitor no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";
            string procedimiento = "UpdateMonitor"; // Nombre del procedimiento almacenado sugerido

            try
            {
                using (Conexion cn = new Conexion())
                {
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; // Indicamos que es un procedimiento almacenado

                            // Agregar parámetros de forma segura
                            // Los nombres de los parámetros deben coincidir con los del SP en MySQL
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario); // Para la cláusula WHERE
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE() o NOW()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Actualización de monitor exitosa. Filas afectadas: {rowsAffected}.";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró el monitor con número de inventario '{modelo.No_Inventario}' para actualizar o no hubo cambios.";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar monitor: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar monitor: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }



        public string insert(MMonitor modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.No_Inventario))
            {
                return "Error: El número de inventario del monitor no puede estar vacío para la inserción.";
            }

            string mensaje = "";
            string procedimiento = "InsertMonitor"; // Nombre del procedimiento almacenado sugerido

            try
            {
                using (Conexion cn = new Conexion())
                {
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; // Indicamos que es un procedimiento almacenado

                            // Agregar parámetros de forma segura
                            // Los nombres de los parámetros deben coincidir con los del SP en MySQL
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario.ToUpper());
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE() o NOW()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                mensaje = "Guardado exitosamente";
                            }
                            else
                            {
                                mensaje = "La inserción del monitor no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El número de inventario '{modelo.No_Inventario}' ya existe. Por favor, ingrese uno diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar monitor: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar monitor: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}
