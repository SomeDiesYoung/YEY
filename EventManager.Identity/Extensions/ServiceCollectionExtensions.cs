using EventManager.Identity.Models;
using EventManager.Identity.Services.Abstractions;
using EventManager.Identity.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EventManager.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IJwtTokenService, JwtTokenService>()
                    .Configure<JwtTokenServiceOptions>(configuration.GetSection(JwtTokenServiceOptions.Authentication));
            return services;
        }

        public static IdentityBuilder AddIdentityServices(this IServiceCollection services)=>services
              .AddScoped<IIdentityService, IdentityService>()
              .AddIdentity<ApplicationUser, IdentityRole>(DefaultSetupAction)
              .AddTokenProvider<ResetPasswordTokenProvider<ApplicationUser>>("ResetPassword")
                    .AddDefaultTokenProviders();
      


        private static void DefaultSetupAction(IdentityOptions options)
        {
            options.Password = new PasswordOptions
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true,
                RequiredLength = 8,
                RequiredUniqueChars = 3,
            };

            options.Lockout = new LockoutOptions { AllowedForNewUsers = true, DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3), MaxFailedAccessAttempts = 3, };

            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;
        }
    }
}
