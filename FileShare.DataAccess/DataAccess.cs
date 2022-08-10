﻿using FileShare.DataAccess.Base.Dto;
using FileShare.DataAccess.Models.Primary;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Reflection;

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
            services.AddMapster();
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

        private static void AddMapster(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.EnableJsonMapping();
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

            // Apply mappings
            Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            typeAdapterConfig.Scan(applicationAssembly);

            // Add dependency injection
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();
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