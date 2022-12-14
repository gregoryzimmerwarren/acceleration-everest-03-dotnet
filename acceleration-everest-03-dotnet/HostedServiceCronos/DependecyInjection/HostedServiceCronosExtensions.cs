using HostedServiceCronos.Interfaces;
using HostedServiceCronos.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class HostedServiceCronosExtensions
{
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options)
        where T : CronJobService
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var config = new ScheduleConfig<T>();
        options.Invoke(config);

        if (string.IsNullOrWhiteSpace(config.CronExpression))
        {
            throw new ArgumentNullException(nameof(config.CronExpression));
        }

        services.AddSingleton<IScheduleConfig<T>>(config);
        services.AddHostedService<T>();

        return services;
    }
}