namespace API.GraphQL.InputTypes;

/// <summary>
/// Input type for creating a new product
/// </summary>
public record CreateProductInput(
    string Name,
    string Brand,
    decimal Price,
    string? Description,
    int Stock,
    int CategoryId
);

/// <summary>
/// Input type for updating an existing product
/// </summary>
public record UpdateProductInput(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string? Description,
    int Stock,
    int CategoryId,
    bool IsActive
);

/// <summary>
/// Input type for creating a new category
/// </summary>
public record CreateCategoryInput(
    string Name,
    string? Description
);

/// <summary>
/// Input type for updating an existing category
/// </summary>
public record UpdateCategoryInput(
    int Id,
    string Name,
    string? Description,
    bool IsActive
);
