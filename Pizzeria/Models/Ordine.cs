using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Models
{
    public class Ordine
    {
        [Key]
        public int IdOrdine { get; set; }

        [Required]
        [ForeignKey("Utente")]
        public int IdUtente { get; set; }

        [Required]
        public string IndirizzoDiConsegna { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; }

        [Required]
        public bool IsEvaso { get; set; } = false;

        [Required]
        public string Nota { get; set; } = "";

        [NotMapped]
        public double PrezzoTotale
        {
            get
            {
                if (ProdottiAcquistati == null)
                {
                    return 0;
                }

                double prezzoTotale = 0;
                foreach (var prodotto in ProdottiAcquistati)
                {
                    prezzoTotale += prodotto.Prodotto.PrezzoProdotto;
                }
                return prezzoTotale;
            }
            set { }
        }

        public virtual Utente Utente { get; set; }
        public virtual ICollection<ProdottoAcquistato> ProdottiAcquistati { get; set; }
    }
}
