using MediatR; //mô hình Mediator giao tiếp giữa các phần khác nhau trong ứng dụng mà không cần liên kết trực tiếp
using Microsoft.AspNetCore.Mvc; //thư viện cung cấp các lớp để tạo API như controller...
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Queries.GetProduct;
using Application.Features.Products.Queries.GetProducts;

namespace API.Controllers;
//Đánh dấu class này là một API controller để ASP.NET Core xử lý các request API
[ApiController]
//định tuyến
[Route("api/[controller]")]   //[controller] sẽ tự động thay thế bằng tên của controller
public class ProductsController : ControllerBase //controllers chỉ xử lý API trả về JSON hoặc status code HTTP (không trả về giao diện)
{
    private readonly IMediator _mediator;  //Dependency injection của thư viện MediatR. cho controller gửi Commands hoặc Queries để xử lý logic.

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    //Định nghĩa endpoint
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProductsResponse>>> GetProducts()
    {
        var query = new GetProductsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductResponse>> GetProduct(int id)
    {
        var query = new GetProductQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductResponse>> CreateProduct(CreateProductCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProduct(int id, UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send(command);
        
        if (!result)
            return NotFound();
            
        return NoContent();
    }
}