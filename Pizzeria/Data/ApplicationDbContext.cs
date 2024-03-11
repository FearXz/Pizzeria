using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;

namespace Pizzeria.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Utente> Utenti { get; set; }
        public virtual DbSet<Prodotto> Prodotti { get; set; }
        public virtual DbSet<ProdottoAcquistato> ProdottiAcquistati { get; set; }
        public virtual DbSet<Ordine> Ordini { get; set; }

    }

}
