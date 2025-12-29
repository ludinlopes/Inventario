
using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para ConnectionState y CommandType

namespace Inventario.Implement
{
    public class ImCelular
    {
        // Ya no necesitas 'private Conexion cn;' aquí.
        // Cada método creará y gestionará su propia instancia de Conexion
        // dentro de un bloque 'using', lo que es más seguro y eficiente.

        public MCelular getCelularByImei(string IMEI)
        {
            MCelular cel = new MCelular();
            string consulta = $"CALL getCelulares('{IMEI}')"; // Usando procedimiento almacenado

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                // Esto asegura que 'Conexion' y su 'MySqlConnection' interna se dispongan correctamente.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection desde la instancia de Conexion.
                    // También la envuelve en un 'using' para asegurar su disposición.
                    using (MySqlConnection conn = cn.GetConnection()) // Asumo que OpenConnection() es ahora GetConnection()
                    {
                        conn.Open(); // Abre la conexión explícitamente

                        // Envuelve el MySqlCommand en un 'using'.
                        using (MySqlCommand mySqlCommand = new MySqlCommand(consulta, conn))
                        {
                            // Envuelve el MySqlDataReader en un 'using'.
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            {
                                while (mySqlDataReader.Read())
                                {
                                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                                    cel.Cod_Emple = cod.ToString();
                                    cel.Nombre = mySqlDataReader.GetString("Nombre");
                                    cel.No_Inventario = mySqlDataReader.GetString("No_Inventario");
                                    cel.Marca = mySqlDataReader.GetString("Marca");
                                    cel.Modelo = mySqlDataReader.GetString("Modelo");
                                    cel.Imei = mySqlDataReader.GetString("IMEI");
                                    cel.Estado = mySqlDataReader.GetString("Estado");
                                    cel.Condicion = mySqlDataReader.GetString("Condicion");
                                }
                            } // mySqlDataReader se cierra y se dispone aquí.
                        } // mySqlCommand se cierra y se dispone aquí.
                    } // conn (MySqlConnection) se cierra y se dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en getImpresoraByNoInv: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MCelular con un indicador de error.
                // Por ahora, solo logueamos el error y devolvemos el objeto parcialmente llenado (o vacío si el error fue al inicio).
            }
            return cel;
        }

        public string setCelular(MCelular modelo)
        {
            // Validaciones iniciales
            if (string.IsNullOrWhiteSpace(modelo.Imei))
            {
                return "Error: El IMEI no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection desde la instancia de Conexion y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre explícitamente la conexión

                        // Siempre es recomendable usar parámetros en lugar de concatenación de strings para UPDATE/INSERT
                        // Esto previene SQL Injection y maneja mejor los tipos de datos y caracteres especiales.
                        string procedimiento = "UpdateCelular"; // Asumo que tienes un SP para esto, si no, lo crearemos con parámetros.

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; // Indicamos que es un procedimiento almacenado

                            // Agregamos parámetros de forma segura
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_IMEI", modelo.Imei.ToUpper()); // Usamos el IMEI en mayúsculas como clave
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // MySQL CURDATE() es compatible con DateTime.Now en C#

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Guardado exitosamente";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró el celular con IMEI '{modelo.Imei}' para actualizar o no hubo cambios.";
                            }
                        } // cmd (MySqlCommand) se dispone aquí.
                    } // conn (MySqlConnection) se cierra y dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.

                resultadoMensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }

        public string insert(MCelular modelo)
        {
            // Validaciones iniciales
            if (string.IsNullOrWhiteSpace(modelo.Imei))
            {
                return "Error: El IMEI no puede estar vacío para la inserción.";
            }

            string mensaje = "";

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection desde la instancia de Conexion y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre explícitamente la conexión

                        // Utilizar un procedimiento almacenado para INSERT es lo más seguro y recomendable.
                        // Si no tienes uno, aquí te muestro cómo sería un INSERT con parámetros directos.
                        // Sin embargo, sugiero encarecidamente crear un SP llamado "InsertCelular".
                        string procedimiento = "InsertCelular"; // Asumo que tienes un SP para esto

                        using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure; // Indicamos que es un procedimiento almacenado

                            // Agregamos parámetros de forma segura
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_No_Inventario", modelo.No_Inventario.ToUpper());
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_IMEI", modelo.Imei.ToUpper());
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
                                mensaje = "La inserción no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd (MySqlCommand) se dispone aquí.
                    } // conn (MySqlConnection) se cierra y dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.

                mensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El IMEI '{modelo.Imei}' ya existe. Por favor, ingrese un IMEI diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}