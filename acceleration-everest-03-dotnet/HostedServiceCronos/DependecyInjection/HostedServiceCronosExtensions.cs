﻿using HostedServiceCronos.Interfaces;
using HostedServiceCronos.Services;

namespace HostedServiceCronos.DependecyInjection;

public static class HostedServiceCronosExtensions
{
    public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options)
        where T : CronJobService
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
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