using FluentValidation;
using FluentValidation.AspNetCore;
using JobJuggler.API.Middleware;
using JobJuggler.Application.Core;
using JobJuggler.Application.Services;
using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace JobJuggler.API.Extensions;

public static class ApplicationServiceExtensions {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {
        #region Middlware
        services.AddTransient<ExceptionMiddleware>();
        #endregion

        #region Controllers
        services.AddControllers()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
        #endregion

        #region Database
        services.AddDbContext<DataContext>(options => {
            var connUrl = config.GetConnectionString("postgres");
            options.UseNpgsql(connUrl, x => x.MigrationsHistoryTable("migrations", "main"));
        });
        #endregion

        #region FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        //services.AddFluentValidation();
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
        #endregion

        #region Services
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ILocationService, LocationService>();
        services.AddTransient<IJobService, JobService>();
        services.AddTransient<IPicklistService, PicklistService>();
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        #endregion
        return services;
    }
}
