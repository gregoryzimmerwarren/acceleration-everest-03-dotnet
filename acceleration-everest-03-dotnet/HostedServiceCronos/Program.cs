using AppServices.DependencyInjections;
using DomainServices.DependencyInjections;
using HostedServiceCronos.DependecyInjection;
using HostedServiceCronos.Services;
using Infrastructure.Data.DependencyInjections;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddCronJob<CronJobExecuteOrders>(config =>
        {
            config.TimeZoneInfo = TimeZoneInfo.Local;
            config.CronExpression = "30 09 * * *";
        });

        services.AddAppServicesConfiguration();
        services.AddDomainServicesConfiguration();
        services.AddInfrastructureDataConfiguration(hostContext.Configuration);
        services.AddAutoMapper(Assembly.Load(nameof(AppServices)));
    })
    .Build();

await host.RunAsync();
