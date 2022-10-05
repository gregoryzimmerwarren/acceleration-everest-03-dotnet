using AppServices.Interfaces;
using AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppServices.DependencyInjections;

public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServicesDependecyInjections(this IServiceCollection services)
    {
        //services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
        services.AddTransient<ICustomerAppService, CustomerAppService>();
        //services.AddTransient<IOrderAppService, OrderAppService>();
        //services.AddTransient<IPortfolioProductAppServices, PortfolioProductAppServices>();
        //services.AddTransient<IPortifolioAppService, PortifolioAppService>();
        //services.AddTransient<IProductAppService, ProductAppService>();

        return services;
    }
}