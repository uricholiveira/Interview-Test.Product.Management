using Business.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto?>> Get(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.Get(id, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto?>>> Get(CancellationToken cancellationToken)
    {
        var products = await _productService.Get(cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto?>> Create(ProductDto data, CancellationToken cancellationToken)
    {
        var product = await _productService.Create(data, cancellationToken);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto?>> Update(Guid id, ProductDto data, CancellationToken cancellationToken)
    {
        var product = await _productService.Update(id, data, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDto?>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.Delete(id, cancellationToken);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
}