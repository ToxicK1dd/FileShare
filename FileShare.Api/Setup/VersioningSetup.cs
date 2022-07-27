using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Setup
{
    public static class VersioningSetup
    {
        /// <summary>
        /// Configure versioning.
        /// </summary>
        /// <param name="services"></param>
        public static void SetupVersioning(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }
    }
}