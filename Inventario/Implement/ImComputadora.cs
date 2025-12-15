using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;

namespace Inventario.Implement
{

    /* public class ImComputadora
    {
        private Conexion? cn;

        public MComputadora getImpresoraByNoInv(string noInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MComputadora compu = new MComputadora();
            MySqlDataReader DR = null;
            string consulta = $"CALL getComputadora('{noInv}');";
            //SELECT * FROM Computadora WHERE No_Inventario = '{noInv}'";
            MySqlCommand mySqlCommand = new MySqlCommand(consulta);
            mySqlCommand.Connection = cn.OpenConnection();
            //mySqlCommand.Connection.Open();
            if (mySqlCommand.Connection != null)
            {
               
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

        */
    public class ImComputadora
    {
        public MComputadora getComputadoraByNoInv(string noInv)
        {
            MComputadora compu = new MComputadora();
            string consulta = $"CALL getComputadora('{noInv}');";

            // Aquí se crea la instancia de Conexion y se asegura su liberación
            using (Conexion cn = new Conexion())
            {
                // Aquí se obtiene la MySqlConnection y se asegura su liberación
                using (MySqlConnection conn = cn.GetConnection())
                {
                    conn.Open(); // Abre la conexión (ahora sí, dentro del using)

                    using (MySqlCommand mySqlCommand = new MySqlCommand(consulta, conn))
                    {
                        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                        {
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
                            } // mySqlDataReader se cierra aquí
                        } // mySqlCommand se cierra aquí
                    } // conn (MySqlConnection) se cierra y libera aquí
                    Console.WriteLine($"Estado de la conexión antes de abrir: {conn.State}");
                } // cn (Conexion) se cierra y libera aquí (lo que a su vez cierra conn si no lo hizo antes
                return compu;
            }
        }



        public string setComputadora(MComputadora compu)
        {
            if (string.IsNullOrWhiteSpace(compu.No_Inventario))
            {
                return "Error: El número de inventario no puede estar vacío.";
            }

            string procedimiento = "UpdateComputadora"; // Nombre del procedimiento almacenado
            string mensaje = "";
            try
            {
                // Paso 1: Usar 'using' para la instancia de Conexion.
                // Esto asegura que la clase Conexion y su MySqlConnection interna se dispongan.
                using (Conexion cn = new Conexion())
                {
                    // Paso 2: Usar 'using' para la MySqlConnection obtenida de Conexion.
                    // Esto es por si la conexión se cierra/dispone antes de que 'cn' salga de su bloque 'using'.
                    // Aunque Dispose en Conexion ya la cierra, es una buena práctica para recursos directamente utilizados.
                    using (MySqlConnection conn = cn.GetConnection()) // Ahora usamos GetConnection()
                    {
                        conn.Open(); // Abrimos explícitamente la conexión

                        // Paso 3: Mantener 'using' para el MySqlCommand, lo cual ya tenías y es correcto.
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
                            cmd.Parameters.AddWithValue("_Fecha_Actualizacion", DateTime.Now);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            mensaje = $"Filas afectadas: {rowsAffected}";
                            
                        } // cmd (MySqlCommand) se dispone 
                        Console.WriteLine($"Estado de la conexión antes de abrir: {conn.State}");
                        return mensaje;
                    } // conn (MySqlConnection) se cierra y dispone aquí
                } // cn (Conexion) se dispone aquí, lo que garantiza el cierre de la MySqlConnection interna
            }
            catch (Exception ex)
            {
                
                return $"Error: {ex.Message}";
            }

        }

        public string insert(MComputadora modelo)
        {
            string mensaje = "Error al insertar";
            string procedimiento = "InsertComputadora"; // Nombre del procedimiento almacenado

            try
            {
                // Paso 1: Usar 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Paso 2: Usar 'using' para la MySqlConnection obtenida de Conexion.
                    using (MySqlConnection conn = cn.GetConnection()) // Ahora usamos GetConnection()
                    {
                        conn.Open(); // Abrimos explícitamente la conexión

                        // Paso 3: Mantener 'using' para el MySqlCommand, lo cual ya tenías y es correcto.
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
                            cmd.Parameters.AddWithValue("_Contra", modelo.Contra);
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
                            mensaje = "Guardado exitosamente";
                        } // cmd (MySqlCommand) se dispone aquí
                    } // conn (MySqlConnection) se cierra y dispone aquí
                } // cn (Conexion) se dispone aquí, lo que garantiza el cierre de la MySqlConnection interna
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    //mensaje = $"Error de duplicado: El No Inventario '{modelo.No_Inventario}' o la serie '{modelo.Serie}' ya existe. Por favor, ingrese un dato diferente.";
                    mensaje = $"Fallo al guardar: El activo no se pudo registrar porque el No.Inventario '{modelo.No_Inventario}' o la Serie '{modelo.Serie}' ya existen en la base de datos.";
                    Console.WriteLine($"Error MySql (1062): {ex.Message}");
                }
                else
                {
                    mensaje = $"Error de base de datos inesperado: {ex.Message}";
                    Console.WriteLine($"Error MySql ({ex.Number}): {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex.Message; // Cambiado a ex.Message para un mensaje más limpio
            }

            return mensaje;
        }
    }


}


    
    

