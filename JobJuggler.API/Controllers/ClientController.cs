using System.Security.Claims;
using JobJuggler.API.DTOs;
// using JobJuggler.Application.DTOs.Client;
using JobJuggler.Application.Services.Interfaces;
using JobJuggler.DTO.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
    // [Authorize(Policy = "IsRecordOwner")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Result<ClientDTO>>> GetClientById(int id) {
        var client = await _service.GetClientById(id);
        
        return client is null ? NotFound(client) : Ok(client);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ClientDTO>> PatchClientById(int id, [FromBody] JsonPatchDocument client) {
        var cl = await _service.UpdateClient(id, client);

        return Ok(cl);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> DeleteClientById(int id)
    {
        var res = await _service.DeleteClient(id);
        
        return Ok(res);
    }

    [HttpGet("profile/{id}")]
    public async Task<ActionResult<ClientProfile>> GetClientProfile(int id) {
        var profile = await _service.GetProfile(id);
        if (profile is null) {
            return NotFound();
        }
        return Ok(profile);
    }
}