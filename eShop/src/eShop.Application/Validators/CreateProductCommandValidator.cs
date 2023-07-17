//using eShop.Application.Products.Commands;
//using FluentValidation;

//namespace eShop.Application.Validators;

//public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
//{
//    public CreateProductCommandValidator()
//    {
//        RuleFor(req => req.Product.Name)
//            .NotEmpty();
//        RuleFor(req => req.Product.Sku)
//            .GreaterThanOrEqualTo(1)
//            .LessThanOrEqualTo(100);
//        RuleFor(req=>req.Product.PriceAmount)
//            .GreaterThanOrEqualTo(1)
//            .LessThanOrEqualTo(5000)
//            .When(req => req.Product.PriceCurrency ==);
//    }
//}
