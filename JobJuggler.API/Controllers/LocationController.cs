using JobJuggler.DTO.Location;
using JobJuggler.Application.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase {
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService) {
        _locationService = locationService;
    }

    // [HttpGet(Name = "Get All Locations")]
    // public async Task<ActionResult<List<LocationDTO>>> GetAllLocations() {
    //     var res = await _locationService.GetAllLocations();
    //     return Ok(res);
    // }
    //
    // [HttpPost(Name = "Create a Single Location")]
    // public async Task<ActionResult<LocationDTO>> CreateLocation([FromBody] LocationInsertDTO locationInsert) {
    //     var res = await _locationService.CreateLocation(locationInsert);
    //
    //     if (res is not null) {
    //         return CreatedAtAction(nameof(CreateLocation), res);
    //     } else {
    //         return BadRequest();
    //     }
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<LocationDTO>> GetLocationById(int id) {
    //     var loc = await _locationService.GetLocationById(id);
    //
    //     return loc;
    // }
    //
    // [HttpPatch("{id}")]
    // public async Task<ActionResult<LocationDTO>> PatchClientById(int id, [FromBody] JsonPatchDocument location) {
    //     var loc = await _locationService.UpdateLocation(id, location);
    //
    //     return Ok(loc);
    // }
}
