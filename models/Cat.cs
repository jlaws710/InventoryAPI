namespace InventoryApi.Models;

public class Cat
{
    public Guid Id { get; set; }            // local GUID
    public string ExternalId { get; set; }  // id from TheCatAPI (if available)
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
