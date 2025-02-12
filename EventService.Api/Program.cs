using EventManager.FileRepository.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;
using EventManager.SqlRepository.Database;
using EventManager.Identity.Models;
using EventService.Api.Extensions;
using EventService.Api.Middlewares;
using Microsoft.AspNetCore.Identity;
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
    .ConfiGureFileStorageOptions()
    .AddSqlDbConnection()
    .AddIdentity()
    .AddLogger();







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
