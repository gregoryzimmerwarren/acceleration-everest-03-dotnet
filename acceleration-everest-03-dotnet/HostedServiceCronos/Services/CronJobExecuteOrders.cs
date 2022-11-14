using AppServices.Interfaces;
using HostedServiceCronos.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServiceCronos.Services;

public class CronJobExecuteOrders : CronJobService
{
    private readonly ILogger<CronJobExecuteOrders> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CronJobExecuteOrders(IScheduleConfig<CronJobExecuteOrders> config, ILogger<CronJobExecuteOrders> logger, IServiceScopeFactory serviceScopeFactor)
    : base(config.CronExpression, config.TimeZoneInfo)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceScopeFactory = serviceScopeFactor ?? throw new ArgumentNullException(nameof(serviceScopeFactor));
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        try
        {
            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation("CronJobExecuteOrders started.");

            var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IPortfolioAppService>();
            await repository.ExecuteOrdersOfTheDayAsync().ConfigureAwait(false);

            _logger.LogInformation("CronJobExecuteOrders finished processing. Time taken: {elapsedMilliseconds}", stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred in CronJobExecuteOrders.");
        }
    }
}