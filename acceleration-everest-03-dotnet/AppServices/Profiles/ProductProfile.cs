using AppModels.Products;
using AutoMapper;
using DomainModels.Models;

namespace AppServices.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResult>();
        CreateMap<Product, ProductResultForOthersDtos>();
        CreateMap<CreateProduct, Product>();
        CreateMap<UpdateProduct, Product>();
    }
}