using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using JobJuggler.API.Middleware;
using JobJuggler.Application;
using JobJuggler.Application.Core;
using JobJuggler.Application.Services;
using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Infrastructure.Security;
using JobJuggler.Persistence;
using JobJuggler.Persistence.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace JobJuggler.API.Extensions;

public static class ApplicationServiceExtensions {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {
        #region Middlware
        services.AddTransient<ExceptionMiddleware>();
        #endregion

        #region Controllers
        services.AddControllers(opt => {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        })
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ;
        #endregion

        #region Database
        services.AddDbContext<DataContext>(options => {
            var connUrl = config.GetConnectionString("postgres");
            options.UseNpgsql(connUrl, x => x.MigrationsHistoryTable("__EFMigrationsHistory", "public")
                .MapEnums()).ConfigureWarnings(c => c.Ignore(RelationalEventId.PendingModelChangesWarning));
        });
        #endregion

        #region FluentValidation
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        //services.AddFluentValidation();
        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
        #endregion

        #region Services
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IPicklistService, PicklistService>();
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        #endregion
        return services;
    }
}
