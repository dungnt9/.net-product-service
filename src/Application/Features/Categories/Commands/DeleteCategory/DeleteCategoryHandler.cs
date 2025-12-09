using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.ExistsAsync(request.Id);
        
        if (!exists)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(request.Id);
        return true;
    }
}
