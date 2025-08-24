using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries;

namespace ProductManagement.API.Controllers;

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
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        if (product == null)
            return NotFound($"Product with ID {id} not found");
        
        return Ok(product);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string category)
    {
        var products = await _mediator.Send(new GetProductsByCategoryQuery(category));
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            var product = await _mediator.Send(new CreateProductCommand(createProductDto));
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            var product = await _mediator.Send(new UpdateProductCommand(id, updateProductDto));
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/price")]
    public async Task<ActionResult<ProductDto>> UpdatePrice(Guid id, [FromBody] UpdateProductPriceDto updatePriceDto)
    {
        try
        {
            var product = await _mediator.Send(new UpdateProductPriceCommand(id, updatePriceDto));
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/stock")]
    public async Task<ActionResult<ProductDto>> UpdateStock(Guid id, [FromBody] UpdateProductStockDto updateStockDto)
    {
        try
        {
            var product = await _mediator.Send(new UpdateProductStockCommand(id, updateStockDto));
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<ActionResult<ProductDto>> Activate(Guid id)
    {
        try
        {
            var product = await _mediator.Send(new ActivateProductCommand(id));
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<ActionResult<ProductDto>> Deactivate(Guid id)
    {
        try
        {
            var product = await _mediator.Send(new DeactivateProductCommand(id));
            return Ok(product);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        if (!result)
            return NotFound($"Product with ID {id} not found");
        
        return NoContent();
    }
}
