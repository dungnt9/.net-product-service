using MediatR;

namespace Application.Features.Products.Queries.GetProduct;

public record GetProductQuery(int Id) : IRequest<GetProductResponse?>;

public record GetProductResponse(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock,
    bool IsActive,
    DateTime CreatedAt
);