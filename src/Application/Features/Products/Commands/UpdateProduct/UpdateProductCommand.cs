using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock,
    int CategoryId
) : IRequest<UpdateProductResponse>;

public record UpdateProductResponse(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock,
    int CategoryId,
    string CategoryName,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
