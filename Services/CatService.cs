using InventoryApi.Models;
using InventoryApi.Repositories;
using System.Text.Json;

namespace InventoryApi.Services;
public interface ICatService
{
    Task<IEnumerable<Cat>> GetAllAsync();
    Task<Cat?> GetByIdAsync(Guid id);
    Task<IEnumerable<Cat>> RefreshFromExternalApiAsync(int limit = 10);
}

public class CatService : ICatService
{
    private readonly ICatRepository iCatRepo;
    private readonly IHttpClientFactory iHttp;
    private readonly IConfiguration iConfig;

    public CatService(ICatRepository repo, IHttpClientFactory http, IConfiguration config)
    {
        iCatRepo = repo;
        iHttp = http;
        iConfig = config;
    }

    public async Task<IEnumerable<Cat>> GetAllAsync() => await iCatRepo.GetAllAsync();

    public async Task<Cat?> GetByIdAsync(Guid id) => await iCatRepo.GetByIdAsync(id);

    // Fetch a few cats from TheCatAPI, map and upsert into local DB
    public async Task<IEnumerable<Cat>> RefreshFromExternalApiAsync(int limit = 10)
    {
        var client = iHttp.CreateClient();
        client.BaseAddress = new Uri(iConfig["TheCatApi:BaseUrl"]);
        var resp = await client.GetAsync($"/images/search?limit={limit}");
        resp.EnsureSuccessStatusCode();
        var payload = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(payload);
        var results = new List<Cat>();

        foreach (var el in doc.RootElement.EnumerateArray())
        {
            var extId = el.GetProperty("id").GetString();
            var url = el.GetProperty("url").GetString();
            var cat = new Cat { Id = Guid.NewGuid(), ExternalId = extId ?? Guid.NewGuid().ToString(), ImageUrl = url, Name = null };
            await iCatRepo.AddAsync(cat);
            results.Add(cat);
        }

        return results;
    }
}
