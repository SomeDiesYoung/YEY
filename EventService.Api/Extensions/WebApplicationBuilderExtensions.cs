using Destructurama;
using EventManager.FileRepository.Extensions;
using EventManager.Identity.Extensions;
using EventManager.MessageSender.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;
using EventManager.SqlRepository.Database;
using EventManager.SqlRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using EventManager.Identity.Models;

namespace EventService.Api.Extensions;

public static class WebApplicationBuilderExtensions
{

    public static WebApplicationBuilder AddSwaggerDocumentation(this WebApplicationBuilder builder)
    {

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();
        });
        builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());

        return builder;
    }


    public static WebApplicationBuilder AddRefreshAppSettings(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("appsettings.json", optional: false,reloadOnChange: true);
        return builder;
    }

    public static WebApplicationBuilder ConfiGureFileStorageOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));
        return builder;
    }

    public static WebApplicationBuilder AddJWTAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwtBearerAuthentication(builder.Configuration);
        return builder;
    }

    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()

         .ReadFrom
         .Configuration(builder.Configuration)
         .Destructure.UsingAttributes()
            .CreateLogger();

        builder.Host.UseSerilog();
        return builder;
    }
    public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServices()
                        .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddJwtToken(builder.Configuration);
        builder.Services.AddEmailSender(builder.Configuration);

        return builder;
    }
    
    public static WebApplicationBuilder AddSqlDbConnection(this WebApplicationBuilder builder)
    {
       
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        return builder;
    }
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddServices().AddSqlRepositories();
        return builder;
    }
}

