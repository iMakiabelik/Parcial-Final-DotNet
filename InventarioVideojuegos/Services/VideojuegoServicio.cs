using System;
using System.Collections.Generic;
using InventarioVideojuegos.Data;
using InventarioVideojuegos.Models;

namespace InventarioVideojuegos.Services;

public class VideojuegoServicio
{
    private readonly VideojuegoDAO _dao;

    public VideojuegoServicio(VideojuegoDAO dao)
    {
        _dao = dao;
    }

    public void RegistrarVideojuego(Videojuego juego)
    {
        Validar(juego);
        _dao.Insertar(juego);
    }

    public List<Videojuego> ListarVideojuegos()
    {
        return _dao.ObtenerTodos();
    }

    public void ActualizarVideojuego(Videojuego juego)
    {
        if (juego.Id <= 0) throw new ArgumentException("Seleccione un videojuego de la lista para actualizar.");
        Validar(juego);
        if (!_dao.Actualizar(juego)) throw new InvalidOperationException("No se encontró el videojuego en la base de datos.");
    }

    public void EliminarVideojuego(int id)
    {
        if (id <= 0) throw new ArgumentException("Seleccione un videojuego de la lista para eliminar.");
        if (!_dao.Eliminar(id)) throw new InvalidOperationException("No se pudo eliminar el registro.");
    }

    private void Validar(Videojuego juego)
    {
        if (string.IsNullOrWhiteSpace(juego.Titulo)) throw new ArgumentException("El título del videojuego es obligatorio.");
        if (string.IsNullOrWhiteSpace(juego.Genero)) throw new ArgumentException("Seleccione un género válido.");
        if (string.IsNullOrWhiteSpace(juego.Plataforma)) throw new ArgumentException("Seleccione una plataforma válida.");
        if (juego.Precio <= 0) throw new ArgumentException("El precio debe ser mayor a 0.");
        if (juego.Stock < 0) throw new ArgumentException("El stock no puede ser negativo.");
    }
}
