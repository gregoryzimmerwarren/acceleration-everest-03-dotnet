using AppServices.Interfaces;
using AppServices.Services;
using HostedServiceCronos.Interfaces;

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
            _logger.LogInformation(@$"
{DateTime.Now:hh:mm:ss} CronJobExecuteOrders started working.
");

            var scope = _serviceScopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IPortfolioAppService>();
            await repository.ExecuteOrdersOfTheDayAsync().ConfigureAwait(false);

            _logger.LogInformation(@$"
{DateTime.Now:hh:mm:ss} CronJobExecuteOrders finished working.
");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred in CronJobExecuteOrders.");
        }
    }
}