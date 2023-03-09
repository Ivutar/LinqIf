public class PizzaFilter
{
    public string Name { get; set; } //Like
    public string Description { get; set; } //Like
    public int? KCalMin { get; set; }
    public int? KCalMax { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public int? DiameterMin { get; set; }
    public int? DiameterMax { get; set; }
    public IEnumerable<string> Ingridients { get; set; } //has
}