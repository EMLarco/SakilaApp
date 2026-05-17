using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Models;

namespace SakilaApp.Controllers;

[Authorize]
public class RentalsController : Controller
{
    private readonly SakilaContext _context;

    public RentalsController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals.Where(r => !r.IsDeleted).ToListAsync();
        return View(rentals);
    }

    public async Task<IActionResult> Details(int id)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id && !r.IsDeleted);
        if (rental == null) return NotFound();
        return View(rental);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rental rental)
    {
        if (!ModelState.IsValid) return View(rental);
        rental.LastUpdate = DateTime.Now;
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Alquiler creado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id && !r.IsDeleted);
        if (rental == null) return NotFound();
        return View(rental);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Rental rental)
    {
        if (id != rental.RentalId) return BadRequest();
        if (!ModelState.IsValid) return View(rental);
        rental.LastUpdate = DateTime.Now;
        _context.Rentals.Update(rental);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Alquiler actualizado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == id && !r.IsDeleted);
        if (rental == null) return NotFound();
        return View(rental);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            rental.IsDeleted = true;   // eliminación lógica
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Alquiler eliminado correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}