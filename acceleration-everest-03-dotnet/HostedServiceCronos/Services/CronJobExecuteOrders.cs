using HostedServiceCronos.Interfaces;

namespace HostedServiceCronos.Services
{
    public class CronJobExecuteOrders : CronJobService
    {        
        public CronJobExecuteOrders(IScheduleConfig<CronJobExecuteOrders> config)
        : base(config.CronExpression, config.TimeZoneInfo)
        {
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            return base.DoWork(cancellationToken);
        }
    }
}