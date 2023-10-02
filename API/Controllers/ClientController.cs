using Application.DTOs.Client;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase {
    private readonly IClientService _service;

    public ClientController(IClientService service, ILogger<ClientController> logger) {
        _service = service;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDTO>> GetClientById(int id) {
        var client = await _service.GetClientById(id);

        return client is null ? NotFound() : Ok(client);
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