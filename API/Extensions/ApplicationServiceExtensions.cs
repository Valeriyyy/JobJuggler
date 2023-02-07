using API.Middleware;
using Application.Core;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Text.Json.Serialization;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ExceptionMiddleware>();
        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddDbContext<DataContext>(options =>
        {
            var connUrl = config.GetConnectionString("postgres");
            options.UseNpgsql(connUrl, x => x.MigrationsHistoryTable("migrations", "crystal_clean"));
        });
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<IJobService, JobService>();
        services.AddTransient<IPicklistService, PicklistService>();
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
