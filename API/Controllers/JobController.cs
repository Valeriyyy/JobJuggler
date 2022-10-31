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
    private readonly ILogger<IJobService> _logger;

    public JobController(IJobService jobService, ILogger<IJobService> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<JobReadDTO>> GetById(int jobId)
    {
        var job = await _jobService.GetJob(jobId);

        if (job is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(job);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Job>> CreateJob([FromBody] JobInsertDTO job)
    {
        var res = await _jobService.CreateJob(job);
        if (res is not null)
        {
            return CreatedAtAction(nameof(CreateJob), res);
        }
        else
        {
            return BadRequest();
        }
    }
}
