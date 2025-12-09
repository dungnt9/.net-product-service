using MediatR;
using Application.Common.Interfaces;

namespace Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _productRepository.ExistsAsync(request.Id);
        
        if (!exists)
        {
            return false;
        }

        await _productRepository.DeleteAsync(request.Id);
        return true;
    }
}
