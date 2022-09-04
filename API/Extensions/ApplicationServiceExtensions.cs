using Application.Core;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
            var connUrl = config.GetConnectionString("postgres");
            options.UseNpgsql(connUrl);
        });
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<IJobService, JobService>();
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        return services;
    }
}
