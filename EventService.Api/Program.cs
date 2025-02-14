using EventService.Api.Extensions;
using EventService.Api.Middlewares;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.AddSwaggerDocumentation()
    .AddApplicationServices()
    .AddRefreshAppSettings()
    .AddJWTAuthentication()
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
