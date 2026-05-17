using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Models;

namespace SakilaApp.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly SakilaContext _context;

    public CategoriesController(SakilaContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
        return View(categories);
    }

    public async Task<IActionResult> Details(byte id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid) return View(category);
        category.LastUpdate = DateTime.Now;
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Categoría creada";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(byte id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte id, Category category)
    {
        if (id != category.CategoryId) return BadRequest();
        if (!ModelState.IsValid) return View(category);
        category.LastUpdate = DateTime.Now;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Categoría actualizada";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(byte id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            category.IsDeleted = true;   // eliminación lógica
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoría eliminada correctamente";
        }
        return RedirectToAction(nameof(Index));
    }
}