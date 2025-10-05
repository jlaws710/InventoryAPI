namespace InventoryApi.Models;

public class InventoryItem
{
    public Guid Id { get; set; }
    public Guid CatId { get; set; }
    public Cat Cat { get; set; }
    public string Borrower { get; set; }
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
