using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _service;

    public ClientController(IClientService service)
    {
        _service = service;
    }

    [HttpGet("/Get-All-Clients", Name = "Get All Clients")]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAll()
    {
        return Ok(await _service.GetClients());
    }

    [HttpPost("/Create-Client", Name = "Create A Single Client")]
    public async Task<ActionResult<ClientDTO>> Create([FromBody] ClientInsertDTO client)
    {
        var res = await _service.CreateClient(client);
        if(res is not null)
        {
            return Ok(res);
        } else
        {
            return BadRequest();
        }
    }

    [HttpGet("/Get-By-Id", Name = "Get Client By Id")]
    public async Task<ActionResult<ClientDTO>> GetClientById(int id)
    {
        var res = await _service.GetClientById(id);
        if(res is null)
        {
            return NotFound();
        } else
        {
            return Ok(res);
        }
    }
}
