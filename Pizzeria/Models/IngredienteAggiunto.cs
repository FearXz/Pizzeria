using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Models
{
    public class IngredienteAggiunto
    {
        [Key]
        public int IdIngredienteAggiunto { get; set; }

        [Required]
        [ForeignKey("Ingrediente")]
        public int IdIngrediente { get; set; }

        [Required]
        [ForeignKey("Prodotto")]
        public int IdProdotto { get; set; }

        public virtual Prodotto Prodotto { get; set; }
        public virtual Ingrediente Ingrediente { get; set; }
    }
}
