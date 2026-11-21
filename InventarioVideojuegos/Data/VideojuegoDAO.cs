using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using InventarioVideojuegos.Models;

namespace InventarioVideojuegos.Data;

public class VideojuegoDAO
{
    private readonly DatabaseHelper _dbHelper;

    public VideojuegoDAO(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
    }

    public void Insertar(Videojuego juego)
    {
        using var conexion = _dbHelper.ObtenerConexion();
        string sql = "INSERT INTO Videojuegos (Titulo, Genero, Plataforma, Precio, Stock) VALUES (@titulo, @genero, @plataforma, @precio, @stock);";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@titulo", juego.Titulo);
        comando.Parameters.AddWithValue("@genero", juego.Genero);
        comando.Parameters.AddWithValue("@plataforma", juego.Plataforma);
        comando.Parameters.AddWithValue("@precio", Convert.ToDouble(juego.Precio));
        comando.Parameters.AddWithValue("@stock", juego.Stock);
        comando.ExecuteNonQuery();
    }

    public List<Videojuego> ObtenerTodos()
    {
        var lista = new List<Videojuego>();
        using var conexion = _dbHelper.ObtenerConexion();
        string sql = "SELECT Id, Titulo, Genero, Plataforma, Precio, Stock FROM Videojuegos ORDER BY Id DESC;";
        using var comando = new SqliteCommand(sql, conexion);
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Videojuego(lector.GetInt32(0), lector.GetString(1), lector.GetString(2), lector.GetString(3), Convert.ToDecimal(lector.GetDouble(4)), lector.GetInt32(5)));
        }
        return lista;
    }

    public bool Actualizar(Videojuego juego)
    {
        using var conexion = _dbHelper.ObtenerConexion();
        string sql = "UPDATE Videojuegos SET Titulo = @titulo, Genero = @genero, Plataforma = @plataforma, Precio = @precio, Stock = @stock WHERE Id = @id;";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@titulo", juego.Titulo);
        comando.Parameters.AddWithValue("@genero", juego.Genero);
        comando.Parameters.AddWithValue("@plataforma", juego.Plataforma);
        comando.Parameters.AddWithValue("@precio", Convert.ToDouble(juego.Precio));
        comando.Parameters.AddWithValue("@stock", juego.Stock);
        comando.Parameters.AddWithValue("@id", juego.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = _dbHelper.ObtenerConexion();
        string sql = "DELETE FROM Videojuegos WHERE Id = @id;";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@id", id);
        return comando.ExecuteNonQuery() > 0;
    }
}
