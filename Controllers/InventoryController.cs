using InventoryApi.Models;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService iIventoryService;
    public InventoryController(IInventoryService service) => iIventoryService = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await iIventoryService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var item = await iIventoryService.GetByIdAsync(id);

        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InventoryItem payload)
    {
        payload.Id = Guid.NewGuid();
        payload.BorrowedAt = DateTime.UtcNow;
        await iIventoryService.AddAsync(payload);

        return CreatedAtAction(nameof(Get), new { id = payload.Id }, payload);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] InventoryItem payload)
    {
        var existing = await iIventoryService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        existing.Borrower = payload.Borrower ?? existing.Borrower;
        existing.ReturnedAt = payload.ReturnedAt;
        await iIventoryService.UpdateAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await iIventoryService.DeleteAsync(id);

        return NoContent();
    }
}
