using eShop.Application.Products.Commands;
using eShop.Domain.Shared;
using FluentValidation;

namespace eShop.Application.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        AvailableCurrencies availableCurrencies = new();
        Guid VndCurrencyId = availableCurrencies.VND.Id;
        Guid UsdCurrencyId = availableCurrencies.USD.Id;

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
                bool isRequestedPriceCurrencyIdValid =
                    availableCurrencies
                        .GetAll()
                        .Select(currency => currency.Id)
                        .Contains(id);

                if (!isRequestedPriceCurrencyIdValid)
                {
                    return false;
                }

                return true;
            })
            .WithMessage("Price Currency Id must be in available currency ids.");

        RuleFor(req => req.Product.PriceCurrencyId)
            .Must((req, priceCurrencyId) =>
            {
                double? amount = req.Product.PriceAmount;

                if (priceCurrencyId == VndCurrencyId)
                    if (amount < minVndPriceAmount || amount > maxVndPriceAmount)
                        return false;

                return true;
            })
            .WithMessage($"Price Amount must be between {minVndPriceAmount} and {maxVndPriceAmount} in VND currency");

        RuleFor(req => req.Product.PriceCurrencyId)
           .Must((req, priceCurrencyId) =>
           {
               double amount = req.Product.PriceAmount;

               if (priceCurrencyId == UsdCurrencyId)
                   if (amount < minUsdPriceAmount || amount > maxUsdPriceAmount)
                       return false;

               return true;
           })
           .WithMessage($"Price Amount must be between {minUsdPriceAmount} and {maxUsdPriceAmount} in USD currency");
    }
}
