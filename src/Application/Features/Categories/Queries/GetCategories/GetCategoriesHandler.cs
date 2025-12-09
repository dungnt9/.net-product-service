using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<GetCategoriesResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(c => new GetCategoriesResponse(
            c.Id,
            c.Name,
            c.Description,
            c.IsActive,
            c.Products?.Count(p => p.IsActive) ?? 0,
            c.CreatedAt
        ));
    }
}
