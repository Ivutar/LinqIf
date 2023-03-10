using Microsoft.AspNetCore.Mvc;

namespace LinqIf.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PizzaController : ControllerBase
{
    private readonly ILogger<PizzaController> _logger;

    public PizzaController(ILogger<PizzaController> logger)
        => _logger = logger;

    [HttpPost]
    public async Task<IEnumerable<Pizza>> Get([FromServices] PizzaService pizzaService, PizzaRequest req)
        => await pizzaService.Get(req);

    [HttpPost]
    public async Task<IEnumerable<Pizza>> GetEx([FromServices] PizzaService pizzaService, PizzaRequest req)
        => await pizzaService.GetEx(req);
}
