using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Data;
using SakilaApp.Models;

namespace SakilaApp.Controllers
{
    [Authorize]
    public class FilmsController : Controller
    {
        private readonly SakilaContext _context;

        public FilmsController(SakilaContext context)
        {
            _context = context;
        }

        // ==========================================
        // EJERCICIO 1
        // Mostrar las 10 primeras películas ordenadas alfabéticamente por título.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .OrderBy(f => f.Title)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 2
        // Mostrar las 5 películas más largas registradas.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .OrderByDescending(f => f.Length)
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 3
        // Mostrar las 10 películas cuyo título contenga la palabra LOVE.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => EF.Functions.ILike(f.Title, "%LOVE%"))
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 4
        // Mostrar las 10 películas cuyo título empiece con la letra A.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => EF.Functions.ILike(f.Title, "A%"))
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 5
        // Mostrar las 10 películas cuyo título termine con la letra N.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => EF.Functions.ILike(f.Title, "%N"))
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 6
        // Mostrar las 10 películas cuya duración sea mayor a 120 minutos.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => f.Length > 120)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 7
        // Mostrar las 10 películas cuyo costo de reemplazo sea menor a 20 dólares.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => f.ReplacementCost < 20)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 8
        // Mostrar las 10 películas cuya duración sea mayor a 100 minutos y cuyo costo de reemplazo sea menor a 20 dólares.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => f.Length > 100 && f.ReplacementCost < 20)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 9
        // Mostrar las 10 películas cuyo título contenga LOVE o cuya tarifa de alquiler sea 4.99.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => EF.Functions.ILike(f.Title, "%LOVE%") || f.RentalRate == 4.99m)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 10
        // Mostrar las 10 películas cuyo título empiece con A o termine con N.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Where(f => EF.Functions.ILike(f.Title, "A%") || EF.Functions.ILike(f.Title, "%N"))
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 15
        // Mostrar las 10 películas junto con el nombre de su idioma (necesita ViewModel o ViewBag).
        // Opción 1: usar ViewBag (solo para mostrar, no tipado)
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculasConIdioma = await _context.Films
        //         .Join(_context.Languages,
        //             film => film.LanguageId,
        //             lang => lang.LanguageId,
        //             (film, lang) => new { film.Title, Idioma = lang.Name })
        //         .Take(10)
        //         .ToListAsync();
        //     ViewBag.Peliculas = peliculasConIdioma;
        //     return View();
        // }
        // // En la vista Index.cshtml accedes con @ViewBag.Peliculas

