using JobJuggler.DTO.Job;
using JobJuggler.Application.Services;
using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace JobJuggler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase {
    private readonly IJobService _jobService;
    private readonly IValidator<JobClientDTO> _validator;
    private readonly ILogger<IJobService> _logger;

    public JobController(IJobService jobService, ILogger<IJobService> logger, IValidator<JobClientDTO> validator) {
        _jobService = jobService;
        _logger = logger;
        _validator = validator;
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<JobReadDTO>> GetJobById(int id) {
    //     var job = await _jobService.GetJob(id);
    //
    //     return job is null ? NotFound() : Ok(job);
    // }
    //
    // [HttpGet]
    // public async Task<ActionResult<List<Job>>> GetAll() {
    //     var jobs = await _jobService.GetAllJobs();
    //     return Ok(jobs);
    // }

    // [HttpPost]
    // public async Task<ActionResult<Job>> CreateJob([FromBody] JobInsertDTO job) {
    //     var clientValidationResult = _validator.Validate(job.Client);
    //     if (!clientValidationResult.IsValid) {
    //         clientValidationResult.AddToModelState(ModelState);
    //         return BadRequest(clientValidationResult);
    //     }
    //     var res = await _jobService.CreateJob(job);
    //     if (res is not null) {
    //         return CreatedAtAction(nameof(CreateJob), res);
    //     } else {
    //         return BadRequest();
    //     }
    // }

    // [HttpGet("client/{clientId}")]
    // public async Task<ActionResult<List<JobPro>>> GetClientJobs(int clientId) {
    //     var clientJobs = await _jobService.GetJobsByClientId(clientId);
    //
    //     return Ok(clientJobs);
    // }
}
