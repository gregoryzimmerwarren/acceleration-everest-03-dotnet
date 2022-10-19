using AppServices.DependencyInjections;
using AutoMapper;
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
            config.CronExpression = "48 16 * * *";
        });

        services.AddAppServicesDependecyInjections();
        services.AddDomainServicesDependecyInjections();
        services.AddInfrastructureDataDependecyInjections(hostContext.Configuration);
        services.AddAutoMapper(Assembly.Load(nameof(AppServices)));

    })
    .Build();

await host.RunAsync();
