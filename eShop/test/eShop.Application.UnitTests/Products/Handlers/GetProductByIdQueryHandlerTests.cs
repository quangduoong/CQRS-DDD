using AutoMapper;
using eShop.Application.Products.Handlers;
using eShop.Application.Products.Queries;
using eShop.Application.Products.Responses;
using eShop.Domain.Abstractions;
using eShop.Domain.Entities;
using eShop.Domain.Errors;
using eShop.Domain.Shared;
using FluentAssertions;
using Moq;

namespace eShop.Application.UnitTests.Products.Handlers;

public class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public GetProductByIdQueryHandlerTests()
    {
        _productRepositoryMock = new();
        _mapperMock = new();
    }

    [Theory]
    [InlineData("841b6e5d-b9f6-42b9-8fa8-188c96f2814c")]
    public async Task Handle_Should_ReturnFailureResult_WhenProductIdNotFound(Guid fakeProductId)
    {
        GetProductByIdQuery query = new(fakeProductId);

        _productRepositoryMock
            .Setup(
                 repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Product?)null);

        GetProductByIdQueryHandler handler = new(_productRepositoryMock.Object, _mapperMock.Object);

        // Act 
        Result<ProductQueryResponse> result = await handler.Handle(query, default);

        // Assert 
        result.IsSuccess.Should().Be(false);
        result.Error.Should().Be(ProductErrors.NotFound(fakeProductId));
    }
}

