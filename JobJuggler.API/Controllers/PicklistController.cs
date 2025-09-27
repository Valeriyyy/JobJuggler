using JobJuggler.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicklistController : ControllerBase {
    private readonly IPicklistService _service;

    public PicklistController(IPicklistService service) {
        _service = service;
    }

    // [HttpGet(Name = "Get All Options")]
    // public async Task<ActionResult> GetAllOptions() {
    //     var options = await _service.GetAll();
    //
    //     return Ok(options);
    // }
}
