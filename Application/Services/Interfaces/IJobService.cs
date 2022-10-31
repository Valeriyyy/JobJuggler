using Application.DTOs;
using Domain.Models;

namespace Application.Services.Interfaces;
public interface IJobService
{
    public Task<JobReadDTO?> GetJob(int jobId);
    public Task<Job> CreateJob(JobInsertDTO jobToInsert);
}
