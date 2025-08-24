using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Commands;

public record CreateProductCommand(CreateProductDto Product) : IRequest<ProductDto>;

public record UpdateProductCommand(Guid Id, UpdateProductDto Product) : IRequest<ProductDto>;

public record UpdateProductPriceCommand(Guid Id, UpdateProductPriceDto PriceUpdate) : IRequest<ProductDto>;

public record UpdateProductStockCommand(Guid Id, UpdateProductStockDto StockUpdate) : IRequest<ProductDto>;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

public record ActivateProductCommand(Guid Id) : IRequest<ProductDto>;

public record DeactivateProductCommand(Guid Id) : IRequest<ProductDto>;
