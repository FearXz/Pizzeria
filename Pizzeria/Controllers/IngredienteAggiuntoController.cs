using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Class;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class IngredienteAggiuntoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredienteAggiuntoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IngredienteAggiunto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context
                .IngredienteAggiunto.Include(i => i.Ingrediente)
                .Include(i => i.Prodotto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IngredienteAggiunto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteAggiunto = await _context
                .IngredienteAggiunto.Include(i => i.Ingrediente)
                .Include(i => i.Prodotto)
                .FirstOrDefaultAsync(m => m.IdIngredienteAggiunto == id);
            if (ingredienteAggiunto == null)
            {
                return NotFound();
            }

            return View(ingredienteAggiunto);
        }

        // GET: IngredienteAggiunto/Create
        public IActionResult Create()
        {
            ViewData["IdIngrediente"] = new SelectList(
                _context.Ingrediente,
                "IdIngrediente",
                "NomeIngrediente"
            );
            ViewData["IdProdotto"] = new SelectList(
                _context.Prodotti,
                "IdProdotto",
                "NomeProdotto"
            );
            return View();
        }

        // POST: IngredienteAggiunto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdIngrediente,IdProdotto")] IngredienteAggiunto ingredienteAggiunto
        )
        {
            ModelState.Remove("Prodotto");
            ModelState.Remove("Ingrediente");

            if (ModelState.IsValid)
            {
                _context.Add(ingredienteAggiunto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIngrediente"] = new SelectList(
                _context.Ingrediente,
                "IdIngrediente",
                "NomeIngrediente",
                ingredienteAggiunto.IdIngrediente
            );
            ViewData["IdProdotto"] = new SelectList(
                _context.Prodotti,
                "IdProdotto",
                "ImgProdotto",
                ingredienteAggiunto.IdProdotto
            );
            return View(ingredienteAggiunto);
        }

        // GET: IngredienteAggiunto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteAggiunto = await _context.IngredienteAggiunto.FindAsync(id);
            if (ingredienteAggiunto == null)
            {
                return NotFound();
            }
            ViewData["IdIngrediente"] = new SelectList(
                _context.Ingrediente,
                "IdIngrediente",
                "NomeIngrediente",
                ingredienteAggiunto.IdIngrediente
            );
            ViewData["IdProdotto"] = new SelectList(
                _context.Prodotti,
                "IdProdotto",
                "ImgProdotto",
                ingredienteAggiunto.IdProdotto
            );
            return View(ingredienteAggiunto);
        }

        // POST: IngredienteAggiunto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdIngredienteAggiunto,IdIngrediente,IdProdotto")]
                IngredienteAggiunto ingredienteAggiunto
        )
        {
            if (id != ingredienteAggiunto.IdIngredienteAggiunto)
            {
                return NotFound();
            }
            ModelState.Remove("Prodotto");
            ModelState.Remove("Ingrediente");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredienteAggiunto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteAggiuntoExists(ingredienteAggiunto.IdIngredienteAggiunto))
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
            ViewData["IdIngrediente"] = new SelectList(
                _context.Ingrediente,
                "IdIngrediente",
                "NomeIngrediente",
                ingredienteAggiunto.IdIngrediente
            );
            ViewData["IdProdotto"] = new SelectList(
                _context.Prodotti,
                "IdProdotto",
                "ImgProdotto",
                ingredienteAggiunto.IdProdotto
            );
            return View(ingredienteAggiunto);
        }

        // GET: IngredienteAggiunto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredienteAggiunto = await _context
                .IngredienteAggiunto.Include(i => i.Ingrediente)
                .Include(i => i.Prodotto)
                .FirstOrDefaultAsync(m => m.IdIngredienteAggiunto == id);
            if (ingredienteAggiunto == null)
            {
                return NotFound();
            }

            return View(ingredienteAggiunto);
        }

        // POST: IngredienteAggiunto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredienteAggiunto = await _context.IngredienteAggiunto.FindAsync(id);
            if (ingredienteAggiunto != null)
            {
                _context.IngredienteAggiunto.Remove(ingredienteAggiunto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteAggiuntoExists(int id)
        {
            return _context.IngredienteAggiunto.Any(e => e.IdIngredienteAggiunto == id);
        }
    }
}
