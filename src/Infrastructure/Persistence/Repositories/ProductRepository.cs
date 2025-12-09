using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    // Constructor nhận tham số ApplicationDbContext qua Dependency Injection -> ProductRepository có quyền truy cập vào database.
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive && p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product); // thêm vao database
        await _context.SaveChangesAsync(); // lưu thay doi vao database
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id && p.IsActive);
    }

    public async Task<bool> CheckStockAsync(int productId, int quantity)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);
        return product != null && product.Stock >= quantity;
    }

    public async Task<bool> UpdateStockAsync(int productId, int quantityChange)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);
        if (product == null) return false;
        
        var newStock = product.Stock + quantityChange;
        if (newStock < 0) return false;
        
        product.Stock = newStock;
        product.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}