using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Products.Queries.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse?>
{
    private readonly IProductRepository _repository;

    public GetProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetProductResponse?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        
        if (product == null)
            return null;

        return new GetProductResponse(
            product.Id,
            product.Name,
            product.Brand,
            product.Price,
            product.Description,
            product.Stock,
            product.IsActive,
            product.CreatedAt
        );
    }
}