public class Pizza
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int KCal { get; set; }
    public decimal Price { get; set; }
    public int Diameter { get; set; }
    public IEnumerable<string> Ingridients { get; set; }
}