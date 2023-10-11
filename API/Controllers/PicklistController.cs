using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicklistController : ControllerBase {
    private readonly IPicklistService _service;
    private readonly IClientService _clientService;

    public PicklistController(IPicklistService service, IClientService clientService) {
        _service = service;
        _clientService = clientService;
    }

    [HttpGet(Name = "Get All Options")]
    public async Task<ActionResult> GetAllOptions() {
        var options = await _service.GetAll();

        return Ok(options);
    }
}
