using MediatR;

namespace Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<IEnumerable<GetProductsResponse>>;

public record GetProductsResponse(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    int Stock,
    bool IsActive,
    int CategoryId,
    string CategoryName
);