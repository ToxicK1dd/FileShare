using FileShare.Utilities.Options;

namespace FileShare.Api.Setup
{
    public static class OptionsSetup
    {
        public static void SetupOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<GatewayApiOptions>(configuration.GetSection("GatewayApi"));
        }
    }
}