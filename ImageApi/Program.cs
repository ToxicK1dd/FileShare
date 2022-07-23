using ImageApi.DataAccess;
using ImageApi.Service;
using ImageApi.Setup;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Setup services in the container
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.SetupBearer(builder.Configuration);
builder.Services.SetupSwagger();
builder.Services.SetupVersioning();

// Add DI to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddServices();


// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.EnvironmentName != "Production")
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(options =>
{
    options.RouteTemplate = "/{documentName}/swagger.json";
});
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Image API";
    options.RoutePrefix = string.Empty;

    options.InjectStylesheet("/swagger-ui/custom.css");
    options.InjectJavascript("/swagger-ui/custom.js");

    options.EnableFilter();
    options.DisplayRequestDuration();
    options.DefaultModelsExpandDepth(-1);
    options.DocExpansion(DocExpansion.None);
    options.EnablePersistAuthorization(); ;

    options.SwaggerEndpoint($"/v2/swagger.json", $"ImageAPI - v2");
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed((host) => true));

app.MapControllers();

app.Run();