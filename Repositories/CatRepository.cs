using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories;
public class CatRepository : ICatRepository
{
    private readonly AppDbContext dbContext;
    public CatRepository(AppDbContext db) => dbContext = db;

    public async Task AddAsync(Cat cat)
    {
        await dbContext.Cats.AddAsync(cat);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var catID = await dbContext.Cats.FindAsync(id);

        if (catID == null) return;
        dbContext.Cats.Remove(catID);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cat>> GetAllAsync() => await dbContext.Cats.ToListAsync();

    public async Task<Cat?> GetByIdAsync(Guid id) => await dbContext.Cats.FindAsync(id);

    public async Task UpdateAsync(Cat cat)
    {
        dbContext.Cats.Update(cat);
        await dbContext.SaveChangesAsync();
    }
}
