using MediatR;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        var createdCategory = await _categoryRepository.CreateAsync(category);

        return new CreateCategoryResponse(
            createdCategory.Id,
            createdCategory.Name,
            createdCategory.Description,
            createdCategory.IsActive,
            createdCategory.CreatedAt
        );
    }
}
