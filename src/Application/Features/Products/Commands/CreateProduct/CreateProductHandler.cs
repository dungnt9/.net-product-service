using MediatR;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductHandler(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate category exists
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId);
        if (!categoryExists)
        {
            throw new Exception($"Category with ID {request.CategoryId} not found");
        }

        var product = new Product
        {
            Name = request.Name,
            Brand = request.Brand,
            Price = request.Price,
            Description = request.Description,
            Stock = request.Stock,
            CategoryId = request.CategoryId
        };

        var createdProduct = await _repository.CreateAsync(product);
        
        // Reload to get category info
        var productWithCategory = await _repository.GetByIdAsync(createdProduct.Id);

        return new CreateProductResponse(
            createdProduct.Id,
            createdProduct.Name,
            createdProduct.Brand,
            createdProduct.Price,
            createdProduct.Description,
            createdProduct.Stock,
            createdProduct.CategoryId,
            productWithCategory?.Category?.Name ?? "",
            createdProduct.CreatedAt
        );
    }
}