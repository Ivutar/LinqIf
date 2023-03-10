public class PizzaService
{
    public async Task<IEnumerable<Pizza>> Get(PizzaRequest req)
    {
        var q = _db/*.AsNoTracking()*/;
        
        if (!string.IsNullOrEmpty(req.Filter.Name))
            q = q.Where(p => p.Name.Contains(req.Filter.Name)); //можно SqlMethods.Like или DbFunctions.Like и т.п.

        if (!string.IsNullOrEmpty(req.Filter.Description))
            q = q.Where(p => p.Description.Contains(req.Filter.Description)); //можно SqlMethods.Like или DbFunctions.Like и т.п.

        if (req.Filter.KCalMin.HasValue)
            q = q.Where(p => p.KCal >= req.Filter.KCalMin.Value);

        if (req.Filter.KCalMax.HasValue)
            q = q.Where(p => p.KCal <= req.Filter.KCalMax.Value);

        if (req.Filter.PriceMin.HasValue)
            q = q.Where(p => p.Price >= req.Filter.PriceMin.Value);

        if (req.Filter.PriceMax.HasValue)
            q = q.Where(p => p.Price <= req.Filter.PriceMax.Value);

        if (req.Filter.DiameterMin.HasValue)
            q = q.Where(p => p.Diameter >= req.Filter.DiameterMin.Value);

        if (req.Filter.DiameterMax.HasValue)
            q = q.Where(p => p.Diameter <= req.Filter.DiameterMax.Value);

        if (req.Order.OrderBy == "Name")
            if (req.Order.Descended)
                q = q.OrderByDescending(p => p.Name);
            else
                q = q.OrderBy(p => p.Name);

        if (req.Order.OrderBy == "Description")
            if (req.Order.Descended)
                q = q.OrderByDescending(p => p.Description);
            else
                q = q.OrderBy(p => p.Description);

        if (req.Order.OrderBy == "KCal")
            if (req.Order.Descended)
                q = q.OrderByDescending(p => p.KCal);
            else
                q = q.OrderBy(p => p.KCal);

        if (req.Order.OrderBy == "Price")
            if (req.Order.Descended)
                q = q.OrderByDescending(p => p.Price);
            else
                q = q.OrderBy(p => p.Price);

        if (req.Order.OrderBy == "Diameter")
            if (req.Order.Descended)
                q = q.OrderByDescending(p => p.Diameter);
            else
                q = q.OrderBy(p => p.Diameter);

        if (req.Offset.HasValue)
            q = q.Skip(req.Offset.Value);

        if (req.Limit.HasValue)
            q = q.Take(req.Limit.Value);

        return q.AsEnumerable<Pizza>();
    }

    public async Task<IEnumerable<Pizza>> GetEx(PizzaRequest req)
    {
        var q = _db
            /*.AsNoTracking()*/
            .WhereIf(!string.IsNullOrEmpty(req.Filter.Name), p => p.Name.Contains(req.Filter.Name)) //можно SqlMethods.Like или DbFunctions.Like и т.п.
            .WhereIf(!string.IsNullOrEmpty(req.Filter.Description), p => p.Name.Contains(req.Filter.Description)) //можно SqlMethods.Like или DbFunctions.Like и т.п.
            .WhereIf(req.Filter.KCalMin.HasValue, p => p.KCal >= req.Filter.KCalMin.Value)
            .WhereIf(req.Filter.KCalMax.HasValue, p => p.KCal <= req.Filter.KCalMax.Value)
            .WhereIf(req.Filter.PriceMin.HasValue, p => p.Price >= req.Filter.PriceMin.Value)
            .WhereIf(req.Filter.PriceMax.HasValue, p => p.Price <= req.Filter.PriceMax.Value)
            .WhereIf(req.Filter.DiameterMin.HasValue, p => p.Diameter >= req.Filter.DiameterMin.Value)
            .WhereIf(req.Filter.DiameterMax.HasValue, p => p.Diameter <= req.Filter.DiameterMax.Value)
            .OrderByIf(req.Order.OrderBy == "Name", p => p.Name, req.Order.Descended)
            .OrderByIf(req.Order.OrderBy == "Description", p => p.Description, req.Order.Descended)
            .OrderByIf(req.Order.OrderBy == "KCal", p => p.KCal, req.Order.Descended)
            .OrderByIf(req.Order.OrderBy == "Price", p => p.Price, req.Order.Descended)
            .OrderByIf(req.Order.OrderBy == "Diameter", p => p.Diameter, req.Order.Descended)
            .OrderByIf(req.Order.OrderBy == "Name", p => p.Name, req.Order.Descended)
            .SkipIf(req.Offset.HasValue, req.Offset.Value)
            .TakeIf(req.Limit.HasValue, req.Limit.Value)
            ;

        return q.AsEnumerable<Pizza>();
    }

    #region [ dummy data ]

    //притворяемся, что берем данные из БД

    IQueryable<Pizza> _db { get => _data.AsQueryable(); }

    static Pizza[] _data = new Pizza[]
    {
        new Pizza()
        {
            Name = "Маргарита",
            Description = "Пицца маргарита",
            KCal = 10,
            Price = 100,
            Diameter = 34,
        },
        
        new Pizza()
        {
            Name = "Моцарелла",
            Description = "Пицца Моцарелла",
            KCal = 10,
            Price = 110,
            Diameter = 34,
        },

        new Pizza()
        {
            Name = "Сырная",
            Description = "Пицца Сырная",
            KCal = 15,
            Price = 90,
            Diameter = 34,
        },
        
        new Pizza()
        {
            Name = "Мясная",
            Description = "Пицца Мясная",
            KCal = 20,
            Price = 150,
            Diameter = 34,
        },

        new Pizza()
        {
            Name = "Вегетарианская",
            Description = "Пицца Вегетарианская",
            KCal = 10,
            Price = 115,
            Diameter = 34,
        },

        new Pizza()
        {
            Name = "Маргарита мини",
            Description = "Пицца Маргарита мини",
            KCal = 2,
            Price = 50,
            Diameter = 20,
        },
        
        new Pizza()
        {
            Name = "Моцарелла мини",
            Description = "Пицца Моцарелла мини",
            KCal = 5,
            Price = 55,
            Diameter = 20,
        },

        new Pizza()
        {
            Name = "Сырная мини",
            Description = "Пицца Сырная мини",
            KCal = 7,
            Price = 45,
            Diameter = 20,
        },
        
        new Pizza()
        {
            Name = "Мясная мини",
            Description = "Пицца Мясная мини",
            KCal = 10,
            Price = 75,
            Diameter = 20,
        },

        new Pizza()
        {
            Name = "Вегетарианская мини",
            Description = "Пицца Вегетарианская мини",
            KCal = 5,
            Price = 57,
            Diameter = 20,
        },
    };

    #endregion
}