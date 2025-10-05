using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace InventoryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<Cat> Cats => Set<Cat>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cat>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired(false);
        });

        modelBuilder.Entity<InventoryItem>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Cat).WithMany().HasForeignKey(x => x.CatId).OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.Borrower).IsRequired();
        });
    }
}

