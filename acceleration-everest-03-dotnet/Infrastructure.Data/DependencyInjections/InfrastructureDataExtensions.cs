using EntityFrameworkCore.UnitOfWork.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.DependencyInjections
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDataDependecyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WarrenEverestDotnetDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), contextLifetime: ServiceLifetime.Transient);
            services.AddUnitOfWork<WarrenEverestDotnetDbContext>(ServiceLifetime.Transient);

            return services;
        }
    }
}
