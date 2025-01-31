using EventManager.FileRepository.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace EventService.Api.Extensions
{
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


        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddServices().AddFileRepositories();
            return builder;
        }
    }
}

