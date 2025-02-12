using EventManager.MessageSender.Abstractions.Models;
using EventManager.MessageSender.Abstractions.Services.Abstractions;
using EventManager.MessageSender.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.MessageSender.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddEmailSender(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IEmailSenderService, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderOptions.EmailSenderSettings));
            return services;
        }
    }
}
