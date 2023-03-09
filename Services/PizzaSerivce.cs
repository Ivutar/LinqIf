public class PizzaService
{
    public Task<IEnumerable<Pizza>> Get(PizzaRequest req)
    {
        var q = _db/*.AsNoTracking()*/;
        
        if (!string.IsNullOrEmpty(req.Filter.Name))
            q = q.Where(p => p.Name == req.Filter.Name); //можно Like
        if (!string.IsNullOrEmpty(req.Filter.Description))
            q = q.Where(p => p.Description == req.Filter.Description); //можно Like
            //...

        
        return null;
    }

    public Task<IEnumerable<Pizza>> GetEx(PizzaRequest req)
    {
        //...
        return null;
    }

    #region [ dummy data ]

    IQueryable<Pizza> _db { get => _data.AsQueryable(); }

    static Pizza[] _data = new Pizza[]
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