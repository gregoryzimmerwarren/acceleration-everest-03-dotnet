using AppServices.DependencyInjections;
using DomainServices.DependencyInjections;
using HostedServiceCronos.Services;
using Infrastructure.Data.DependencyInjections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
