using MediatR;

namespace Application.Features.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    int Id,
    string Name,
    string Description
) : IRequest<UpdateCategoryResponse>;

public record UpdateCategoryResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
