namespace InventarioVideojuegos.Models;

public class Videojuego
{
    private int _id;
    private string _titulo = string.Empty;
    private string _genero = string.Empty;
    private string _plataforma = string.Empty;
    private decimal _precio;
    private int _stock;

    public int Id { get => _id; set => _id = value; }
    public string Titulo { get => _titulo; set => _titulo = value?.Trim() ?? string.Empty; }
    public string Genero { get => _genero; set => _genero = value?.Trim() ?? string.Empty; }
    public string Plataforma { get => _plataforma; set => _plataforma = value?.Trim() ?? string.Empty; }
    public decimal Precio { get => _precio; set => _precio = value; }
    public int Stock { get => _stock; set => _stock = value; }

    public Videojuego() { }

    public Videojuego(string titulo, string genero, string plataforma, decimal precio, int stock)
    {
        Titulo = titulo;
        Genero = genero;
        Plataforma = plataforma;
        Precio = precio;
        Stock = stock;
    }

    public Videojuego(int id, string titulo, string genero, string plataforma, decimal precio, int stock)
        : this(titulo, genero, plataforma, precio, stock)
    {
        Id = id;
    }

    public string ObtenerResumen()
    {
        return $"#{Id} | {Titulo} ({Plataforma}) - {Genero} | ${Precio:N2} | Stock: {Stock}";
    }

    public override string ToString() => ObtenerResumen();
}
