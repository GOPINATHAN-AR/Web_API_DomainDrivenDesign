using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

public record GetProductsByCategoryQuery(string Category) : IRequest<IEnumerable<ProductDto>>;
