using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace PracticeWebApi.Extensions;
public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtValues = configuration.GetSection("JwtValues").Get<JwtValues>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtValues.Issuer,
                    ValidAudience = jwtValues.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtValues.Key))
                };
            });

        return services;
    }
}

