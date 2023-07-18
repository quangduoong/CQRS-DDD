using AutoMapper;
using eShop.Domain.Products.Requests;
using eShop.Domain.Products.Responses;
using eShop.Domain.Entities;

namespace eShop.Domain.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<PriceCurrency, PriceCurrencyResponse>();
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.PriceCurrency));
    }
}
