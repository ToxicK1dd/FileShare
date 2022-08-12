using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace FileShare.Api.Setup
{
    public static class SwaggerSetup
    {
        /// <summary>
        /// Configure Swagger.
        /// </summary>
        /// <param name="services"></param>
        public static void SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "File Share",
                    Description = "An ASP.NET Core Web API for safely storing, and sharing images.",
                    Contact = new OpenApiContact
                    {
                        Name = "ToxicK1dd",
                        Url = new Uri("https://baek.pro/"),
                        Email = "kontact@baek.pro"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/ToxicK1dd/FileShare/blob/master/LICENSE")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                options.EnableAnnotations();
                options.OrderActionsBy(x => x.RelativePath);

                options.ExampleFilters();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                options.AddSecurityDefinition("Bearer", new()
                {
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization using bearer scheme. Enter only the access token."
                });
                options.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Reference = new()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}