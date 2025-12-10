using Domain.Entities;

namespace API.GraphQL.Types;

public class CategoryType : ObjectType<Category>
{
    protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
    {
        descriptor.Description("Represents a product category.");

        descriptor.Field(c => c.Id)
            .Description("The unique identifier of the category.");

        descriptor.Field(c => c.Name)
            .Description("The name of the category.");

        descriptor.Field(c => c.Description)
            .Description("The description of the category.");

        descriptor.Field(c => c.IsActive)
            .Description("Indicates if the category is active.");

        descriptor.Field(c => c.Products)
            .Description("The products in this category.");

        descriptor.Field(c => c.CreatedAt)
            .Description("The date and time when the category was created.");

        descriptor.Field(c => c.UpdatedAt)
            .Description("The date and time when the category was last updated.");
    }
}
