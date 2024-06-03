using Application.Models.Request;
using AutoMapper;
using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[controller]/Transaction")]
public class InventoryController(IMapper mapper, IInventoryService inventoryService) : ControllerBase
{
    [HttpPost("Incoming")]
    public async Task<IActionResult> AddIncoming(Guid id, UpdateInventoryRequest data,
        CancellationToken cancellationToken)
    {
        try
        {
            var updateInventoryDto = mapper.Map<UpdateInventoryDto>(data);
            var inventory = await inventoryService.AddIncoming(id, updateInventoryDto, cancellationToken);

            return Ok(inventory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Outgoing")]
    public async Task<IActionResult> AddOutgoing(Guid id, UpdateInventoryRequest data,
        CancellationToken cancellationToken)
    {
        try
        {
            var updateInventoryDto = mapper.Map<UpdateInventoryDto>(data);
            var inventory = await inventoryService.AddOutgoing(id, updateInventoryDto, cancellationToken);

            return Ok(inventory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Adjustment")]
    public async Task<IActionResult> AddAdjustment(Guid id, UpdateInventoryRequest data,
        CancellationToken cancellationToken)
    {
        try
        {
            var updateInventoryDto = mapper.Map<UpdateInventoryDto>(data);
            var inventory = await inventoryService.AddAdjustment(id, updateInventoryDto, cancellationToken);

            return Ok(inventory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}