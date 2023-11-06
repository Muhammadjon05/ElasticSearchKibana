using ElasticSearchKibana.DbContext;
using ElasticSearchKibana.Entities;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticSearchKibana.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IElasticClient _client;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(AppDbContext appDbContext, IElasticClient client, ILogger<ProductsController> logger)
    {
        _appDbContext = appDbContext;
        _client = client;
        _logger = logger;
    }

    [HttpPut]
    public async ValueTask<IActionResult> InsertProduct(ProductDto dto)
    {

        return Ok();
    }
}