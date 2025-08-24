using ProductManagement.Domain.ValueObjects;
using ProductManagement.Domain.DomainEvents;

namespace ProductManagement.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = null!;
    public int StockQuantity { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    // Private constructor for EF Core
    private Product() { }

    public Product(
        string name,
        string description,
        Money price,
        int stockQuantity,
        string category)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        SetStockQuantity(stockQuantity);
        SetCategory(category);
        IsActive = true;
        
        AddDomainEvent(new ProductCreatedEvent(Id, Name));
    }

    public void UpdateDetails(string name, string description, string category)
    {
        SetName(name);
        SetDescription(description);
        SetCategory(category);
        MarkAsModified();
        
        AddDomainEvent(new ProductUpdatedEvent(Id, Name));
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Amount <= 0)
            throw new ArgumentException("Price must be greater than zero");
            
        var oldPrice = Price;
        SetPrice(newPrice);
        MarkAsModified();
        
        AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice, newPrice));
    }

    public void UpdateStock(int quantity)
    {
        SetStockQuantity(quantity);
        MarkAsModified();
        
        AddDomainEvent(new ProductStockUpdatedEvent(Id, quantity));
    }

    public void Activate()
    {
        if (IsActive) return;
        
        IsActive = true;
        MarkAsModified();
        
        AddDomainEvent(new ProductActivatedEvent(Id, Name));
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        
        IsActive = false;
        MarkAsModified();
        
        AddDomainEvent(new ProductDeactivatedEvent(Id, Name));
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty");
        if (name.Length > 200)
            throw new ArgumentException("Product name cannot exceed 200 characters");
            
        Name = name.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty");
        if (description.Length > 1000)
            throw new ArgumentException("Product description cannot exceed 1000 characters");
            
        Description = description.Trim();
    }

    private void SetPrice(Money price)
    {
        if (price.Amount <= 0)
            throw new ArgumentException("Product price must be greater than zero");
            
        Price = price;
    }

    private void SetStockQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Stock quantity cannot be negative");
            
        StockQuantity = quantity;
    }

    private void SetCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Product category cannot be empty");
        if (category.Length > 100)
            throw new ArgumentException("Product category cannot exceed 100 characters");
            
        Category = category.Trim();
    }
}
