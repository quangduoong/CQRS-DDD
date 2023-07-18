using AutoMapper;
using eShop.Application.Products.Requests;
using eShop.Application.Products.Responses;
using eShop.Domain.Entities;

namespace eShop.Application.Profiles;

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
