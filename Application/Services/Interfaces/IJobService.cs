using Application.DTOs.Job;
using Domain.Models;

namespace Application.Services.Interfaces;
public interface IJobService
{
    public Task<List<Job>> GetAllJobs();
    public Task<JobReadDTO?> GetJob(int jobId);
    public Task<Job> CreateJob(JobInsertDTO jobToInsert);
}
