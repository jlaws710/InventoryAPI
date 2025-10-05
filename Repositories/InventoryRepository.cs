using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories;
public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext dbContext;
    public InventoryRepository(AppDbContext db) => dbContext = db;

    public async Task AddAsync(InventoryItem item)
    {
        await dbContext.InventoryItems.AddAsync(item);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var catID = await dbContext.InventoryItems.FindAsync(id);
        if (catID == null) return;
        dbContext.InventoryItems.Remove(catID);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        => await dbContext.InventoryItems.Include(i => i.Cat).ToListAsync();

    public async Task<InventoryItem?> GetByIdAsync(Guid id)
        => await dbContext.InventoryItems.Include(i => i.Cat).FirstOrDefaultAsync(i => i.Id == id);

    public async Task UpdateAsync(InventoryItem item)
    {
        dbContext.InventoryItems.Update(item);
        await dbContext.SaveChangesAsync();
    }
}
