using AutoMapper;
using eShop.Infrastructure.Products.Requests;
using eShop.Infrastructure.Products.Responses;
using eShop.Domain.Entities;

namespace eShop.Infrastructure.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>()
            .ForMember(destination => destination.Id, options => options.MapFrom(src => Guid.NewGuid()));
        CreateMap<Product, ProductResponse>();
    }
}
