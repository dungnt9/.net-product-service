using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> CheckStockAsync(int productId, int quantity);
    Task<bool> UpdateStockAsync(int productId, int quantityChange);
}