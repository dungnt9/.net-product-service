using MediatR;

namespace Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Description
) : IRequest<CreateCategoryResponse>;

public record CreateCategoryResponse(
    int Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedAt
);
