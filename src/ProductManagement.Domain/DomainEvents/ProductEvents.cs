using ProductManagement.Domain.Entities;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Domain.DomainEvents;

public record ProductCreatedEvent(Guid ProductId, string ProductName) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record ProductUpdatedEvent(Guid ProductId, string ProductName) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record ProductPriceChangedEvent(Guid ProductId, Money OldPrice, Money NewPrice) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record ProductStockUpdatedEvent(Guid ProductId, int NewQuantity) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record ProductActivatedEvent(Guid ProductId, string ProductName) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record ProductDeactivatedEvent(Guid ProductId, string ProductName) : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
