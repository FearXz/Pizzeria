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
        public DateTime DataOrdine { get; set; } = DateTime.Now;

        [Required]
        public bool IsEvaso { get; set; } = false;

        [Required]
        public string Nota { get; set; } = "";

        [NotMapped]
        public double PrezzoTotale { get; set; } = 0;

        public virtual Utente Utente { get; set; }
        public virtual ICollection<ProdottoAcquistato> ProdottiAcquistati { get; set; }
    }
}
