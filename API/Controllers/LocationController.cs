using Application.DTOs;
using Application.DTOs.Location;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet(Name = "Get All Locations")]
    public async Task<ActionResult<List<LocationDTO>>> GetAllLocations()
    {
        var res = await _locationService.GetAllLocations();
        return Ok(res);
    }

    [HttpPost(Name = "Create a Single Location")]
    public async Task<ActionResult<LocationDTO>> CreateLocation([FromBody] LocationInsertDTO locationInsert)
    {
        var res = await _locationService.CreateLocation(locationInsert);

        if (res is not null)
        {
            return CreatedAtAction(nameof(CreateLocation), res);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDTO>> GetLocationById(int id)
    {
        var loc = await _locationService.GetLocationById(id);

        return loc;
    }
}
