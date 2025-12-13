/*using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImImpresora
    {

        private Conexion cn;
        public MImpresora getImpresoraByNoInv(string NoInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MImpresora imp = new MImpresora();
            MySqlDataReader DR = null;
            string consulta = $"CALL getImpresoras('{NoInv}')";
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
                    imp.Nombre = mySqlDataReader.GetString("Nombre");
                    imp.No_Inventario = mySqlDataReader.GetString("No_Inventario");
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
                $", Marca = '{modelo.Marca.ToUpper()}'" +
                $", Modelo = '{modelo.Modelo.ToUpper()}'" +
                $", Serie = '{modelo.Serie.ToUpper()}'" +
                $", Tipo = '{modelo.Tipo.ToUpper()}'" +
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

        public string insert(MImpresora modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Impresoras  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.No_Inventario.ToUpper()}'" +
                    $", '{modelo.Marca.ToUpper()}'" +
                    $", '{modelo.Modelo.ToUpper()}'" +
                    $", '{modelo.Serie.ToUpper()}'" +
                    $", '{modelo.Tipo.ToUpper()}'" +
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
*/

using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType y ConnectionState

namespace Inventario.Implement
{
    public class ImImpresora
    {
        // Ya no necesitas 'private Conexion cn;' aquí.
        // Cada método creará y gestionará su propia instancia de Conexion
        // dentro de un bloque 'using'.

        public MImpresora getImpresoraByNoInv(string NoInv) // Renombrado a getImpresoraByNoInv para mayor claridad
        {
            MImpresora imp = new MImpresora();
            // Asumo que 'getImpresoras' es un procedimiento almacenado que acepta No_Inventario
            string procedimiento = "getImpresoras";

            try
            {
                using (Conexion cn = new Conexion())
                {
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión explícitamente

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("noInv", NoInv); // Parámetro para el procedimiento almacenado

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Si se encuentra un registro
                                {
                                    imp.Cod_Emple = reader.GetInt32("Cod_Empleado").ToString();
                                    imp.Nombre = reader.GetString("Nombre");
                                    imp.No_Inventario = reader.GetString("No_Inventario");
                                    imp.Marca = reader.GetString("Marca");
                                    imp.Modelo = reader.GetString("Modelo");
                                    imp.Serie = reader.GetString("Serie");
                                    imp.Tipo = reader.GetString("Tipo");
                                    imp.Estado = reader.GetString("Estado");
                                    imp.Condicion = reader.GetString("Condicion");
                                }
                            } // reader se cierra y dispone aquí.
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener impresora por No_Inventario: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MImpresora vacío.
            }
            return imp;
        }

        

        public string setImpresora(MImpresora modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.No_Inventario))
            {
                return "Error: El número de inventario no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";
            string procedimiento = "UpdateImpresora"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Tipo", modelo.Tipo.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE()
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario); // Para la cláusula WHERE

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Actualización exitosa. Filas afectadas: {rowsAffected}.";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró la impresora con número de inventario '{modelo.No_Inventario}' para actualizar o no hubo cambios.";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.

                //resultadoMensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar impresora: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar impresora: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }

       

        public string insert(MImpresora modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.No_Inventario))
            {
                return "Error: El número de inventario no puede estar vacío para la inserción.";
            }

            string mensaje = "";
            string procedimiento = "InsertImpresora"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario.ToUpper());
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Tipo", modelo.Tipo.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                mensaje = "Guardado exitosamente";
                            }
                            else
                            {
                                mensaje = "La inserción de la impresora no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.

                //mensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El número de inventario '{modelo.No_Inventario}' ya existe. Por favor, ingrese uno diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar impresora: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar impresora: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}