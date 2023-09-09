using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); // ADDITIONAL SERVICES
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

//Middleware
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// Allow Access multiorigin webserver to api
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); //Access Controllers

// Load Seed Data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
  var context = services.GetRequiredService<DataContext>();
  await context.Database.MigrateAsync();
  await Seed.SeedUers(context);
}
catch (Exception ex)
{
  var logger = services.GetService<ILogger<Program>>();
  logger.LogError(ex,"An error occured during migration");
}

app.Run();
