using JobJuggler.API.Extensions;
using JobJuggler.API.Middleware;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using JobJuggler.Persistence.Seeds;
using Serilog;
using Microsoft.AspNetCore.Identity;

// set environment to local
// $env:ASPNETCORE_ENVIRONMENT = 'Local'

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddLogging(l => {
    l.ClearProviders().AddSerilog(new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger());
});

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try {
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await MainSeed.SeedData(context);
    await IdentitySeed.SeedIdentity(context, userManager);
} catch (Exception ex) {
    Console.WriteLine("Something has gone wrong seeding the database");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}

app.Run();

