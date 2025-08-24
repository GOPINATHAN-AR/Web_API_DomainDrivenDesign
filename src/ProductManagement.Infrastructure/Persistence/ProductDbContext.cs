using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Infrastructure.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
            
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.StockQuantity)
                .IsRequired();
            
            entity.Property(e => e.IsActive)
                .IsRequired();
            
            entity.Property(e => e.CreatedAt)
                .IsRequired();
            
            entity.Property(e => e.UpdatedAt);

            // Configure Money value object
            entity.OwnsOne(e => e.Price, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2)
                    .IsRequired();
                    
                price.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // Ignore domain events for persistence
            entity.Ignore(e => e.DomainEvents);
        });

        base.OnModelCreating(modelBuilder);
    }
}
