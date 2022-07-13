using ImageApi.DataAccess.Dto;
using ImageApi.DataAccess.Models.Primary;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ImageApi.DataAccess
{
    public static class DataAccess
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddMapster();
        }


        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddDbContext<PrimaryContext>(options =>
            {
                options.UseMySql(
                connectionStrings.Primary,
                ServerVersion.Parse("10.4.13-mariadb"),
                mySqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
                });
            });
        }

        public static void AddMapster(this IServiceCollection services)
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


        internal class ConnectionStrings
        {
            public string Primary { get; set; }
        }
    }
}