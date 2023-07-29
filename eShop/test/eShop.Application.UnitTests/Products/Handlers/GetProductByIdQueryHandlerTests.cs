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

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenProductIdNotFound()
    {
        // Arrange
        Guid fakeProductId = Guid.NewGuid();
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

