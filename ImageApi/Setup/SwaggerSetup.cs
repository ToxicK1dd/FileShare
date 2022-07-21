using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace ImageApi.Setup
{
    public static class SwaggerSetup
    {
        public static void SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Image Api",
                    Description = "An ASP.NET Core Web API for safely storing, and sharing images.",
                    Contact = new OpenApiContact
                    {
                        Name = "ToxicK1dd",
                        Url = new Uri("https://baek.pro/"),
                        Email = "contact@baek.pro"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/ToxicK1dd/ImageApi/blob/master/LICENSE")
                    }
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Image Api",
                    Description = "An ASP.NET Core Web API for safely storing, and sharing images.",
                    Contact = new OpenApiContact
                    {
                        Name = "ToxicK1dd",
                        Url = new Uri("https://baek.pro/"),
                        Email = "contact@baek.pro"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/ToxicK1dd/ImageApi/blob/master/LICENSE")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                options.EnableAnnotations();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.ExampleFilters();

                options.AddSecurityDefinition("Bearer", new()
                {
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization using bearer scheme. Enter only the JWT."
                });
                options.AddSecurityRequirement(new()
                {
                    {
                        new()
                        {
                            Name = HeaderNames.Authorization,
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
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