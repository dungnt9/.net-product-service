using Grpc.Core;
using Application.Common.Interfaces;
using ProductService.Grpc;

namespace API.Services;

public class ProductGrpcService : ProductGrpc.ProductGrpcBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductGrpcService> _logger;

    public ProductGrpcService(IProductRepository productRepository, ILogger<ProductGrpcService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public override async Task<ProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        _logger.LogInformation("gRPC GetProduct called for ProductId: {ProductId}", request.Id);

        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            _logger.LogWarning("Product {ProductId} not found", request.Id);
            return new ProductResponse { Found = false };
        }

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price.ToString("F2"),
            Description = product.Description,
            Stock = product.Stock,
            IsActive = product.IsActive,
            CreatedAt = product.CreatedAt.ToString("O"),
            Found = true
        };
    }

    public override async Task<GetProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
    {
        _logger.LogInformation("gRPC GetProducts called for {Count} product IDs", request.Ids.Count);

        var response = new GetProductsResponse();

        foreach (var id in request.Ids)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null)
            {
                response.Products.Add(new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Price = product.Price.ToString("F2"),
                    Description = product.Description,
                    Stock = product.Stock,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt.ToString("O"),
                    Found = true
                });
            }
            else
            {
                response.Products.Add(new ProductResponse { Id = id, Found = false });
            }
        }

        return response;
    }
}
