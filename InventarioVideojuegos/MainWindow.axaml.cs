using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using InventarioVideojuegos.Data;
using InventarioVideojuegos.Models;
using InventarioVideojuegos.Services;

namespace InventarioVideojuegos;

public partial class MainWindow : Window
{
    private readonly VideojuegoServicio _servicio;
    private int _idSeleccionado = 0;

    public MainWindow()
    {
        InitializeComponent();
        _servicio = new VideojuegoServicio(new VideojuegoDAO(new DatabaseHelper()));
        CargarLista();
    }

    private void CargarLista()
    {
        var juegos = _servicio.ListarVideojuegos();
        LstVideojuegos.ItemsSource = juegos;
        TxtContador.Text = $"Total: {juegos.Count} registros (Seleccione uno para editar o eliminar)";
    }

    private Videojuego ConstruirDesdeFormulario()
    {
        string titulo = TxtTitulo.Text ?? string.Empty;
        string genero = (CmbGenero.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;
        string plataforma = (CmbPlataforma.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;

        string precioTxt = (TxtPrecio.Text ?? string.Empty).Replace(",", ".");
        if (!decimal.TryParse(precioTxt, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio))
            throw new ArgumentException("El precio debe ser numérico (ej. 59.99).");

        if (!int.TryParse(TxtStock.Text, out int stock))
            throw new ArgumentException("El stock debe ser entero (ej. 10).");

        return new Videojuego(_idSeleccionado, titulo, genero, plataforma, precio, stock);
    }

    private void BtnGuardar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            _idSeleccionado = 0;
            var nuevo = ConstruirDesdeFormulario();
            _servicio.RegistrarVideojuego(nuevo);
            LimpiarFormulario();
            CargarLista();
            MostrarMensaje($"Videojuego [{nuevo.Titulo}] registrado en SQLite.", false);
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error: {ex.Message}", true);
        }
    }

    private void BtnActualizar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var act = ConstruirDesdeFormulario();
            _servicio.ActualizarVideojuego(act);
            LimpiarFormulario();
            CargarLista();
            MostrarMensaje($"Videojuego #{act.Id} [{act.Titulo}] actualizado.", false);
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error: {ex.Message}", true);
        }
    }

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            int id = _idSeleccionado;
            _servicio.EliminarVideojuego(id);
            LimpiarFormulario();
            CargarLista();
            MostrarMensaje($"Videojuego #{id} eliminado.", false);
        }
        catch (Exception ex)
        {
            MostrarMensaje($"Error: {ex.Message}", true);
        }
    }

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        LimpiarFormulario();
        MostrarMensaje("Formulario limpio.", false);
    }

    private void LstVideojuegos_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LstVideojuegos.SelectedItem is Videojuego s)
        {
            _idSeleccionado = s.Id;
            TxtIdSeleccionado.Text = $"Modo: Editando #{s.Id}";
            TxtTitulo.Text = s.Titulo;
            TxtPrecio.Text = s.Precio.ToString(CultureInfo.InvariantCulture);
            TxtStock.Text = s.Stock.ToString();
            SeleccionarCombo(CmbGenero, s.Genero);
            SeleccionarCombo(CmbPlataforma, s.Plataforma);
            MostrarMensaje($"Seleccionado: {s.Titulo} (#{s.Id})", false);
        }
    }

    private void SeleccionarCombo(ComboBox combo, string valor)
    {
        foreach (var item in combo.Items)
        {
            if (item is ComboBoxItem cbi && string.Equals(cbi.Content?.ToString(), valor, StringComparison.OrdinalIgnoreCase))
            {
                combo.SelectedItem = cbi;
                return;
            }
        }
        combo.SelectedIndex = -1;
    }

    private void LimpiarFormulario()
    {
        _idSeleccionado = 0;
        TxtIdSeleccionado.Text = "Modo: Nuevo registro";
        TxtTitulo.Text = string.Empty;
        TxtPrecio.Text = string.Empty;
        TxtStock.Text = string.Empty;
        CmbGenero.SelectedIndex = -1;
        CmbPlataforma.SelectedIndex = -1;
        LstVideojuegos.SelectedItem = null;
    }

    private void MostrarMensaje(string msg, bool error)
    {
        TxtMensaje.Text = msg;
        BannerMensaje.Background = SolidColorBrush.Parse(error ? "#FEE2E2" : "#DCFCE7");
        TxtMensaje.Foreground = SolidColorBrush.Parse(error ? "#991B1B" : "#166534");
    }
}