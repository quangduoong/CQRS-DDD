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
            .ForMember(destination => destination.Id, options => options.MapFrom(src => Guid.NewGuid()));
        CreateMap<Product, ProductResponse>();
    }
}
