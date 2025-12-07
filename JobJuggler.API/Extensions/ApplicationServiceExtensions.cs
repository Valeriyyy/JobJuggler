using System.Security.Claims;
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
using JobJuggler.Persistence.Interceptors;
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
        services.AddControllersWithViews(opt => {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        })
            .AddRazorRuntimeCompilation()
            .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ;
        #endregion

        #region Database
        var userAccessor = new UserAccessor(new HttpContextAccessor());
        services.AddDbContext<DataContext>((sp, options) => {
            var connUrl = config.GetConnectionString("postgres");
            options.UseNpgsql(connUrl, x => x.MigrationsHistoryTable("__EFMigrationsHistory", "public")
            .MapEnums()).ConfigureWarnings(c => c.Ignore(RelationalEventId.PendingModelChangesWarning))
            .AddInterceptors(new AuditInterceptor(userAccessor.GetUserId));
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
        services.AddScoped<IRoleService, RoleService>();
        #endregion
        return services;
    }
}
