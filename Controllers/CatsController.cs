using InventoryApi.Models;
using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{
    private readonly ICatService iCatService;
    public CatsController(ICatService service) => iCatService = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await iCatService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var cat = await iCatService.GetByIdAsync(id);

        return cat == null ? NotFound() : Ok(cat);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncFromTheCatApi([FromQuery] int limit = 10)
    {
        var cats = await iCatService.RefreshFromExternalApiAsync(limit);

        return Ok(cats);
    }
}
