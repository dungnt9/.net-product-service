using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Commands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        
        if (category == null)
        {
            throw new Exception($"Category with ID {request.Id} not found");
        }

        category.Name = request.Name;
        category.Description = request.Description;

        var updatedCategory = await _categoryRepository.UpdateAsync(category);

        return new UpdateCategoryResponse(
            updatedCategory.Id,
            updatedCategory.Name,
            updatedCategory.Description,
            updatedCategory.IsActive,
            updatedCategory.CreatedAt,
            updatedCategory.UpdatedAt
        );
    }
}
