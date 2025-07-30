/*using System;
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
        server = "be64rlwsmzynyxdwrk8w-mysql.services.clever-cloud.com";
        database = "be64rlwsmzynyxdwrk8w";
        uid = "uhr5sob5aywl7btt";
        password = "kT0rcQZXejpOVHlmyz4d";
        string connectionString;
        connectionString = $"DataSource={server};DATABASE={database};User Id={uid};Password={password}";

        connection = new MySqlConnection(connectionString);
    }

    // Método para abrir la conexión
    public MySqlConnection OpenConnection()
    {
        //connection.Open();
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
}*/

using System;
using MySql.Data.MySqlClient;
using System.Data; // Añadido: Importante para ConnectionState

public class Conexion : IDisposable // ¡Aquí le decimos que implementa IDisposable!
{
    private MySqlConnection connection;
    private string server = "";
    private string database = "";
    private string uid = "";
    private string password = "";

    // Constructor
    public Conexion() => Initialize();

    // Inicializa los valores de la conexión
    private void Initialize()
    {
        server = "be64rlwsmzynyxdwrk8w-mysql.services.clever-cloud.com";
        database = "be64rlwsmzynyxdwrk8w";
        uid = "uhr5sob5aywl7btt";
        password = "kT0rcQZXejpOVHlmyz4d";
        string connectionString;
        connectionString = $"DataSource={server};DATABASE={database};User Id={uid};Password={password}";

        connection = new MySqlConnection(connectionString);
    }

    // Método para obtener la conexión (renombrado para mayor claridad)
    public MySqlConnection GetConnection()
    {
        return connection;
    }

    // --- Inicio: Implementación de IDisposable ---

    // Paso 1: Implementar el método Dispose() requerido por IDisposable
    public void Dispose()
    {
        // Llama al método Dispose principal y le indica que libere recursos manejados.
        Dispose(true);

        // Suprime la finalización. Esto le dice al recolector de basura que ya
        // hemos limpiado los recursos, así que no es necesario llamar al finalizador.
        GC.SuppressFinalize(this);
    }

    // Paso 2: Crear el método Dispose(bool disposing) para la lógica de limpieza
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Libera recursos manejados (objetos que implementan IDisposable)
            // Aquí, nos aseguramos de que la conexión MySqlConnection se cierre y se libere.
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close(); // Cierra la conexión si está abierta
                }
                connection.Dispose(); // Libera los recursos de la conexión
                //connection = null; // Opcional: Establece la conexión a null para evitar un uso posterior
            }
        }
        // Puedes agregar lógica aquí para liberar recursos no manejados si los tuvieras.
        // En este caso, MySqlConnection es un recurso manejado.
    }

    // Paso 3 (Opcional pero recomendado para mayor robustez): Implementar un finalizador
    // Esto es un "paracaídas" por si alguien olvida llamar a Dispose().
    ~Conexion()
    {
        // Llama a Dispose(false) para indicar que los recursos se están limpiando
        // desde el finalizador (generalmente menos ideal, ya que no controlamos el tiempo).
        Dispose(false);
    }

    // --- Fin: Implementación de IDisposable ---

    // El método CloseConnection() se vuelve redundante si usas Dispose,
    // pero puedes mantenerlo por compatibilidad si otros archivos lo llaman explícitamente.
    // Sin embargo, la recomendación es usar 'using' y 'Dispose'.
    public bool CloseConnection()
    {
        try
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
