using Microsoft.Extensions.DependencyInjection;

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

        #endregion
    }
}