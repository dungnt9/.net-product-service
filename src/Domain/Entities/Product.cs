using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation property
    public virtual Category? Category { get; set; }
}