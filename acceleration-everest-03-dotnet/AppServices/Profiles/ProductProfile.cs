using AppModels.Products;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResultDto>();
        CreateMap<Product, ProductResultForOthersDtos>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}