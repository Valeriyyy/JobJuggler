using JobJuggler.API.DTOs;
using JobJuggler.Application.DTOs.Client;
using JobJuggler.Application.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase {
    private readonly IClientService _service;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientService service, ILogger<ClientController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet(Name = "Get All Clients")]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAll() {
        return Ok(await _service.GetClients());
    }

    [HttpPost(Name = "Create A Single Client")]
    public async Task<ActionResult<ClientDTO>> Create([FromBody] ClientInsertDTO client) {
        var res = await _service.CreateClient(client);

        return res is null ? BadRequest() : Ok(res);
    }

    [HttpGet("{id}", Name = "Get a minimal client record by id")]
    public async Task<ActionResult<Result<ClientDTO>>> GetClientById(int id) {
        var client = await _service.GetClientById(id);
        var result = new Result<ClientDTO>();
        result.Value = client;
        return client is null ? NotFound(result) : Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ClientDTO>> PatchClientById(int id, [FromBody] JsonPatchDocument client) {
        var cl = await _service.UpdateClient(id, client);

        return Ok(cl);
    }

    [HttpGet("profile/{id}")]
    public async Task<ActionResult<ClientProfile>> GetClientProfile(int clientId) {
        var profile = await _service.GetProfile(clientId);
        if (profile is null) {
            return NotFound();
        }
        return Ok(profile);
    }
}