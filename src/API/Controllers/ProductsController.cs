using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetProduct;
using Application.Features.Products.Queries.GetProducts;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }
}