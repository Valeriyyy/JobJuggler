using JobJuggler.API.Services;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace JobJuggler.API.Extensions;

public static class IdentityServiceExtensions {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config) {
        services.AddIdentityCore<AppUser>(opt => {
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<DataContext>();
        
        services.AddScoped<IPasswordHasher<AppUser>, IdentityUtilities.PasswordHasher<AppUser>>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Identity:SecurityTokenKey"]!));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        services.AddScoped<TokenService>();

        return services;
    }
}