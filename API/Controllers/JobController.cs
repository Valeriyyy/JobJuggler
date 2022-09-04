using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetAllOrders()
    {
        Console.WriteLine("this is where it would get all orders");
        await Task.Delay(100);
        return Ok("your mom gay");
    }

    [HttpPost("/Create-Job")]
    public async Task<ActionResult<Job>> CreateJob([FromBody] JobInsertDTO job)
    {
        var res = await _jobService.CreateJob(job);
        if(res is not null)
        {
            return CreatedAtAction(nameof(CreateJob),res);
        } else
        {
            return BadRequest();
        }
    }
}
