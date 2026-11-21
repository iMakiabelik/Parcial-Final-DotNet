# Sistema de Gestión de Inventario de Videojuegos

Aplicación de escritorio desarrollada en **C# (.NET 8)** bajo el paradigma de **Programación Orientada a Objetos (POO)**, con interfaz gráfica multiplataforma en **Avalonia UI** y persistencia relacional local mediante **ADO.NET** y **SQLite**.

* **Autor:** Juan David Vellojin Donado
* **Programa:** Ingeniería de Sistemas — Corporación Universitaria Americana
* **Asignatura:** Examen Final Práctico (.NET / C#)

---

## 1. Arquitectura y Diseño Orientado a Objetos (POO)

El proyecto aplica separación de responsabilidades en cuatro capas dentro de `InventarioVideojuegos/`:

* **`Models/Videojuego.cs` (Modelo de Dominio):** Implementa encapsulamiento mediante atributos privados (`_id`, `_titulo`, `_genero`, `_plataforma`, `_precio`, `_stock`) y propiedades públicas con normalización de cadenas, sobrecarga de constructores y sobrescritura de `ToString()` junto con `ObtenerResumen()`.
* **`Data/DatabaseHelper.cs` (Gestión de Conexión):** Centraliza la cadena de conexión a `inventario_videojuegos.db`, abre instancias de `SqliteConnection` y ejecuta el script DDL (`CREATE TABLE IF NOT EXISTS Videojuegos`) al iniciar el sistema.
* **`Data/VideojuegoDAO.cs` (Acceso a Datos con ADO.NET):** Implementa las operaciones CRUD (`Insertar`, `ObtenerTodos`, `Actualizar`, `Eliminar`) utilizando `SqliteCommand` con parámetros (`@titulo`, `@genero`, `@plataforma`, `@precio`, `@stock`, `@id`) para prevenir inyección SQL y `SqliteDataReader` para mapear registros a objetos `Videojuego`.
* **`Services/VideojuegoServicio.cs` (Lógica de Negocio y Validaciones):** Desacopla las reglas de dominio de la vista y de la base de datos, validando campos obligatorios, precio mayor a cero, stock no negativo e identificadores válidos antes de invocar al DAO.
* **`MainWindow.axaml` y `MainWindow.axaml.cs` (Interfaz Gráfica):** Vista declarativa en XAML con formulario de captura, selector de género y plataforma, listado interactivo (`ListBox`) y barra de notificaciones en tiempo real para operaciones exitosas y errores controlados.

---

## 2. Estructura del Repositorio

```text
Parcial-Final-DotNet/
├── README.md
├── .gitignore
├── evidencias/
│   ├── 01_interfaz_inicial.png
│   ├── 02_validacion_error.png
│   ├── 03_crear_registros.png
│   ├── 04_actualizar_registro.png
│   ├── 05_eliminar_registro.png
│   └── 06_persistencia_sqlite.png
└── InventarioVideojuegos/
    ├── InventarioVideojuegos.csproj
    ├── Program.cs
    ├── App.axaml / App.axaml.cs
    ├── MainWindow.axaml / MainWindow.axaml.cs
    ├── Models/
    │   └── Videojuego.cs
    ├── Data/
    │   ├── DatabaseHelper.cs
    │   └── VideojuegoDAO.cs
    └── Services/
        └── VideojuegoServicio.cs