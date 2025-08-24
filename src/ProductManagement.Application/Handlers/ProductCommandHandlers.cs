using MediatR;
using ProductManagement.Application.Commands;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Application.Handlers;

public class ProductCommandHandlers :
    IRequestHandler<CreateProductCommand, ProductDto>,
    IRequestHandler<UpdateProductCommand, ProductDto>,
    IRequestHandler<UpdateProductPriceCommand, ProductDto>,
    IRequestHandler<UpdateProductStockCommand, ProductDto>,
    IRequestHandler<DeleteProductCommand, bool>,
    IRequestHandler<ActivateProductCommand, ProductDto>,
    IRequestHandler<DeactivateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public ProductCommandHandlers(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var money = new Money(request.Product.Price, request.Product.Currency);
        
        var product = new Product(
            request.Product.Name,
            request.Product.Description,
            money,
            request.Product.StockQuantity,
            request.Product.Category
        );

        var createdProduct = await _productRepository.AddAsync(product, cancellationToken);
        
        return MapToDto(createdProduct);
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {request.Id} not found");

        product.UpdateDetails(
            request.Product.Name,
            request.Product.Description,
            request.Product.Category
        );

        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return MapToDto(product);
    }

    public async Task<ProductDto> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {request.Id} not found");

        var newPrice = new Money(request.PriceUpdate.Price, request.PriceUpdate.Currency);
        product.UpdatePrice(newPrice);

        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return MapToDto(product);
    }

    public async Task<ProductDto> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {request.Id} not found");

        product.UpdateStock(request.StockUpdate.StockQuantity);

        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return MapToDto(product);
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _productRepository.ExistsAsync(request.Id, cancellationToken);
        if (!exists)
            return false;

        await _productRepository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }

    public async Task<ProductDto> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {request.Id} not found");

        product.Activate();
        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return MapToDto(product);
    }

    public async Task<ProductDto> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {request.Id} not found");

        product.Deactivate();
        await _productRepository.UpdateAsync(product, cancellationToken);
        
        return MapToDto(product);
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price.Amount,
            Currency = product.Price.Currency,
            StockQuantity = product.StockQuantity,
            Category = product.Category,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }
}
