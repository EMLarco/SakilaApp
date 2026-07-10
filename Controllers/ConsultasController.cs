using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SakilaApp.Data;
using SakilaApp.Models;

namespace SakilaApp.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private readonly SakilaContext _context;

        public ConsultasController(SakilaContext context)
        {
            _context = context;
        }

        // ===========================
        // CONSULTAS OBLIGATORIAS
        // ===========================

        // 1. Where: Películas con duración entre 90 y 120 minutos
        public async Task<IActionResult> WhereDuracion(int? min, int? max)
        {
            int minVal = min ?? 90;
            int maxVal = max ?? 120;
            var peliculas = await _context.Films
                .Where(f => f.Length >= minVal && f.Length <= maxVal)
                .OrderBy(f => f.Title)
                .ToListAsync();
            ViewBag.Min = minVal;
            ViewBag.Max = maxVal;
            return View(peliculas);
        }

        // 2. OrderBy: Películas ordenadas alfabéticamente por título
        public async Task<IActionResult> OrderByTitulo()
        {
            var peliculas = await _context.Films
                .OrderBy(f => f.Title)
                .Take(50)
                .ToListAsync();
            return View(peliculas);
        }

        // 3. OrderByDescending: Películas ordenadas por duración descendente
        public async Task<IActionResult> OrderByDuracionDesc()
        {
            var peliculas = await _context.Films
                .OrderByDescending(f => f.Length)
                .Take(50)
                .ToListAsync();
            return View(peliculas);
        }

        // 4. Take: Primeras 10 películas
        public async Task<IActionResult> TakePrimeras10()
        {
            var peliculas = await _context.Films
                .OrderBy(f => f.FilmId)
                .Take(10)
                .ToListAsync();
            return View(peliculas);
        }

        // 5. Contains / ILike: Películas cuyo título contenga una palabra
        public async Task<IActionResult> ContainsTitulo(string texto = "LOVE")
        {
            var peliculas = await _context.Films
                .Where(f => EF.Functions.ILike(f.Title, $"%{texto}%"))
                .OrderBy(f => f.Title)
                .ToListAsync();
            ViewBag.Texto = texto;
            return View(peliculas);
        }

        // ===========================
        // RETOS SELECCIONADOS
        // ===========================

        // RETO 1: Películas duración entre 90 y 120 minutos (ya cubierto en Where, se redirige)
        public async Task<IActionResult> Reto1Duracion90_120() => await WhereDuracion(90, 120);

        // RETO 2: Las 10 películas más largas
        public async Task<IActionResult> Reto2Las10MasLargas()
        {
            var peliculas = await _context.Films
                .OrderByDescending(f => f.Length)
                .Take(10)
                .ToListAsync();
            return View(peliculas);
        }

        // RETO 4: Películas de la categoría Drama
        public async Task<IActionResult> Reto4PeliculasDrama()
        {
            var peliculas = await _context.FilmCategories
                .Include(fc => fc.Film)
                .Include(fc => fc.Category)
                .Where(fc => fc.Category.Name == "Drama")
                .Select(fc => fc.Film)
                .OrderBy(f => f.Title)
                .ToListAsync();
            return View(peliculas);
        }

        // RETO 6: Películas de Action ordenadas de la más larga a la más corta
        public async Task<IActionResult> Reto6ActionOrdenadas()
        {
            var peliculas = await _context.FilmCategories
                .Include(fc => fc.Film)
                .Include(fc => fc.Category)
                .Where(fc => fc.Category.Name == "Action")
                .OrderByDescending(fc => fc.Film.Length)
                .Select(fc => fc.Film)
                .ToListAsync();
            return View(peliculas);
        }

        // RETO 10: Clientes cuyo apellido contenga una cadena específica
        public async Task<IActionResult> Reto10ClientesApellido(string patron = "son")
        {
            var clientes = await _context.Customers
                .Where(c => EF.Functions.ILike(c.LastName, $"%{patron}%"))
                .OrderBy(c => c.LastName)
                .ToListAsync();
            ViewBag.Patron = patron;
            return View(clientes);
        }
    }
}
