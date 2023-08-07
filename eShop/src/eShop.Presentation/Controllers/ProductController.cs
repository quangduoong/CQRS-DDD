using eShop.Application.Products.Commands;
using eShop.Application.Products.Queries;
using eShop.Application.Products.Requests;
using eShop.Application.Products.Responses;
using eShop.Domain.DomainEvents;
using eShop.Domain.Errors;
using eShop.Domain.Shared;
using eShop.Presentation.ApiResponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public ProductController(ISender sender, IPublisher publisher)
    {
        _sender = sender;
        _publisher = publisher;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(Guid productId)
    {
        Result<ProductQueryResponse> result = await _sender.Send(new GetProductByIdQuery(productId));

        if (result.IsSuccess)
            return Ok(ApiResponse.Ok(result.Value));

        if (result.Error.Equals(ProductErrors.NotFound(productId)))
            return NotFound(ApiResponse.NotFound(result.Error.Message));

        return BadRequest(ApiResponse.BadRequest(result.Error.Message));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(CreateProductRequest request)
    {
        Result<CreateProductResponse> result = await _sender.Send(new CreateProductCommand(request));

        if (!result.IsSuccess)
            return HandleValidationResponse(result);

        await _publisher.Publish(new ProductDomainEvent.Created(result.Value.Id));

        return CreatedAtAction(
            nameof(GetProductById),
            new { ProductId = result.Value?.Id },
            ApiResponse.Created(result.Value));
    }

    private IActionResult HandleValidationResponse(Result result)
    {
        if (result is null) throw new ArgumentNullException(nameof(result));
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(new ValidationApiResponse(validationResult.Errors)),
            _ => BadRequest(result.Error)
        };
    }
}
