using Microsoft.OpenApi.Models;
using ReutersMarketDataApi.Data;
using ReutersMarketDataApi.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAssetRepository, AssetRepository>();
builder.Services.AddDbContext<AssetContext>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Management API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asset Management API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
