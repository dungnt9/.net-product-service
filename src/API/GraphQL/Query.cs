using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.GraphQL;

public class Query
{
    /// <summary>
    /// Get all products with optional filtering
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts([Service] ApplicationDbContext context)
    {
        return context.Products.Include(p => p.Category);
    }

    /// <summary>
    /// Get a single product by ID
    /// </summary>
    public async Task<Product?> GetProduct(int id, [Service] ApplicationDbContext context)
    {
        return await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Get all categories with optional filtering
    /// </summary>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Category> GetCategories([Service] ApplicationDbContext context)
    {
        return context.Categories.Include(c => c.Products);
    }

    /// <summary>
    /// Get a single category by ID
    /// </summary>
    public async Task<Category?> GetCategory(int id, [Service] ApplicationDbContext context)
    {
        return await context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
