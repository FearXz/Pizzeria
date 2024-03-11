using System.ComponentModel.DataAnnotations;
using Pizzeria.Class;

namespace Pizzeria.Models
{
    public class Utente
    {
        [Key]
        public int IdUtente { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = UserRole.USER;
        public virtual ICollection<Ordine> Ordini { get; set; }
    }
}
