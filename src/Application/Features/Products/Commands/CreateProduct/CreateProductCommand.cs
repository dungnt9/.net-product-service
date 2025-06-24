using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock
) : IRequest<CreateProductResponse>;

public record CreateProductResponse(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock,
    DateTime CreatedAt
);