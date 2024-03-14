using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Models
{
    public class Prodotto
    {
        [Key]
        public int IdProdotto { get; set; }

        [Required]
        public string NomeProdotto { get; set; }

        [Required]
        public string ImgProdotto { get; set; }

        [Required]
        public double PrezzoProdotto { get; set; }

        [Required]
        public int TempoConsegna { get; set; }

        [NotMapped]
        public string IngredientiAggiuntiHidden { get; set; }

        public virtual ICollection<ProdottoAcquistato> ProdottiAcquistati { get; set; }
        public virtual ICollection<IngredienteAggiunto> IngredientiAggiunti { get; set; }
    }
}
