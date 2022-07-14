using ImageApi.DataAccess;
using ImageApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(options =>
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
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy =>
    {
        policy.AuthenticationSchemes.Add(
                JwtBearerDefaults.AuthenticationScheme);

        policy.RequireAuthenticatedUser();
    });
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
// Add DI
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Image API";
        options.RoutePrefix = string.Empty;

        options.EnableFilter();
        options.DisplayRequestDuration();

        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");

    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();