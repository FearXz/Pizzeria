using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class Ingrediente
    {
        [Key]
        public int IdIngrediente { get; set; }

        [Required]
        public string NomeIngrediente { get; set; }

        [Required]
        public double PrezzoIngrediente { get; set; }

        public virtual ICollection<IngredienteAggiunto> IngredientiAggiunti { get; set; }
    }
}
