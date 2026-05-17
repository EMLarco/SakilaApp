using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Models;

namespace SakilaApp.Controllers;

[Authorize]
public class InventoriesController : Controller
{
    private readonly SakilaContext _context;

    public InventoriesController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var inventories = await _context.Inventories.Where(i => !i.IsDeleted).ToListAsync();
        return View(inventories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == id && !i.IsDeleted);
        if (inventory == null) return NotFound();
        return View(inventory);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Inventory inventory)
    {
        if (!ModelState.IsValid) return View(inventory);
        inventory.LastUpdate = DateTime.Now;
        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Inventario creado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == id && !i.IsDeleted);
        if (inventory == null) return NotFound();
        return View(inventory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Inventory inventory)
    {
        if (id != inventory.InventoryId) return BadRequest();
        if (!ModelState.IsValid) return View(inventory);
        inventory.LastUpdate = DateTime.Now;
        _context.Inventories.Update(inventory);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Inventario actualizado";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.InventoryId == id && !i.IsDeleted);
        if (inventory == null) return NotFound();
        return View(inventory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory != null)
        {
            inventory.IsDeleted = true;   // eliminación lógica
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Inventario eliminado correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}