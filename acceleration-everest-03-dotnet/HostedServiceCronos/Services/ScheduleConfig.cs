using HostedServiceCronos.Interfaces;
using System;

namespace HostedServiceCronos.Services;

public class ScheduleConfig<T> : IScheduleConfig<T>
{
    public string CronExpression { get; set; }
    public TimeZoneInfo TimeZoneInfo { get; set; }
}