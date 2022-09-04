using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var job = _mapper.Map<Job>(jobToInsert);
        _context.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }
}
