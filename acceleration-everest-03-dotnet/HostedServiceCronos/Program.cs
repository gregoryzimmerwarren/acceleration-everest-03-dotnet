using HostedServiceCronos.DependecyInjection;
using HostedServiceCronos.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddCronJob<CronJobService>(config =>
        {
            config.TimeZoneInfo = TimeZoneInfo.Local;
            config.CronExpression = @"00 09 * * *";
        });
    }).Build();

await host.RunAsync();
