using FileShare.DataAccess.Models.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace FileShare.DataAccess
{
    /// <summary>
    /// Configure dependency injection for the database.
    /// </summary>
    public static class DataAccess
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPrimaryDatabase(configuration);
            services.AddUnitOfWork();
        }


        #region Helpers

        private static void AddPrimaryDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddDbContext<PrimaryContext>(options =>
            {
                options.UseMySql(
                connectionStrings.Primary,
                ServerVersion.Parse("10.4.13-mariadb"),
                mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
                })
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }, ServiceLifetime.Scoped);
        }

        private static void AddUnitOfWork(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("UnitOfWork")))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }

        private class ConnectionStrings
        {
            public string Primary { get; set; }
        }

        #endregion
    }
}