// ...
using System.Data;
using Inventario.Models;
using MySql.Data.MySqlClient;

public class ImSucursales
{
    public List<MSucursales> getSucursalesActivas()
    {
        var sucursales = new List<MSucursales>();
        string procedimiento = "GetSucursalesActivas";

        using (Conexion cn = new Conexion())
        {
            using (MySqlConnection conn = cn.GetConnection())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(procedimiento, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sucursales.Add(new MSucursales
                            {
                                ID = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Abrev = reader.GetString("abrev"),
                                Activa = reader.GetBoolean("activa")
                            });
                        }
                    }
                }
            }
        } // Si hay un error, una excepción se lanzará desde aquí

        return sucursales;
    }
}