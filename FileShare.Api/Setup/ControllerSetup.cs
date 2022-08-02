using FileShare.Api.Filters;
using System.Text.Json.Serialization;

namespace FileShare.Api.Setup
{
    public static class ControllerSetup
    {
        /// <summary>
        /// Configure controllers
        /// </summary>
        /// <param name="services"></param>
        public static void SetupControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<RequireEnabledFilter>();
            })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddCors();
        }
    }
}