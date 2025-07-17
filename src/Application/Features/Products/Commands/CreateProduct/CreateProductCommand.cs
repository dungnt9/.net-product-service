using MediatR;

namespace Application.Features.Products.Commands.CreateProduct;
// record kiểu dữ liệu bất biến (immutable) định nghĩa một command để gửi yêu cầu tạo
public record CreateProductCommand(
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock
) : IRequest<CreateProductResponse>; // kết quả xử lý của command này sẽ trả về một CreateProductResponse

public record CreateProductResponse(
    int Id,
    string Name,
    string Brand,
    decimal Price,
    string Description,
    int Stock,
    DateTime CreatedAt
);