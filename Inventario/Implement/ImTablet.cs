/*using Inventario.Models;
using MySql.Data.MySqlClient;

namespace Inventario.Implement
{
    public class ImTablet
    {

        private Conexion cn;
        public MTablet getEmple(string IMEI)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MTablet cel = new MTablet();
            MySqlDataReader DR = null;
            string consulta = $"CALL getTablet('{IMEI}')";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int cod = mySqlDataReader.GetInt32("Cod_Empleado");
                    cel.Cod_Emple = cod.ToString();
                    cel.Nombre = mySqlDataReader.GetString("Nombre");
                    cel.Marca = mySqlDataReader.GetString("Marca");
                    cel.Modelo = mySqlDataReader.GetString("Modelo");
                    cel.Imei = mySqlDataReader.GetString("IMEI");
                    cel.Estado = mySqlDataReader.GetString("Estado");
                    cel.Condicion = mySqlDataReader.GetString("Condicion");
                    //if (mySqlDataReader.GetString("Estado") == "A")
                    //{
                    //    cel.Estado = "Activo";
                    //}
                    //else
                    //{
                    //    cel.Estado = "Inactivo";
                    //}

                    //cel.Condicion = mySqlDataReader.GetString("Condicion");

                }
                mySqlCommand.Connection.Close();
            }
            return cel;
        }


        public string setCelular(MTablet modelo)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Tablet set " +
                $"  Cod_Empleado = '{modelo.Cod_Emple}'" +
                $", Marca = '{modelo.Marca.ToUpper()}'" +
                $", Modelo = '{modelo.Modelo.ToUpper()}'" +
                $", IMEI = '{modelo.Imei.ToUpper()}'" +
                $", Estado = '{modelo.Estado.ToUpper()}'" +
                $", Condicion = '{modelo.Condicion.ToUpper()}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $"WHERE IMEI = '{modelo.Imei}'";

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

        public string insert(MTablet modelo)
        {
            cn = new Conexion();
            string consulta = $"INSERT INTO Tablet  VALUES (" +
                    $" '{modelo.Cod_Emple}'" +
                    $", '{modelo.Marca.ToUpper()}'" +
                    $", '{modelo.Modelo.ToUpper()}'" +
                    $", '{modelo.Imei.ToUpper()}'" +
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
    public class ImTablet
    {
        // Eliminamos 'private Conexion cn;' para gestionar la conexión con bloques 'using' en cada método.

        public MTablet getTabletByIMEI(string IMEI) // Renombramos para mayor claridad
        {
            MTablet tablet = new MTablet();
            // Nombre del procedimiento almacenado para obtener una tablet por su IMEI
            string procedimiento = "GetTablet"; // Sugerencia de nombre

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
                            cmd.Parameters.AddWithValue("IMEI", IMEI);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Si se encuentra un registro
                                {
                                    tablet.Cod_Emple = reader.GetInt32("Cod_Empleado").ToString();
                                    tablet.Nombre = reader.GetString("Nombre"); // Asumo que el SP une con Empleado para obtener el nombre
                                    tablet.Marca = reader.GetString("Marca");
                                    tablet.Modelo = reader.GetString("Modelo");
                                    tablet.Imei = reader.GetString("IMEI");
                                    tablet.Estado = reader.GetString("Estado");
                                    tablet.Condicion = reader.GetString("Condicion");
                                    
                                }
                            } // reader se cierra y dispone aquí.
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener tablet por IMEI: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver un objeto MTablet vacío.
            }
            return tablet;
        }



        public string setTablet(MTablet modelo) // Renombramos para mayor claridad
        {
            if (string.IsNullOrWhiteSpace(modelo.Imei))
            {
                return "Error: El IMEI de la tablet no puede estar vacío para la actualización.";
            }

            string resultadoMensaje = "";
            string procedimiento = "UpdateTablet"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_IMEI", modelo.Imei); // Para la cláusula WHERE
                            cmd.Parameters.AddWithValue("_Cod_Empleado", modelo.Cod_Emple);
                            cmd.Parameters.AddWithValue("_Marca", modelo.Marca.ToUpper());
                            cmd.Parameters.AddWithValue("_Modelo", modelo.Modelo.ToUpper());
                            cmd.Parameters.AddWithValue("_Estado", modelo.Estado.ToUpper());
                            cmd.Parameters.AddWithValue("_Condicion", modelo.Condicion.ToUpper());
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now); // Para CURDATE() o NOW()

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                resultadoMensaje = $"Actualización de tablet exitosa. Filas afectadas: {rowsAffected}.";
                            }
                            else
                            {
                                resultadoMensaje = $"No se encontró la tablet con IMEI '{modelo.Imei}' para actualizar o no hubo cambios.";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                resultadoMensaje = $"Error de base de datos al actualizar tablet: {ex.Message} (Código: {ex.Number})";
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                resultadoMensaje = $"Error inesperado al actualizar tablet: {ex.Message}";
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return resultadoMensaje;
        }



        public string insertTablet(MTablet modelo) // Renombramos para mayor claridad
        {
            if (string.IsNullOrWhiteSpace(modelo.Imei))
            {
                return "Error: El IMEI de la tablet no puede estar vacío para la inserción.";
            }

            string mensaje = "";
            string procedimiento = "InsertTablet"; // Nombre del procedimiento almacenado sugerido

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
                            cmd.Parameters.AddWithValue("_IMEI", modelo.Imei.ToUpper());
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
                                mensaje = "La inserción de la tablet no afectó ninguna fila (posiblemente un problema con la lógica del SP).";
                            }
                        } // cmd se dispone aquí.
                    } // conn se cierra y dispone aquí.
                } // cn se dispone aquí.
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // Código de error para entrada duplicada (Duplicate entry for primary key)
                {
                    mensaje = $"Error de duplicado: El IMEI '{modelo.Imei}' ya existe. Por favor, ingrese uno diferente.";
                }
                else
                {
                    mensaje = $"Error de base de datos al insertar tablet: {ex.Message} (Código: {ex.Number})";
                }
                Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
            }
            catch (Exception ex)
            {
                mensaje = "Error inesperado al insertar tablet: " + ex.Message;
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return mensaje;
        }
    }
}
