using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Data; // Necesario para CommandType

namespace Inventario.Implement
{
    public class ImInvGeneral
    {
        // Ya no necesitas 'private Conexion cn;' aquí.
        // Cada método creará y gestionará su propia instancia de Conexion
        // dentro de un bloque 'using'.

        public List<MInvGeneral> getInventario(string sucursal)
        {
            var inv = new List<MInvGeneral>();
            // Asumo que 'getInventario' es un procedimiento almacenado sin parámetros
            string procedimiento = "getInventario3";
            // Ya tienes la sucursal disponible aquí
            Console.WriteLine("Sucursal - InInvGeneral: " + sucursal);
            try
            {
                // Inicia el bloque 'using' para la instancia de Conexion.
                using (Conexion cn = new Conexion())
                {
                    // Obtiene la MySqlConnection y la envuelve en un 'using'.
                    using (MySqlConnection conn = cn.GetConnection())
                    {
                        conn.Open(); // Abre la conexión explícitamente

                        // PASO CLAVE 1: Establecer la variable de sesión de MySQL
                        // Esto se hace ANTES de llamar al procedimiento almacenado.
                        using (MySqlCommand setSessionVarCommand = new MySqlCommand("SET @sucursal_a_filtrar = @sucursal_param", conn))
                        {
                            // Agregamos el parámetro C# con el valor que recibimos en el método
                            setSessionVarCommand.Parameters.AddWithValue("@sucursal_param", "1");
                            setSessionVarCommand.ExecuteNonQuery();
                        }

                        // Envuelve el MySqlCommand en un 'using'.
                        using (MySqlCommand mySqlCommand = new MySqlCommand(procedimiento, conn))
                        {
                            mySqlCommand.CommandType = CommandType.StoredProcedure; // Indicamos que es un procedimiento almacenado

                            // Envuelve el MySqlDataReader en un 'using'.
                            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                            {
                                // --- INICIO de la parte que pediste mantener tal cual ---
                                Console.WriteLine(mySqlDataReader.Read());
                                while (mySqlDataReader.Read())
                                {
                                    var minv = new MInvGeneral();

                                    minv.Linea = mySqlDataReader.GetString("Linea");
                                    minv.Cod_Empleado = mySqlDataReader.GetString("Cod_Empleado");
                                    minv.Nombre = mySqlDataReader.GetString("Nombre");
                                    minv.Area = mySqlDataReader.GetString("Area");
                                    minv.Estado = mySqlDataReader.GetString("Estado");
                                    minv.Sucursal = mySqlDataReader.GetString("Sucursal");
                                    minv.Cod_Empleado_Com = mySqlDataReader.GetString("Cod_Empleado_Com");
                                    minv.Estado_Com = mySqlDataReader.GetString("Estado_Com");
                                    minv.No_Inventario_Com = mySqlDataReader.GetString("No_Inventario_Com");
                                    minv.Tipo_Com = mySqlDataReader.GetString("Tipo_Com");
                                    minv.NombrePc_Com = mySqlDataReader.GetString("NombrePc_Com");
                                    minv.Dominio_Com = mySqlDataReader.GetString("Dominio_Com");
                                    minv.Usuario_Com = mySqlDataReader.GetString("Usuario_Com");
                                    minv.Contra_Com = mySqlDataReader.GetString("Contra_Com");
                                    minv.Marca_Com = mySqlDataReader.GetString("Marca_Com");
                                    minv.Modelo_Com = mySqlDataReader.GetString("Modelo_Com");
                                    minv.Serie_Com = mySqlDataReader.GetString("Serie_Com");
                                    minv.Procesador_Com = mySqlDataReader.GetString("Procesador_Com");
                                    minv.Generacion_Com = mySqlDataReader.GetString("Generacion_Com");
                                    minv.Tipo_Disco_Com = mySqlDataReader.GetString("Tipo_Disco_Com");
                                    minv.Capacidad_Disco_Com = mySqlDataReader.GetString("Capacidad_Disco_Com");
                                    minv.Ram_Com = mySqlDataReader.GetString("Ram_Com");
                                    minv.Mac_Address_Com = mySqlDataReader.GetString("Mac_Address_Com");
                                    minv.No_IP_Com = mySqlDataReader.GetString("No_IP_Com");
                                    minv.Mouse_Com = mySqlDataReader.GetString("Mouse_Com");
                                    minv.Teclado_Com = mySqlDataReader.GetString("Teclado_Com");
                                    minv.Condicion_Com = mySqlDataReader.GetString("Condicion_Com");
                                    // Asegurarse de manejar valores nulos para fechas si es posible en DB
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Com")))
                                    {
                                        minv.Fecha_Actualizacion_Com = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Com").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Com = ""; // O un valor predeterminado
                                    }

                                    minv.Cod_Empleado_Mon = mySqlDataReader.GetString("Cod_Empleado_Mon");
                                    minv.No_Inventario_Mon = mySqlDataReader.GetString("No_Inventario_Mon");
                                    minv.Marca_Mon = mySqlDataReader.GetString("Marca_Mon");
                                    minv.Modelo_Mon = mySqlDataReader.GetString("Modelo_Mon");
                                    minv.Serie_Mon = mySqlDataReader.GetString("Serie_Mon");
                                    minv.Estado_Mon = mySqlDataReader.GetString("Estado_Mon");
                                    minv.Condicion_Mon = mySqlDataReader.GetString("Condicion_Mon");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Mon")))
                                    {
                                        minv.Fecha_Actualizacion_Mon = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Mon").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Mon = "";
                                    }

                                    minv.Cod_Empleado_Imp = mySqlDataReader.GetString("Cod_Empleado_Imp");
                                    minv.No_Inventario_Imp = mySqlDataReader.GetString("No_Inventario_Imp");
                                    minv.Marca_Imp = mySqlDataReader.GetString("Marca_Imp");
                                    minv.Modelo_Imp = mySqlDataReader.GetString("Modelo_Imp");
                                    minv.Serie_Imp = mySqlDataReader.GetString("Serie_Imp");
                                    minv.Tipo_Imp = mySqlDataReader.GetString("Tipo_Imp");
                                    minv.Estado_Imp = mySqlDataReader.GetString("Estado_Imp");
                                    minv.Condicion_Imp = mySqlDataReader.GetString("Condicion_Imp");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Imp")))
                                    {
                                        minv.Fecha_Actualizacion_Imp = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Imp").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Imp = "";
                                    }

                                    minv.Cod_Empleado_Scn = mySqlDataReader.GetString("Cod_Empleado_Scn");
                                    minv.No_Inventario_Scn = mySqlDataReader.GetString("No_Inventario_Scn");
                                    minv.Marca_Scn = mySqlDataReader.GetString("Marca_Scn");
                                    minv.Modelo_Scn = mySqlDataReader.GetString("Modelo_Scn");
                                    minv.Serie_Scn = mySqlDataReader.GetString("Serie_Scn");
                                    minv.Estado_Scn = mySqlDataReader.GetString("Estado_Scn");
                                    minv.Condicion_Scn = mySqlDataReader.GetString("Condicion_Scn");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Scn")))
                                    {
                                        minv.Fecha_Actualizacion_Scn = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Scn").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Scn = "";
                                    }

                                    minv.Cod_Empleado_Ups = mySqlDataReader.GetString("Cod_Empleado_Ups");
                                    minv.No_Inventario_Ups = mySqlDataReader.GetString("No_Inventario_Ups");
                                    minv.Marca_Ups = mySqlDataReader.GetString("Marca_Ups");
                                    minv.Modelo_Ups = mySqlDataReader.GetString("Modelo_Ups");
                                    minv.Serie_Ups = mySqlDataReader.GetString("Serie_Ups");
                                    minv.Estado_Ups = mySqlDataReader.GetString("Estado_Ups");
                                    minv.Condicion_Ups = mySqlDataReader.GetString("Condicion_Ups");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Ups")))
                                    {
                                        minv.Fecha_Actualizacion_Ups = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Ups").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Ups = "";
                                    }

                                    minv.Cod_Empleado_Tel = mySqlDataReader.GetString("Cod_Empleado_Tel");
                                    minv.No_Inventario_Tel = mySqlDataReader.GetString("No_Inventario_Tel");
                                    minv.Marca_Tel = mySqlDataReader.GetString("Marca_Tel");
                                    minv.Modelo_Tel = mySqlDataReader.GetString("Modelo_Tel");
                                    minv.Serie_Tel = mySqlDataReader.GetString("Serie_Tel");
                                    minv.Tipo_Tel = mySqlDataReader.GetString("Tipo_Tel");
                                    minv.Estado_Tel = mySqlDataReader.GetString("Estado_Tel");
                                    minv.Condicion_Tel = mySqlDataReader.GetString("Condicion_Tel");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Tel")))
                                    {
                                        minv.Fecha_Actualizacion_Tel = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Tel").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Tel = "";
                                    }

                                    minv.Cod_Empleado_Cel = mySqlDataReader.GetString("Cod_Empleado_Cel");
                                    minv.Marca_Cel = mySqlDataReader.GetString("Marca_Cel");
                                    minv.Modelo_Cel = mySqlDataReader.GetString("Modelo_Cel");
                                    minv.IMEI_Cel = mySqlDataReader.GetString("IMEI_Cel");
                                    minv.Estado_Cel = mySqlDataReader.GetString("Estado_Cel");
                                    minv.Condicion_Cel = mySqlDataReader.GetString("Condicion_Cel");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Cel")))
                                    {
                                        minv.Fecha_Actualizacion_Cel = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Cel").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Cel = "";
                                    }

                                    minv.Cod_Empleado_Tab = mySqlDataReader.GetString("Cod_Empleado_Tab");
                                    minv.Marca_Tab = mySqlDataReader.GetString("Marca_Tab");
                                    minv.Modelo_Tab = mySqlDataReader.GetString("Modelo_Tab");
                                    minv.IMEI_Tab = mySqlDataReader.GetString("IMEI_Tab");
                                    minv.Estado_Tab = mySqlDataReader.GetString("Estado_Tab");
                                    minv.Condicion_Tab = mySqlDataReader.GetString("Condicion_Tab");
                                    if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("Fecha_Actualizacion_Tab")))
                                    {
                                        minv.Fecha_Actualizacion_Tab = mySqlDataReader.GetDateTime("Fecha_Actualizacion_Tab").ToString();
                                    }
                                    else
                                    {
                                        minv.Fecha_Actualizacion_Tab = "";
                                    }

                                    inv.Add(minv);
                                }
                                // --- FIN de la parte que pediste mantener tal cual ---
                            } // mySqlDataReader se cierra y se dispone aquí.
                        } // mySqlCommand se cierra y se dispone aquí.
                    } // conn (MySqlConnection) se cierra y se dispone aquí.
                } // cn (Conexion) se dispone aquí, asegurando el cierre de la MySqlConnection interna.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener inventario general: {ex.Message}");
                // Puedes optar por lanzar la excepción o devolver una lista vacía.
            }
            // --- INICIO de la parte que pediste mantener tal cual ---
            return inv;
            // --- FIN de la parte que pediste mantener tal cual ---
        }
    }
}