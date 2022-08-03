using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace FileShare.Api.Setup
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
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = false;
            });
        }
    }
}