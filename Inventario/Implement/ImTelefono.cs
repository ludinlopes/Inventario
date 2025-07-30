/*using Inventario.Controllers;
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
                $", No_Inventario = '{modelo.NoInventario.ToUpper()}'" +
                $", Marca = '{modelo.Marca.ToUpper()}'" +
                $", Modelo = '{modelo.Modelo.ToUpper()}'" +
                $", Serie = '{modelo.Serie.ToUpper()}'" +
                $", Tipo = '{modelo.Tipo.ToUpper()}'" +
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


        public string insert(MTelefono modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Telefono  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.NoInventario.ToUpper()}'" +
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
}*/

using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType

namespace Inventario.Implement
{
    public class ImTelefono
    {
        // Eliminamos 'private Conexion cn;' para gestionar la conexión con bloques 'using' en cada método.

        public MTelefono getTelefonoByCodEmple(string CodEmple) // Renombramos para mayor claridad
        {
            MTelefono tel = new MTelefono();
            // Nombre del procedimiento almacenado para obtener un teléfono por el código de empleado
            string procedimiento = "GetTelefonoByCodEmple"; // Sugerencia de nombre

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
                            cmd.Parameters.AddWithValue("_Cod_Empleado", CodEmple);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Si se encuentra un registro
                                {
                                    tel.Cod_Emple = reader.GetInt32("Cod_Empleado").ToString();
                                    tel.Nombre = reader.GetString("Nombre"); // Asumo que el SP une con Empleado para obtener el nombre
                                    tel.NoInventario = reader.GetString("No_Inventario");
                                    tel.Marca = reader.GetString("Marca");
                                    tel.Modelo = reader.GetString("Modelo");
                                    tel.Serie = reader.GetString("Serie");
                                    tel.Tipo = reader.GetString("Tipo");
                                    tel.Estado = reader.GetString("Estado");
                                    tel.Condicion = reader.GetString("Condicion");
                                   
                                }
                            } // reader se cierra y dispone aquí.
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener teléfono por Cod_Empleado: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MTelefono vacío.
            }
            return tel;
        }



        public string setTelefono(MTelefono modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.NoInventario))
            {
                return "Error: El número de inventario del teléfono no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";
            string procedimiento = "UpdateTelefono"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.NoInventario); // Para la cláusula WHERE
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Tipo", modelo.Tipo.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE() o NOW()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Actualización de teléfono exitosa. Filas afectadas: {rowsAffected}.";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró el teléfono con número de inventario '{modelo.NoInventario}' para actualizar o no hubo cambios.";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar teléfono: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar teléfono: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }


        public string insertTelefono(MTelefono modelo) // Renombramos para mayor claridad
        {
            if (string.IsNullOrWhiteSpace(modelo.NoInventario))
            {
                return "Error: El número de inventario del teléfono no puede estar vacío para la inserción.";
            }

            string mensaje = "";
            string procedimiento = "InsertTelefono"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.NoInventario.ToUpper());
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Serie", modelo.Serie.ToUpper());
                            cmd.Parameters.AddWithValue("_Tipo", modelo.Tipo.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE() o NOW()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                mensaje = "Inserción de teléfono exitosa.";
                            }
                            else
                            {
                                mensaje = "La inserción del teléfono no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El número de inventario '{modelo.NoInventario}' ya existe. Por favor, ingrese uno diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar teléfono: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar teléfono: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}