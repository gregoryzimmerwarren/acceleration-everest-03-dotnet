using AppServices.Interfaces;
using AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.DependencyInjections;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppServicesDependecyInjections(this IServiceCollection services)
    {
        services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
        services.AddTransient<ICustomerAppService, CustomerAppService>();
        services.AddTransient<IOrderAppService, OrderAppService>();
        services.AddTransient<IPortfolioProductAppService, PortfolioProductAppService>();
        services.AddTransient<IPortfolioAppService, PortfolioAppService>();
        services.AddTransient<IProductAppService, ProductAppService>();

        return services;
    }
}