using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Products.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductsResponse>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        
        return products.Select(p => new GetProductsResponse(
            p.Id,
            p.Name,
            p.Brand,
            p.Price,
            p.Stock,
            p.IsActive
        ));
    }
}