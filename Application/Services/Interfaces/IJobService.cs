using JobJuggler.Application.DTOs.Job;
using JobJuggler.Domain.Models;

namespace JobJuggler.Application.Services.Interfaces;
public interface IJobService {
    public Task<List<Job>> GetAllJobs();
    public Task<JobReadDTO?> GetJob(int jobId);
    public Task<Job> CreateJob(JobInsertDTO jobToInsert);
    public Task<List<JobPro>> GetJobsByClientId(int clientId);
}
