public class PizzaService
{
    public Task<IEnumerable<Pizza>> Get()
    {
        //...
        return null;
    }

    public Task<IEnumerable<Pizza>> GetEx()
    {
        //...
        return null;
    }

    #region [ dummy data ]

    IEnumerable<Pizza> Pizzas { get; } = data;

    static Pizza[] data = new Pizza[]
    {
        new Pizza()
        {
            Name = "",
            Description = "",
            KCal = 10,
            Price = 100,
            Diameter = 34,
            Ingridients = new string[] { "Сыр", "Грибы" }
        },
        //...
    };

    #endregion
}