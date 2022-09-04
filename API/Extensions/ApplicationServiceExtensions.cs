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
        Console.WriteLine("this is the postgres connection string " + config.GetConnectionString("postgres"));
        Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
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
