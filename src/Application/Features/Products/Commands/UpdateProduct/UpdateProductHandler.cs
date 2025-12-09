using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        
        if (product == null)
        {
            throw new Exception($"Product with ID {request.Id} not found");
        }

        // Validate category exists
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId);
        if (!categoryExists)
        {
            throw new Exception($"Category with ID {request.CategoryId} not found");
        }

        product.Name = request.Name;
        product.Brand = request.Brand;
        product.Price = request.Price;
        product.Description = request.Description;
        product.Stock = request.Stock;
        product.CategoryId = request.CategoryId;

        var updatedProduct = await _productRepository.UpdateAsync(product);
        
        // Reload to get category info
        var productWithCategory = await _productRepository.GetByIdAsync(updatedProduct.Id);

        return new UpdateProductResponse(
            updatedProduct.Id,
            updatedProduct.Name,
            updatedProduct.Brand,
            updatedProduct.Price,
            updatedProduct.Description,
            updatedProduct.Stock,
            updatedProduct.CategoryId,
            productWithCategory?.Category?.Name ?? "",
            updatedProduct.IsActive,
            updatedProduct.CreatedAt,
            updatedProduct.UpdatedAt
        );
    }
}