        // ==========================================
        // EJERCICIO 16
        // Mostrar las 10 primeras películas cuyo idioma sea English.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.Languages,
        //             film => film.LanguageId,
        //             lang => lang.LanguageId,
        //             (film, lang) => new { film, lang })
        //         .Where(x => x.lang.Name == "English")
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 17
        // Mostrar las 10 primeras películas cuyo idioma sea English y título empiece con A.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.Languages,
        //             film => film.LanguageId,
        //             lang => lang.LanguageId,
        //             (film, lang) => new { film, lang })
        //         .Where(x => x.lang.Name == "English" && EF.Functions.ILike(x.film.Title, "A%"))
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 18
        // Mostrar las 5 películas cuyo idioma sea English o cuyo título contenga LOVE.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.Languages,
        //             film => film.LanguageId,
        //             lang => lang.LanguageId,
        //             (film, lang) => new { film, lang })
        //         .Where(x => x.lang.Name == "English" || EF.Functions.ILike(x.film.Title, "%LOVE%"))
        //         .Select(x => x.film)
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 19
        // Mostrar las 5 películas más largas cuyo idioma sea English.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.Languages,
        //             film => film.LanguageId,
        //             lang => lang.LanguageId,
        //             (film, lang) => new { film, lang })
        //         .Where(x => x.lang.Name == "English")
        //         .OrderByDescending(x => x.film.Length)
        //         .Select(x => x.film)
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 20
        // Mostrar las 10 películas pertenecientes a la categoría Action.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Action")
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 21
        // Mostrar las 5 películas más largas de la categoría Drama.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Drama")
        //         .OrderByDescending(x => x.film.Length)
        //         .Select(x => x.film)
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 22
        // Mostrar las 10 películas de categoría Comedy cuyo título contenga la letra A.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Comedy" && EF.Functions.ILike(x.film.Title, "%A%"))
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 23
        // Mostrar las 5 películas de categoría Horror, omitiendo la primera.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Horror")
        //         .OrderBy(x => x.film.Title)
        //         .Select(x => x.film)
        //         .Skip(1)
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 24
        // Mostrar las 10 películas de categoría Family ordenadas por título.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Family")
        //         .OrderBy(x => x.film.Title)
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 25
        // Mostrar las 10 películas de categoría Animation cuya duración sea mayor a 100 minutos.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Where(x => x.cat.Name == "Animation" && x.film.Length > 100)
        //         .Select(x => x.film)
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 26
        // Mostrar las 10 películas en las que participe un actor cuyo apellido empiece con S.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmActors,
        //             film => film.FilmId,
        //             fa => fa.FilmId,
        //             (film, fa) => new { film, fa })
        //         .Join(_context.Actors,
        //             temp => temp.fa.ActorId,
        //             actor => actor.ActorId,
        //             (temp, actor) => new { temp.film, actor })
        //         .Where(x => EF.Functions.ILike(x.actor.LastName, "S%"))
        //         .Select(x => x.film)
        //         .Distinct()
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 27
        // Mostrar las 5 películas en las que participe un actor cuyo nombre contenga JO.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmActors,
        //             film => film.FilmId,
        //             fa => fa.FilmId,
        //             (film, fa) => new { film, fa })
        //         .Join(_context.Actors,
        //             temp => temp.fa.ActorId,
        //             actor => actor.ActorId,
        //             (temp, actor) => new { temp.film, actor })
        //         .Where(x => EF.Functions.ILike(x.actor.FirstName, "%JO%"))
        //         .Select(x => x.film)
        //         .Distinct()
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 28
        // Mostrar las 5 películas en las que participe un actor cuyo apellido termine con N.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmActors,
        //             film => film.FilmId,
        //             fa => fa.FilmId,
        //             (film, fa) => new { film, fa })
        //         .Join(_context.Actors,
        //             temp => temp.fa.ActorId,
        //             actor => actor.ActorId,
        //             (temp, actor) => new { temp.film, actor })
        //         .Where(x => EF.Functions.ILike(x.actor.LastName, "%N"))
        //         .Select(x => x.film)
        //         .Distinct()
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 29
        // Mostrar las 10 películas en las que participe un actor cuyo nombre empiece con M y cuyo título contenga la letra A.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmActors,
        //             film => film.FilmId,
        //             fa => fa.FilmId,
        //             (film, fa) => new { film, fa })
        //         .Join(_context.Actors,
        //             temp => temp.fa.ActorId,
        //             actor => actor.ActorId,
        //             (temp, actor) => new { temp.film, actor })
        //         .Where(x => EF.Functions.ILike(x.actor.FirstName, "M%") && EF.Functions.ILike(x.film.Title, "%A%"))
        //         .Select(x => x.film)
        //         .Distinct()
        //         .Take(10)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // EJERCICIO 30
        // Mostrar las 5 películas de categoría Comedy en las que participe un actor cuyo apellido empiece con B.
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films
        //         .Join(_context.FilmCategories,
        //             film => film.FilmId,
        //             fc => fc.FilmId,
        //             (film, fc) => new { film, fc })
        //         .Join(_context.Categories,
        //             temp => temp.fc.CategoryId,
        //             cat => cat.CategoryId,
        //             (temp, cat) => new { temp.film, cat })
        //         .Join(_context.FilmActors,
        //             temp => temp.film.FilmId,
        //             fa => fa.FilmId,
        //             (temp, fa) => new { temp.film, temp.cat, fa })
        //         .Join(_context.Actors,
        //             temp => temp.fa.ActorId,
        //             actor => actor.ActorId,
        //             (temp, actor) => new { temp.film, temp.cat, actor })
        //         .Where(x => x.cat.Name == "Comedy" && EF.Functions.ILike(x.actor.LastName, "B%"))
        //         .Select(x => x.film)
        //         .Distinct()
        //         .Take(5)
        //         .ToListAsync();
        //     return View(peliculas);
        // }

