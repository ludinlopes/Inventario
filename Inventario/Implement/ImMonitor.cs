﻿using Inventario.Models;
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
            string consulta = $"SELECT * FROM Monitores WHERE No_Inventario = '{NoInv}'";
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
                    mon.Nombre = "Nombre";
                    mon.NoInventario = mySqlDataReader.GetString("No_Inventario");
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
                $", Marca = '{modelo.Marca}'" +
                $", Modelo = '{modelo.Modelo}'" +
                $", Serie = '{modelo.Serie}'" +
                $", Estado = '{modelo.Estado}'" +
                $", Condicion = '{modelo.Condicion}'" +
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
                return $"Error: {ex.Message}";
            }
        }
    }
}
