using EventManager.Service.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class JwtConfigurationExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new()
                 {
                     ValidateAudience = true,
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = configuration.GetJwtIssuer(),
                     ValidAudience = configuration.GetJwtAudience(),
                     IssuerSigningKey = configuration.GetIssuerSigningKey(),


                     ClockSkew = TimeSpan.Zero
                 };
    });
        return services;
    }

    public static string GetJwtIssuer(this IConfiguration configuration)
    {
        return configuration.GetValueOrThrow("Authentication:Issuer");
    }
    
    public static string GetJwtAudience(this IConfiguration configuration)
    {
        return configuration.GetValueOrThrow("Authentication:Audience");
    }
        
    public static string GetJwtSecretKey(this IConfiguration configuration)
    {
        return configuration.GetValueOrThrow("Authentication:SecretKey");
    }        
    public static SecurityKey GetIssuerSigningKey(this IConfiguration configuration)
    {
        return new SymmetricSecurityKey(Convert.FromBase64String(configuration.GetJwtSecretKey()));
    }


    public static string GetValueOrThrow(this IConfiguration configuration, string key)
    {
        return configuration[key] ?? throw new ConfigurationException($"{key} was not provided in configuration file");
    }
}

