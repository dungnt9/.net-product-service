using Domain.Entities;

namespace API.GraphQL.Types;

public class ProductType : ObjectType<Product>
{
    protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
    {
        descriptor.Description("Represents a product in the catalog.");

        descriptor.Field(p => p.Id)
            .Description("The unique identifier of the product.");

        descriptor.Field(p => p.Name)
            .Description("The name of the product.");

        descriptor.Field(p => p.Brand)
            .Description("The brand of the product.");

        descriptor.Field(p => p.Price)
            .Description("The price of the product.");

        descriptor.Field(p => p.Description)
            .Description("The detailed description of the product.");

        descriptor.Field(p => p.Stock)
            .Description("The available stock quantity.");

        descriptor.Field(p => p.IsActive)
            .Description("Indicates if the product is active.");

        descriptor.Field(p => p.CategoryId)
            .Description("The category ID of the product.");

        descriptor.Field(p => p.Category)
            .Description("The category this product belongs to.");

        descriptor.Field(p => p.CreatedAt)
            .Description("The date and time when the product was created.");

        descriptor.Field(p => p.UpdatedAt)
            .Description("The date and time when the product was last updated.");
    }
}
