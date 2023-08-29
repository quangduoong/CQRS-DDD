using eShop.Application.Products.Requests;
using eShop.Domain.Shared;
using eShop.Infrastructure.Database;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace eShop.Application.IntegrationTests.Controllers;

public class ProductControllerTests : IClassFixture<ProductServiceApiFactory<Program, AppDbContext>>
{
    private readonly ProductServiceApiFactory<Program, AppDbContext> _factory;

    public ProductControllerTests(ProductServiceApiFactory<Program, AppDbContext> factory)
        => _factory = factory;

    [Fact]
    public async Task Handle_ShouldCreateSuccessfully()
    {
        // Arrange 
        HttpClient httpClient = _factory.CreateClient();
        CreateProductRequest request = new()
        {
            Name = "Test",
            Sku = 10,
            PriceAmount = 1000,
            PriceCurrencyId = new AvailableCurrencies().USD.Id
        };

        // Act
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/product", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}

