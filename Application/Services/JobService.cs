using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Text.Json;

namespace Application.Services;
public class JobService : IJobService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public JobService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Job> CreateJob(JobInsertDTO jobToInsert)
    {
        if(jobToInsert.ScheduledArrivalEndDate < jobToInsert.ScheduledArrivalStartDate)
        {
            throw new Exception("Cannot set ScheduledArrivalEndDate before ScheduledArrivalStartDate");
        }
        var job = new Job
        {
            ScheduledDate = jobToInsert.ScheduledDate,
            ScheduledArrivalStartDate = jobToInsert.ScheduledArrivalStartDate,
            ScheduledArrivalEndDate = jobToInsert.ScheduledArrivalEndDate
        };
        var jobClient = _mapper.Map<Client>(jobToInsert.Client);
        if (jobToInsert.Client.Id is null)
        {
            job.Client = jobClient;
        }
        else
        {
            jobClient = await _context.Clients.Where(c => c.Id == jobToInsert.Client.Id).Select(c => new Client { Id = c.Id }).FirstOrDefaultAsync();
            if(jobClient is null)
            {
                throw new NullReferenceException("Client cannot be found");
            }
            job.ClientId = jobClient.Id;
        }

        var jobLocation = _mapper.Map<Location>(jobToInsert.Location);
        if (jobToInsert.Location.Id is null)
        {
            job.Location = jobLocation;
        }
        else
        {
            jobLocation = await _context.Locations.Where(l => l.Id == jobLocation.Id).Select(l => new Location { Id = l.Id }).FirstOrDefaultAsync();
            if(jobLocation is null)
            {
                throw new NullReferenceException("Location cannot be found");
            }
            job.LocationId = jobLocation.Id;
        }

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }
}
