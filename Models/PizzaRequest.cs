public class PizzaRequest
{
    public PizzaFilter Filter { get; set; }
    public Order Order { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}