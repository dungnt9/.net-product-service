using MediatR;

namespace Application.Features.Categories.Queries.GetCategory;

public record GetCategoryQuery(int Id) : IRequest<GetCategoryResponse?>;

public record GetCategoryResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    IEnumerable<CategoryProductDto> Products,
    DateTime CreatedAt
);

public record CategoryProductDto(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    int Stock
);
