using MediatR;

namespace Application.Features.Categories.Queries.GetCategories;

public record GetCategoriesQuery() : IRequest<IEnumerable<GetCategoriesResponse>>;

public record GetCategoriesResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    int ProductCount,
    DateTime CreatedAt
);
