using DomainServices.Interfaces;
using DomainServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DomainServices.DependencyInjections;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServicesConfiguration(this IServiceCollection services)
    {
        services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPortfolioProductService, PortfolioProductService>();
        services.AddTransient<IPortfolioService, PortfolioService>();
        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}