using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Queries.GetCategory;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, GetCategoryResponse?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GetCategoryResponse?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            return null;

        return new GetCategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.Products?.Select(p => new CategoryProductDto(
                p.Id,
                p.Name,
                p.Brand,
                p.Price,
                p.Stock
            )) ?? Enumerable.Empty<CategoryProductDto>(),
            category.CreatedAt
        );
    }
}
