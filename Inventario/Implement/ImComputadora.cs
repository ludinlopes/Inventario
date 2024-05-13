﻿using Inventario.Models;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Inventario.Implement
{

    public class ImComputadora
    {
        private Conexion cn;

        public MComputadora getEmple(string noInv)
        {
            cn = new Conexion();
            MySqlDataReader mySqlDataReader;
            MComputadora compu = new MComputadora();
            MySqlDataReader DR = null;
            string consulta = $"SELECT * FROM Computadoras WHERE No_Inventario = '{noInv}'";
            if (cn.OpenConnection() != null)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(consulta);
                mySqlCommand.Connection = cn.OpenConnection();
                mySqlCommand.Connection.Open();
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    /////////////////////llenado de modelo/////////////////////////////////////////////
                    int no = mySqlDataReader.GetInt32("Cod_Empleado");
                    compu.cod_Emple = no.ToString();
                    compu.Nombre = "nombre";
                    compu.NoInmentario = mySqlDataReader.GetString("No_Inventario");
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
                    compu.Dominio = mySqlDataReader.GetString("Dominio");
                    compu.Usuario = mySqlDataReader.GetString("Usuario");
                    compu.Estado = mySqlDataReader.GetString("Estado");
                    compu.Condicion = mySqlDataReader.GetString("Condicion");

                    ////////////////////////////////////////////////////////////////////////////////////////////
                }
                mySqlCommand.Connection.Close();
            }
            return compu;
        }

        public string setComputadora(MComputadora compu)
        {
            cn = new Conexion();
            string consulta = $"UPDATE Computadoras set " +
                $"  Cod_Empleado = '{compu.cod_Emple}'" +
                $", Estado = '{compu.Estado}'" +
                $", Tipo = '{compu.Tipo}'" +
                $", Dominio = '{compu.Dominio}'" +
                $", Usuario = '{compu.Usuario}'" +
                $", Marca = '{compu.Marca}'" +
                $", Modelo = '{compu.Modelo}'" +
                $", Serie = '{compu.Serie}'" +
                $", Procesador = '{compu.Procesador}'" +
                $", Generacion = '{compu.Generacion}'" +
                $", Tipo_Disco = '{compu.TipoDisco}'" +
                $", Capacidad_Disco = '{compu.CapacidadDisco}'" +
                $", Ram = '{compu.Ram}'" +
                $", Mac_Address = '{compu.MacAddress}'" +
                $", No_IP = '{compu.NoIp}'" +
                $", Mouse = '{compu.Mouse}'" +
                $", Teclado = '{compu.Teclado}'" +
                $", Condicion = '{compu.Condicion}'" +
                $", Fecha_Actualizacion = CURDATE() " +
                $" WHERE No_Inventario = '{compu.NoInmentario}'";
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
                return $"Error: {ex.Message}           "+consulta;
            }
        }
    }

}

    
    

