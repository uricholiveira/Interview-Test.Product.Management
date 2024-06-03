using Application.Models.Request;
using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IMapper mapper, IProductService productService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto?>> Get(Guid id, CancellationToken cancellationToken)
    {
        var product = await productService.Get(id, cancellationToken);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto?>>> Get([FromQuery] GetProductRequest payload,
        CancellationToken cancellationToken)
    {
        var data = mapper.Map<ProductFilterDto>(payload);
        var products = await productService.Get(data, cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto?>> Create(CreateProductRequest payload,
        CancellationToken cancellationToken)
    {
        var data = mapper.Map<CreateProductDto>(payload);
        var product = await productService.Create(data, cancellationToken);

        return CreatedAtRoute(nameof(Create), new { id = product!.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto?>> Update(Guid id, UpdateProductRequest payload,
        CancellationToken cancellationToken)
    {
        var data = mapper.Map<UpdateProductDto>(payload);
        var product = await productService.Update(id, data, cancellationToken);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProductDto?>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var hasDeleted = await productService.Delete(id, cancellationToken);
        if (!hasDeleted) return NotFound();

        return Ok();
    }
}