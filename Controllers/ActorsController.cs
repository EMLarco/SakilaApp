using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Models;

namespace SakilaApp.Controllers;

[Authorize]
public class ActorsController : Controller
{
    private readonly SakilaContext _context;

    public ActorsController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var actors = await _context.Actors.Where(a => !a.IsDeleted).ToListAsync();
        return View(actors);
    }

    public async Task<IActionResult> Details(int id)
    {
        var actor = await _context.Actors
            .Include(a => a.FilmActors)
            .ThenInclude(fa => fa.Film)
            .FirstOrDefaultAsync(a => a.ActorId == id && !a.IsDeleted);
        if (actor == null) return NotFound();
        return View(actor);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Actor actor)
    {
        if (!ModelState.IsValid) return View(actor);
        actor.LastUpdate = DateTime.Now;
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Actor creado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.ActorId == id && !a.IsDeleted);
        if (actor == null) return NotFound();
        return View(actor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Actor actor)
    {
        if (id != actor.ActorId) return BadRequest();
        if (!ModelState.IsValid) return View(actor);
        actor.LastUpdate = DateTime.Now;
        _context.Actors.Update(actor);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Actor actualizado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.ActorId == id && !a.IsDeleted);
        if (actor == null) return NotFound();
        return View(actor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor != null)
        {
            actor.IsDeleted = true;   // eliminación lógica
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Actor eliminado correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}