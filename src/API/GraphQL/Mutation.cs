using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using API.GraphQL.InputTypes;

namespace API.GraphQL;

public class Mutation
{
    /// <summary>
    /// Create a new product
    /// </summary>
    public async Task<Product> CreateProduct(
        CreateProductInput input,
        [Service] ApplicationDbContext context)
    {
        // Validate category exists
        var category = await context.Categories.FindAsync(input.CategoryId);
        if (category == null)
        {
            throw new GraphQLException($"Category with ID {input.CategoryId} not found.");
        }

        var product = new Product
        {
            Name = input.Name,
            Brand = input.Brand,
            Price = input.Price,
            Description = input.Description ?? string.Empty,
            Stock = input.Stock,
            CategoryId = input.CategoryId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        // Load the category for the response
        await context.Entry(product).Reference(p => p.Category).LoadAsync();

        return product;
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    public async Task<Product> UpdateProduct(
        UpdateProductInput input,
        [Service] ApplicationDbContext context)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == input.Id);

        if (product == null)
        {
            throw new GraphQLException($"Product with ID {input.Id} not found.");
        }

        // Validate category exists
        var category = await context.Categories.FindAsync(input.CategoryId);
        if (category == null)
        {
            throw new GraphQLException($"Category with ID {input.CategoryId} not found.");
        }

        product.Name = input.Name;
        product.Brand = input.Brand;
        product.Price = input.Price;
        product.Description = input.Description ?? string.Empty;
        product.Stock = input.Stock;
        product.CategoryId = input.CategoryId;
        product.IsActive = input.IsActive;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return product;
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    public async Task<bool> DeleteProduct(int id, [Service] ApplicationDbContext context)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    public async Task<Category> CreateCategory(
        CreateCategoryInput input,
        [Service] ApplicationDbContext context)
    {
        var category = new Category
        {
            Name = input.Name,
            Description = input.Description ?? string.Empty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return category;
    }

    /// <summary>
    /// Update an existing category
    /// </summary>
    public async Task<Category> UpdateCategory(
        UpdateCategoryInput input,
        [Service] ApplicationDbContext context)
    {
        var category = await context.Categories.FindAsync(input.Id);
        if (category == null)
        {
            throw new GraphQLException($"Category with ID {input.Id} not found.");
        }

        category.Name = input.Name;
        category.Description = input.Description ?? string.Empty;
        category.IsActive = input.IsActive;
        category.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return category;
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    public async Task<bool> DeleteCategory(int id, [Service] ApplicationDbContext context)
    {
        var category = await context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return false;
        }

        if (category.Products.Any())
        {
            throw new GraphQLException("Cannot delete category with existing products.");
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }
}
