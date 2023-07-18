using eShop.Domain.Products.Commands;
using eShop.Domain.Products.Queries;
using eShop.Domain.Products.Requests;
using eShop.Domain.Products.Responses;
using eShop.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductResponse>> GetProductById(Guid productId)
    {
        ProductResponse? product = await _sender.Send(new GetProductByIdQuery(productId));

        return Ok(product) ?? throw new ProductNotFoundException(productId);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> AddProduct(CreateProductRequest request)
    {
        ProductResponse addedProduct = await _sender.Send(new CreateProductCommand(request));
        return CreatedAtAction(nameof(GetProductById), new { ProductId = addedProduct.Id }, addedProduct);
    }
}
