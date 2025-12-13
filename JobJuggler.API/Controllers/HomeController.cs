using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IO;
using JobJuggler.DTO.Client;

namespace JobJuggler.API.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        // Serve the built Svelte SPA index.html directly at the root so asset URLs resolve reliably
        var indexPath = Path.Combine(_env.WebRootPath ?? string.Empty, "dist", "index.html");
        if (System.IO.File.Exists(indexPath))
        {
            return PhysicalFile(indexPath, "text/html");
        }

        // Fallback to the Razor view if the SPA build is not present
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SomeDataDisplay()
    {
        var client = new ClientDTO
        {
            Id = 1,
            Guid = Guid.NewGuid(),
            Name = "Acme Corporation",
            Phone = "555-1234",
            Email = "gggg@gmiakl.com"
        };

        return View(client);
    }

    [HttpGet("/{id:int}")]
    public ClientDTO Get(int id)
    {
        var name = id % 2 == 0 ? "Even Corp" : "Odd Inc";
        var email = id % 2 == 0 ? "john@doe.com" : "jane:@doe.com";
        var phone = id % 2 == 0 ? "123-456-7890" : "098-765-4321";
        return new ClientDTO
        {
            Id = id,
            Name = name,
            Email = email,
            Phone = phone
        };
    }
}