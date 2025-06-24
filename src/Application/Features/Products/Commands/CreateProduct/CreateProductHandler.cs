using MediatR;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Brand = request.Brand,
            Price = request.Price,
            Description = request.Description,
            Stock = request.Stock
        };

        var createdProduct = await _repository.CreateAsync(product);

        return new CreateProductResponse(
            createdProduct.Id,
            createdProduct.Name,
            createdProduct.Brand,
            createdProduct.Price,
            createdProduct.Description,
            createdProduct.Stock,
            createdProduct.CreatedAt
        );
    }
}