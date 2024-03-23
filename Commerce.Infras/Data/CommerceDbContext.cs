using Commerce.Infras.Models;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Infras.Data;

public class CommerceDbContext : DbContext
{
    public CommerceDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<Item> Items { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Kellog's Cornflakes",
                Description = "Breakfast Cereal",
                UnitPrice = 10.50m
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Milo",
                Description = "Chocolate Malt Drink",
                UnitPrice = 5.50m
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Nescafe",
                Description = "Instant Coffee",
                UnitPrice = 7.50m
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Lipton Tea",
                Description = "Black Tea",
                UnitPrice = 3.50m
            },
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Coca Cola",
                Description = "Soft Drink",
                UnitPrice = 2.50m
            }
            
        );
    }
}
