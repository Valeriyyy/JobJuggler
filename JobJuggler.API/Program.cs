using JobJuggler.API.Extensions;
using JobJuggler.API.Middleware;
using JobJuggler.Domain.IdentityModels;
using JobJuggler.Persistence;
using JobJuggler.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using Serilog;

// set environment to local
// $env:ASPNETCORE_ENVIRONMENT = 'Local'

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


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
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction()) {
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("JobJuggler")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);

    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map API controllers and MVC routes
app.MapControllers();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Serve SPA index.html for client-side routes that are not handled by controllers or static files
app.MapFallback(async context => {
    var env = app.Services.GetRequiredService<IWebHostEnvironment>();
    var indexPath = Path.Combine(env.WebRootPath ?? string.Empty, "dist", "index.html");
    if (System.IO.File.Exists(indexPath))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(indexPath);
        return;
    }
    context.Response.StatusCode = 404;
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// try {
//     var context = services.GetRequiredService<DataContext>();
//     // context.Database.Migrate();
//     var userManager = services.GetRequiredService<UserManager<AppUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
//     
//     var identitySeed = new IdentitySeed(context, userManager, roleManager);
//     await identitySeed.SeedIdentity();
//     await MainSeed.SeedData(context);
// } catch (Exception ex) {
//     Console.WriteLine("Something has gone wrong seeding the database");
//     Console.WriteLine(ex.Message);
//     Console.WriteLine(ex.StackTrace);
// }

app.Run();