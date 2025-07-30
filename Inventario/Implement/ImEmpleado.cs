/*using Inventario.Models;
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
*/
using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType y ConnectionState
using System.Collections.Generic; // Necesario para List<T>

namespace Inventario.Implement
{
    public class ImEmpleado
    {
        // Ya no necesitas 'private Conexion cn;' aquí.
        // Cada método creará y gestionará su propia instancia de Conexion
        // dentro de un bloque 'using'.

        public ImEmpleado()
        {
            // El constructor puede quedar vacío o ser eliminado si no se necesita inicialización específica de ImEmpleado.
        }

        public List<MEmpleado> getEmpleados()
        {
            var emple = new List<MEmpleado>();
            string consulta = "SELECT Cod_Empleado, Nombre, Area, Estado, Sucursal FROM Empleado"; // Consulta directa

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
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
                                    var g = new MEmpleado();

                                    // Asegúrate de que los nombres de columna coincidan exactamente con tu base de datos
                                    g.Cod_Emple = mySqlDataReader.GetInt32("Cod_Empleado").ToString();
                                    g.Nombre = mySqlDataReader.GetString("Nombre");
                                    g.Area = mySqlDataReader.GetString("Area");
                                    g.Estado = mySqlDataReader.GetString("Estado");
                                    g.Sucursal = mySqlDataReader.GetString("Sucursal");

                                    emple.Add(g);
                                }
                            } // mySqlDataReader se cierra y se dispone aquí.
                        } // mySqlCommand se cierra y se dispone aquí.
                    } // conn (MySqlConnection) se cierra y se dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener empleados: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver una lista vacía.
            }
            return emple;
        }

        public MEmpleado getEmple(string codEmple)
        {
            MEmpleado emple = new MEmpleado();
            string procedimiento = "GetEmpleadoByCod"; // Sugerencia: usar un procedimiento almacenado

            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión explícitamente

                        // Envuelve el MySqlCommand en un 'using'.
                        // Usar CommandType.StoredProcedure si tienes el SP, de lo contrario, deja CommandType.Text
                        using (MySqlCommand mySqlCommand = new MySqlCommand(procedimiento, conn))
                        {
                            mySqlCommand.CommandType = CommandType.StoredProcedure; // Si usas SP

                            // Agrega el parámetro para el código de empleado
                            mySqlCommand.Parameters.AddWithValue("_Cod_Empleado", codEmple); // Asumo que el SP espera '_Cod_Empleado'

                            // Envuelve el MySqlDataReader en un 'using'.
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            {
                                if (mySqlDataReader.Read()) // Solo esperamos una fila para un código único
                                {
                                    emple.Cod_Emple = mySqlDataReader.GetInt32("Cod_Empleado").ToString();
                                    emple.Nombre = mySqlDataReader.GetString("Nombre");
                                    emple.Area = mySqlDataReader.GetString("Area");
                                    emple.Estado = mySqlDataReader.GetString("Estado");
                                    emple.Sucursal = mySqlDataReader.GetString("Sucursal");
                                }
                            } // mySqlDataReader se cierra y se dispone aquí.
                        } // mySqlCommand se cierra y se dispone aquí.
                    } // conn (MySqlConnection) se cierra y se dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener empleado por código: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MEmpleado vacío/nulo.
            }
            return emple;
        }


        public string setEmpleado(MEmpleado modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.Cod_Emple))
            {
                return "Error: El código de empleado no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";
            string procedimiento = "UpdateEmpleado"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_Nombre", modelo.Nombre.ToUpper());
                            cmd.Parameters.AddWithValue("_Area", modelo.Area.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Sucursal", modelo.Sucursal.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE()
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple); // Para la cláusula WHERE

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Actualización exitosa. Filas afectadas: {rowsAffected}.";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró el empleado con código '{modelo.Cod_Emple}' para actualizar o no hubo cambios.";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.

                resultadoMensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar empleado: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar empleado: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }

        public string insert(MEmpleado modelo)
        {
            if (string.IsNullOrWhiteSpace(modelo.Cod_Emple))
            {
                return "Error: El código de empleado no puede estar vacío para la inserción.";
            }

            string mensaje = "";
            string procedimiento = "InsertEmpleado"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_Nombre", modelo.Nombre.ToUpper());
                            cmd.Parameters.AddWithValue("_Area", modelo.Area.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Sucursal", modelo.Sucursal.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                mensaje = "Inserción de empleado exitosa.";
                            }
                            else
                            {
                                mensaje = "La inserción del empleado no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.

                mensaje += " **Base de datos cerrada (conexión liberada).**";
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El código de empleado '{modelo.Cod_Emple}' ya existe. Por favor, ingrese uno diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar empleado: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar empleado: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}