using System.Text;
using BookConnect.API.Service;
using BookConnect.Application.Common.Interfaces;
using BookConnect.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace BookConnect.API;

public static class ConfigureServices
{
    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddAuthentication().AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Authentication:Issuer"],
                ValidAudience = config["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes(config["Authentication:SecretForKey"]!)
                )
            };
        });

        services.AddAuthorization(cfg =>
        {
            cfg.AddPolicy("RolePolicy", policy =>
            {
                policy
                .RequireAuthenticatedUser()
                .RequireRole(UserRole.Admin.ToString(), UserRole.Author.ToString(), UserRole.Reader.ToString());
            });
        });


        services.AddCors(cfg =>
        {
            cfg.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins(config["ClientURL"] ?? "")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        return services;
    } 
}