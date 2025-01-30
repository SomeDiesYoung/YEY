using EventManager.FileRepository.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;
using EventService.Api.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration.GetValue<string>("Authentication:SecretKey"))),


            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>{
    options.ExampleFilters();
    });

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));



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
