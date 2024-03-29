﻿using FileShare.Utilities.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileShare.Utilities
{
    /// <summary>
    /// Configure dependency injection for utillities.
    /// </summary>
    public static class Utillities
    {
        public static void AddUtillities(this IServiceCollection services, IConfiguration configuration)
        {
            services.ScanHelpers();
            services.ScanGenerators();

            services.Configure<GatewayApiOptions>(configuration.GetSection("GatewayApi"));
        }


        #region Helpers

        private static void ScanHelpers(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Helper")))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }

        private static void ScanGenerators(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Generator")))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }

        #endregion
    }
}