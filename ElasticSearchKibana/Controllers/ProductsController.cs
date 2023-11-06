using AutoMapper;
using ElasticSearchKibana.DbContext;
using ElasticSearchKibana.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace ElasticSearchKibana.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly IElasticClient _client;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(AppDbContext appDbContext, IElasticClient client, ILogger<ProductsController> logger, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _client = client;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    public async ValueTask<IActionResult> InsertProduct(ProductDto dto)
    {
        var model = _mapper.Map<Product>(dto);
        await _appDbContext.Products.AddAsync(model);
        await _client.IndexDocumentAsync(model);
        await _appDbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("GetWithElastic/{keyword}")]
    public async ValueTask<IActionResult> GetProducts(string keyword)
    { var result = await _client.SearchAsync<ProductDto>(s
            => s.Query(q => q
                .QueryString(d =>
                    d.Query('*'+keyword+'*'))).Size(1000));
        return Ok(result.Documents.ToList());
    }
    [HttpGet("GetWithDb/{keyword}")]
    public async ValueTask<IActionResult> GetWithDb(string keyword)
    {
        var result = await _appDbContext.Products.Where(i => i.Name.Contains(keyword) || 
                                                             i.Description.Contains(keyword) ||
                                                             i.Quantity.ToString().Contains(keyword) || 
                                                             i.Price.ToString().Contains(keyword)).
            FirstOrDefaultAsync();
        return Ok(result);
    }
}