using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SakilaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SakilaApp.Controllers;

[Authorize]
public class FilmsController : Controller
{
    private readonly SakilaContext _context;

    public FilmsController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var films = await _context.Films.Where(f => !f.IsDeleted).ToListAsync();
        return View(films);
    }

    public async Task<IActionResult> Details(int id)
    {
        var film = await _context.Films
            .Include(f => f.FilmActors)
            .ThenInclude(fa => fa.Actor)
            .FirstOrDefaultAsync(f => f.FilmId == id && !f.IsDeleted);
        if (film == null) return NotFound();
        return View(film);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Film film)
    {
        if (!ModelState.IsValid) return View(film);

        // Asignar idioma por defecto (English = 1)
        if (film.LanguageId == 0)
            film.LanguageId = 1;

        film.LastUpdate = DateTime.Now;
        _context.Films.Add(film);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Película creada exitosamente";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id && !f.IsDeleted);
        if (film == null) return NotFound();
        return View(film);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Film film)
    {
        if (id != film.FilmId) return BadRequest();
        if (!ModelState.IsValid) return View(film);
        film.LastUpdate = DateTime.Now;
        _context.Films.Update(film);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Película actualizada";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var film = await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id && !f.IsDeleted);
        if (film == null) return NotFound();
        return View(film);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film != null)
        {
            film.IsDeleted = true;   // eliminación lógica
            _context.Films.Update(film);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Película eliminada correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}