using System;
using MySql.Data.MySqlClient;

public class Conexion
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    // Constructor
    public Conexion()
    {
        Initialize();
    }

    // Inicializa los valores de la conexión
    private void Initialize()
    {
        server = "bnn2mmcbgsjf5thnipcz-mysql.services.clever-cloud.com";
        database = "bnn2mmcbgsjf5thnipcz";
        uid = "uhr5sob5aywl7btt";
        password = "kT0rcQZXejpOVHlmyz4d";
        string connectionString;
        connectionString = $"DataSource={server};DATABASE={database};User Id={uid};Password={password}";

        connection = new MySqlConnection(connectionString);
    }

    // Método para abrir la conexión
    public MySqlConnection OpenConnection()
    {
       // connection.Open();
        return connection;
    }

    // Método para cerrar la conexión
    public bool CloseConnection()
    {
        try
        {
            connection.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            // Manejar excepciones de cierre de conexión aquí
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
