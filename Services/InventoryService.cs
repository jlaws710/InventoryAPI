using InventoryApi.Models;
using InventoryApi.Repositories;

namespace InventoryApi.Services;
public interface IInventoryService
{
    Task<IEnumerable<InventoryItem>> GetAllAsync();
    Task<InventoryItem?> GetByIdAsync(Guid id);
    Task AddAsync(InventoryItem item);
    Task UpdateAsync(InventoryItem item);
    Task DeleteAsync(Guid id);
}

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository iInventoryRepo;
    public InventoryService(IInventoryRepository repo) => iInventoryRepo = repo;
    public Task AddAsync(InventoryItem item) => iInventoryRepo.AddAsync(item);
    public Task DeleteAsync(Guid id) => iInventoryRepo.DeleteAsync(id);
    public Task<IEnumerable<InventoryItem>> GetAllAsync() => iInventoryRepo.GetAllAsync();
    public Task<InventoryItem?> GetByIdAsync(Guid id) => iInventoryRepo.GetByIdAsync(id);
    public Task UpdateAsync(InventoryItem item) => iInventoryRepo.UpdateAsync(item);
}
