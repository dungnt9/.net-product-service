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
            Found = true,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name ?? ""
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
                    Found = true,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category?.Name ?? ""
                });
            }
            else
            {
                response.Products.Add(new ProductResponse { Id = id, Found = false });
            }
        }

        return response;
    }

    public override async Task<CheckStockResponse> CheckStock(CheckStockRequest request, ServerCallContext context)
    {
        _logger.LogInformation("gRPC CheckStock called for ProductId: {ProductId}, Quantity: {Quantity}", 
            request.ProductId, request.Quantity);

        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product == null)
        {
            return new CheckStockResponse
            {
                IsAvailable = false,
                CurrentStock = 0,
                Message = $"Product with ID {request.ProductId} not found"
            };
        }

        var isAvailable = product.Stock >= request.Quantity;

        return new CheckStockResponse
        {
            IsAvailable = isAvailable,
            CurrentStock = product.Stock,
            Message = isAvailable 
                ? $"Stock available: {product.Stock}" 
                : $"Insufficient stock. Available: {product.Stock}, Requested: {request.Quantity}"
        };
    }

    public override async Task<UpdateStockResponse> UpdateStock(UpdateStockRequest request, ServerCallContext context)
    {
        _logger.LogInformation("gRPC UpdateStock called for ProductId: {ProductId}, QuantityChange: {QuantityChange}", 
            request.ProductId, request.QuantityChange);

        var product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product == null)
        {
            return new UpdateStockResponse
            {
                Success = false,
                NewStock = 0,
                Message = $"Product with ID {request.ProductId} not found"
            };
        }

        var newStock = product.Stock + request.QuantityChange;
        
        if (newStock < 0)
        {
            return new UpdateStockResponse
            {
                Success = false,
                NewStock = product.Stock,
                Message = $"Cannot reduce stock below 0. Current: {product.Stock}, Change: {request.QuantityChange}"
            };
        }

        var success = await _productRepository.UpdateStockAsync(request.ProductId, request.QuantityChange);

        return new UpdateStockResponse
        {
            Success = success,
            NewStock = newStock,
            Message = success 
                ? $"Stock updated successfully. New stock: {newStock}" 
                : "Failed to update stock"
        };
    }
}
