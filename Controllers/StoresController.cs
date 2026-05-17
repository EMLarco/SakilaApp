using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Models;

namespace SakilaApp.Controllers;

[Authorize]
public class StoresController : Controller
{
    private readonly SakilaContext _context;

    public StoresController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var stores = await _context.Stores.Where(s => !s.IsDeleted).ToListAsync();
        return View(stores);
    }

    public async Task<IActionResult> Details(byte id)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreId == id && !s.IsDeleted);
        if (store == null) return NotFound();
        return View(store);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Store store)
    {
        if (!ModelState.IsValid) return View(store);
        store.LastUpdate = DateTime.Now;
        _context.Stores.Add(store);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Tienda creada";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(byte id)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreId == id && !s.IsDeleted);
        if (store == null) return NotFound();
        return View(store);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte id, Store store)
    {
        if (id != store.StoreId) return BadRequest();
        if (!ModelState.IsValid) return View(store);
        store.LastUpdate = DateTime.Now;
        _context.Stores.Update(store);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Tienda actualizada";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(byte id)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(s => s.StoreId == id && !s.IsDeleted);
        if (store == null) return NotFound();
        return View(store);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte id)
    {
        var store = await _context.Stores.FindAsync(id);
        if (store != null)
        {
            store.IsDeleted = true;   // eliminación lógica
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Tienda eliminada correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}