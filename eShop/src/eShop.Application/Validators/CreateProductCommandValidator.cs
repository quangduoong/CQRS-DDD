using eShop.Domain.Products.Commands;
using eShop.Domain.Entities;
using eShop.Domain.Shared;
using FluentValidation;

namespace eShop.Domain.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        List<PriceCurrency> priceCurrencies = new AvailableCurrencies().Values;
        Guid UsdCurrencyId = priceCurrencies.Where(currency => currency.Name == "USD").FirstOrDefault()!.Id;
        Guid VndCurrencyId = priceCurrencies.Where(currency => currency.Name == "VND").FirstOrDefault()!.Id;

        RuleFor(req => req.Product.Name)
            .NotEmpty();
        RuleFor(req => req.Product.Sku)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100);
        RuleFor(req => req.Product.PriceAmount)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5000)
            .When(req => req.Product.PriceCurrencyId == UsdCurrencyId)
            .WithMessage("Amount must be between 1 and 5000 in USD.");
        RuleFor(req => req.Product.PriceAmount)
            .GreaterThanOrEqualTo(1000)
            .LessThanOrEqualTo(50000000)
            .When(req => req.Product.PriceCurrencyId == UsdCurrencyId)
            .WithMessage("Amount must be between 1000 and 50000000 in VND.");
        //RuleFor(req => req.Product.PriceCurrencyId)
        //    .
    }
}