        // ==========================================
        // Index ACTIVO (por defecto: todas las películas)
        // ==========================================
        // public async Task<IActionResult> Index()
        // {
        //     var peliculas = await _context.Films.Take(100).ToListAsync();
        //     return View(peliculas);
        // }

            // public async Task<IActionResult> Index(string? buscar, int? duracionMinima)
            // {
            //     var consulta = _context.Films.AsQueryable();

            //     // Filtro por título (búsqueda)
            //     if (!string.IsNullOrWhiteSpace(buscar))
            //     {
            //         consulta = consulta.Where(f => f.Title.Contains(buscar));
            //     }

            //     // Filtro por duración mínima (corregido)
            //     if (duracionMinima.HasValue)
            //     {
            //         consulta = consulta.Where(f => f.Length >= duracionMinima.Value);
            //     }

            //     var peliculas = await consulta
            //         .OrderBy(f => f.Title)
            //         .ToListAsync();

            //     ViewBag.Buscar = buscar;
            //     return View(peliculas);
            // }

// ==========================================
// EJERCICIO 25
// Mostrar las 10 películas de categoría Family ordenadas por título.
// ==========================================
// public async Task<IActionResult> Index()
// {
//     var peliculas = await _context.Films
//         .Join(_context.FilmCategories,
//             film => film.FilmId,
//             fc => fc.FilmId,
//             (film, fc) => new { film, fc })
//         .Join(_context.Categories,
//             temp => temp.fc.CategoryId,
//             cat => cat.CategoryId,
//             (temp, cat) => new { temp.film, cat })
//         .Where(x => x.cat.Name == "Family")
//         .OrderBy(x => x.film.Title)
//         .Select(x => x.film)
//         .Take(10)
//         .ToListAsync();

//     return View(peliculas);
// }
 

// ==========================================
// EJERCICIO 15
// Mostrar las 10 películas junto con el nombre de su idioma.
// ==========================================
public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
        .Join(_context.FilmActors,
            film => film.FilmId,
            fa => fa.FilmId,
            (film, fa) => new { film, fa })
        .Join(_context.Actors,
            temp => temp.fa.ActorId,
            actor => actor.ActorId,
            (temp, actor) => new { temp.film, actor })
        .Join(_context.FilmCategories,
            temp => temp.film.FilmId,
            fc => fc.FilmId,
            (temp, fc) => new { temp.film, temp.actor, fc })
        .Join(_context.Categories,
            temp => temp.fc.CategoryId,
            cat => cat.CategoryId,
            (temp, cat) => new { temp.film, temp.actor, cat })
        .Where(x => x.cat.Name == "Action" && EF.Functions.ILike(x.actor.LastName, "C%"))
        .Select(x => x.film)
        .Distinct()
        .Take(10)
        .ToListAsync();
    return View(peliculas);
}



 
        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmId,Title,Description,ReleaseYear,LanguageId,OriginalLanguageId,RentalDuration,RentalRate,Length,ReplacementCost,LastUpdate,SpecialFeatures,Fulltext")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,Title,Description,ReleaseYear,LanguageId,OriginalLanguageId,RentalDuration,RentalRate,Length,ReplacementCost,LastUpdate,SpecialFeatures,Fulltext")] Film film)
        {
            if (id != film.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.FilmId == id);
        }
    }
}
