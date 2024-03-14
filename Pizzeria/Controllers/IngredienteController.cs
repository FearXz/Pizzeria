using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Class;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class IngredienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingrediente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingrediente.ToListAsync());
        }

        // GET: Ingrediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente.FirstOrDefaultAsync(m =>
                m.IdIngrediente == id
            );
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // GET: Ingrediente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingrediente/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NomeIngrediente,PrezzoIngrediente")] Ingrediente ingrediente
        )
        {
            ModelState.Remove("IngredientiAggiunti");

            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        // GET: Ingrediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return View(ingrediente);
        }

        // POST: Ingrediente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdIngrediente,NomeIngrediente,PrezzoIngrediente")] Ingrediente ingrediente
        )
        {
            if (id != ingrediente.IdIngrediente)
            {
                return NotFound();
            }
            ModelState.Remove("IngredientiAggiunti");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.IdIngrediente))
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
            return View(ingrediente);
        }

        // GET: Ingrediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingrediente.FirstOrDefaultAsync(m =>
                m.IdIngrediente == id
            );
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // POST: Ingrediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingrediente.FindAsync(id);
            if (ingrediente != null)
            {
                _context.Ingrediente.Remove(ingrediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingrediente.Any(e => e.IdIngrediente == id);
        }
    }
}
