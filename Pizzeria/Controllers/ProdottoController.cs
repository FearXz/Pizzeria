using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Class;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize(Roles = UserRole.ADMIN)]
    public class ProdottoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdottoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prodotto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prodotti.ToListAsync());
        }

        // GET: Prodotto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context
                .Prodotti.Include(p => p.IngredientiAggiunti)
                .ThenInclude(i => i.Ingrediente)
                .FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // GET: Prodotto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prodotto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NomeProdotto,ImgProdotto,PrezzoProdotto,TempoConsegna")] Prodotto prodotto
        )
        {
            ModelState.Remove("Prodotto");
            ModelState.Remove("Ingrediente");

            if (ModelState.IsValid)
            {
                _context.Add(prodotto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodotto);
        }

        // GET: Prodotto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        // POST: Prodotto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdProdotto,NomeProdotto,ImgProdotto,PrezzoProdotto,TempoConsegna")]
                Prodotto prodotto
        )
        {
            if (id != prodotto.IdProdotto)
            {
                return NotFound();
            }

            ModelState.Remove("Prodotto");
            ModelState.Remove("Ingrediente");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodotto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdottoExists(prodotto.IdProdotto))
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
            return View(prodotto);
        }

        // GET: Prodotto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodotto = await _context.Prodotti.FirstOrDefaultAsync(m => m.IdProdotto == id);
            if (prodotto == null)
            {
                return NotFound();
            }

            return View(prodotto);
        }

        // POST: Prodotto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodotto = await _context.Prodotti.FindAsync(id);
            if (prodotto != null)
            {
                _context.Prodotti.Remove(prodotto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdottoExists(int id)
        {
            return _context.Prodotti.Any(e => e.IdProdotto == id);
        }
    }
}
