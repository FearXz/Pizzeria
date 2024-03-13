namespace Pizzeria.Models
{
    public class CartItem
    {
        public int IdProdotto { get; set; }
        public string ImgProdotto { get; set; }
        public string NomeProdotto { get; set; }
        public double PrezzoProdotto { get; set; }
        public int TempoConsegna { get; set; }
        public int Quantita { get; set; }
        public List<IngredienteItem> IngredienteItem { get; set; }
    }

    public class IngredienteItem
    {
        public int IdIngrediente { get; set; }
        public string NomeIngrediente { get; set; }
        public double PrezzoIngrediente { get; set; }
    }
}
