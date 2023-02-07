using Application.DTOs.Job;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly ILogger<IJobService> _logger;

    public JobController(IJobService jobService, ILogger<IJobService> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobReadDTO>> GetJobById(int id)
    {
        var job = await _jobService.GetJob(id);

        return job is null ? NotFound() : Ok(job);
    }

    [HttpGet]
    public async Task<ActionResult<List<Job>>> GetAll()
    {
        var jobs = await _jobService.GetAllJobs();
        return Ok(jobs);
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
