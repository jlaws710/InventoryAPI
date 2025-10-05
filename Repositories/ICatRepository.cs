using InventoryApi.Models;

namespace InventoryApi.Repositories;
public interface ICatRepository
{
    Task<IEnumerable<Cat>> GetAllAsync();
    Task<Cat?> GetByIdAsync(Guid id);
    Task AddAsync(Cat cat);
    Task UpdateAsync(Cat cat);
    Task DeleteAsync(Guid id);
}
