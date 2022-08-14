using FileShare.Service.Dtos;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FileShare.Service
{
    /// <summary>
    /// Configure dependency injection for services.
    /// </summary>
    public static class Service
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.ScanServices();
            services.AddMapster();
        }


        #region Helpers

        public static void ScanServices(this IServiceCollection services)
        {
            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsMatchingInterface()
                    .WithScopedLifetime());
        }

        private static void AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            // Apply mappings
            Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
            config.Scan(applicationAssembly);

            // Add dependency injection
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        #endregion
    }
}