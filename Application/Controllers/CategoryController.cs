using Application.Models.Request;
using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(IMapper mapper, ICategoryService categoryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto?>> Get(int id, CancellationToken cancellationToken)
    {
        try
        {
            var category = await categoryService.Get(id, cancellationToken);

            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto?>>> Get([FromQuery] CategoryFilterDto data,
        CancellationToken cancellationToken)
    {
        var categories = await categoryService.Get(data, cancellationToken);
        return Ok(categories);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto?>> Create(CreateCategoryRequest payload,
        CancellationToken cancellationToken)
    {
        try
        {
            var data = mapper.Map<CreateCategoryDto>(payload);
            var category = await categoryService.Create(data, cancellationToken);
            return CreatedAtRoute(nameof(Create), new { id = category!.Id }, category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryDto?>> Update(int id, UpdateCategoryRequest payload,
        CancellationToken cancellationToken)
    {
        try
        {
            var data = mapper.Map<UpdateCategoryDto>(payload);
            var category = await categoryService.Update(id, data, cancellationToken);

            return Ok(category);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await categoryService.Delete(id, cancellationToken);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}