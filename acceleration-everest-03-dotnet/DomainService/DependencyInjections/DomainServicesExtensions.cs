using DomainServices.Interfaces;
using DomainServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DomainServices.DependencyInjections;

public static class DomainServicesExtensions
{
    public static IServiceCollection AddDomainServicesDependecyInjections(this IServiceCollection services)
    {
        services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IPortfolioProductServices, PortfolioProductServices>();
        services.AddTransient<IPortifolioService, PortifolioService>();
        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}