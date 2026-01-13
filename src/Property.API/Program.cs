using Microsoft.EntityFrameworkCore;
using Property.API.Data;
using Property.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PropertyDbContext>(options =>
    options.UseInMemoryDatabase("PropertyDb"));

builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Property Service API", Version = "v1" });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropertyDbContext>();
    context.Database.EnsureCreated();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "5002";
app.Urls.Add($"http://+:{port}");

Console.WriteLine($"Property Service starting on port {port}...");

app.Run();
