using eShop.Domain.Entities;
using eShop.Domain.Products.Commands;
using eShop.Domain.Shared;
using FluentValidation;
using System.Diagnostics;
using System.Numerics;

namespace eShop.Domain.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        string currenciesJsonFilePath = "../../currencies.json";
        List<PriceCurrency> priceCurrencies = new AvailableCurrencies(currenciesJsonFilePath).Values;
        Guid VndCurrencyId = priceCurrencies.Where(currency => currency.Name == "VND").FirstOrDefault()!.Id;
        Guid UsdCurrencyId = priceCurrencies.Where(currency => currency.Name == "USD").FirstOrDefault()!.Id;
        double minVndPriceAmount = 1000;
        double maxVndPriceAmount = 50000000;
        double minUsdPriceAmount = 1;
        double maxUsdPriceAmount = 50000;

        RuleFor(req => req.Product.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(req => req.Product.Sku)
            .InclusiveBetween(1, 100)
            .WithMessage("Sku must be between 1 and 100.");

        RuleFor(req => req.Product.PriceCurrencyId)
            .Must(id =>
            {
                if (!priceCurrencies.Select(currency => currency.Id).ToList().Contains(id)) return false;
                return true;
            })
            .WithMessage("Price Currency Id must be in available currency ids.");

        RuleFor(req => req.Product.PriceCurrencyId)
            .Must((req, priceCurrencyId) =>
            {
                double? amount = req.Product.PriceAmount;

                if (priceCurrencyId == VndCurrencyId)
                    if (amount < minVndPriceAmount || amount > maxVndPriceAmount) return false;
                return true;
            })
            .WithMessage($"Price Amount must be between {minVndPriceAmount} and {maxVndPriceAmount} in VND currency");

        RuleFor(req => req.Product.PriceCurrencyId)
           .Must((req, priceCurrencyId) =>
           {
               double amount = req.Product.PriceAmount;

               if (priceCurrencyId == UsdCurrencyId)
                   if (amount < minUsdPriceAmount || amount > maxUsdPriceAmount) return false;
               return true;
           })
           .WithMessage($"Price Amount must be between {minUsdPriceAmount} and {maxUsdPriceAmount} in USD currency");
    }
}
