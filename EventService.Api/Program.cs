using EventManager.FileRepository.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;
using EventManager.SqlRepository.Database;
using EventService.Api.Extensions;
using EventService.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.AddSwaggerDocumentation()
    .AddApplicationServices()
    .AddRefreshAppSettings()
    .AddJWTAuthentication()
    .ConfiGureFileStorageOptions();



Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandlingMiddleware();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
