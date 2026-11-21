using System.IO;
using Microsoft.Data.Sqlite;

namespace InventarioVideojuegos.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper()
    {
        string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "inventario_videojuegos.db");
        _connectionString = $"Data Source={dbPath}";
        InicializarBaseDeDatos();
    }

    public SqliteConnection ObtenerConexion()
    {
        var conexion = new SqliteConnection(_connectionString);
        conexion.Open();
        return conexion;
    }

    private void InicializarBaseDeDatos()
    {
        using var conexion = ObtenerConexion();
        string sql = "CREATE TABLE IF NOT EXISTS Videojuegos (Id INTEGER PRIMARY KEY AUTOINCREMENT, Titulo TEXT NOT NULL, Genero TEXT NOT NULL, Plataforma TEXT NOT NULL, Precio REAL NOT NULL, Stock INTEGER NOT NULL);";
        using var comando = new SqliteCommand(sql, conexion);
        comando.ExecuteNonQuery();
    }
}
