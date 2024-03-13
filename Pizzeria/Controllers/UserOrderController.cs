using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class UserOrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserOrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Ordine
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Carrello") != null)
            {
                List<CartItem> carrello = JsonConvert.DeserializeObject<List<CartItem>>(
                    HttpContext.Session.GetString("Carrello")
                );
                return View(carrello);
            }

            return View();
        }

        // GET: Fetch lista prodotti /UserOrder/FetchListaProdotti
        public async Task<IActionResult> FetchListaProdotti()
        {
            var listaProdotti = await _db
                .Prodotti.Include(i => i.IngredientiAggiunti)
                .Select(p => new
                {
                    p.IdProdotto,
                    p.ImgProdotto,
                    p.NomeProdotto,
                    p.PrezzoProdotto,
                    p.TempoConsegna,

                    IngredientiAggiunti = p.IngredientiAggiunti.Select(i => new
                    {
                        i.IdIngrediente,
                        i.Ingrediente.NomeIngrediente,
                        i.Ingrediente.PrezzoIngrediente,
                    })
                })
                .ToListAsync();
            return Json(listaProdotti);
        }

        public async Task<IActionResult> FetchAddToCartSession(int id)
        {
            var prodotto = await _db
                .Prodotti.Include(i => i.IngredientiAggiunti)
                .Select(p => new
                {
                    p.IdProdotto,
                    p.ImgProdotto,
                    p.NomeProdotto,
                    p.PrezzoProdotto,
                    p.TempoConsegna,

                    IngredientiAggiunti = p.IngredientiAggiunti.Select(i => new
                    {
                        i.IdIngredienteAggiunto,
                        i.IdIngrediente,
                        i.Ingrediente.NomeIngrediente,
                        i.Ingrediente.PrezzoIngrediente,
                    })
                })
                .FirstOrDefaultAsync(p => p.IdProdotto == id);

            if (prodotto == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("Carrello") == null)
            {
                List<CartItem> carrello = new List<CartItem>();
                string json = JsonConvert.SerializeObject(carrello);
                HttpContext.Session.SetString("Carrello", json);
            }

            List<CartItem> cartFromSession = JsonConvert.DeserializeObject<List<CartItem>>(
                HttpContext.Session.GetString("Carrello")
            );
            var existingItem = cartFromSession.FirstOrDefault(i => i.IdProdotto == id);
            if (existingItem != null)
            {
                existingItem.Quantita++;
            }
            else
            {
                CartItem cartItem = new CartItem
                {
                    IdProdotto = prodotto.IdProdotto,
                    ImgProdotto = prodotto.ImgProdotto,
                    NomeProdotto = prodotto.NomeProdotto,
                    PrezzoProdotto = prodotto.PrezzoProdotto,
                    TempoConsegna = prodotto.TempoConsegna,
                    Quantita = 1,
                    IngredienteItem = prodotto
                        .IngredientiAggiunti.Select(i => new IngredienteItem
                        {
                            IdIngrediente = i.IdIngrediente,
                            NomeIngrediente = i.NomeIngrediente,
                            PrezzoIngrediente = i.PrezzoIngrediente,
                        })
                        .ToList()
                };

                cartFromSession.Add(cartItem);
            }
            string jsonCart = JsonConvert.SerializeObject(cartFromSession);
            HttpContext.Session.SetString("Carrello", jsonCart);

            System.Diagnostics.Debug.WriteLine(HttpContext.Session.GetString("Carrello"));

            return Ok();
        }

        public async Task<IActionResult> FetchRemoveFromCartSession(int id)
        {
            if (HttpContext.Session.GetString("Carrello") == null)
            {
                return NotFound();
            }

            List<CartItem> cartFromSession = JsonConvert.DeserializeObject<List<CartItem>>(
                HttpContext.Session.GetString("Carrello")
            );
            var existingItem = cartFromSession.FirstOrDefault(i => i.IdProdotto == id);
            if (existingItem != null)
            {
                if (existingItem.Quantita > 1)
                {
                    existingItem.Quantita--;
                }
                else
                {
                    cartFromSession.Remove(existingItem);
                }
            }
            string jsonCart = JsonConvert.SerializeObject(cartFromSession);
            HttpContext.Session.SetString("Carrello", jsonCart);

            return Ok();
        }
    }
}
