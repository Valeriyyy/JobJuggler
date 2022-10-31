using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Services;
public class JobService : IJobService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<JobService> _logger;

    public JobService(DataContext context, IMapper mapper, ILogger<JobService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<JobReadDTO?> GetJob(int jobId)
    {
        var job = await _context.Jobs.ProjectTo<JobReadDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(j => j.Id == jobId);
        return job;
    }

    public async Task<Job> CreateJob(JobInsertDTO jobToInsert)
    {
        if (jobToInsert.ScheduledArrivalEndDate < jobToInsert.ScheduledArrivalStartDate)
        {
            throw new Exception("Cannot set ScheduledArrivalEndDate before ScheduledArrivalStartDate");
        }
        var job = new Job
        {
            ScheduledDate = jobToInsert.ScheduledDate,
            ScheduledArrivalStartDate = jobToInsert.ScheduledArrivalStartDate,
            ScheduledArrivalEndDate = jobToInsert.ScheduledArrivalEndDate
        };
        using var trx = await _context.Database.BeginTransactionAsync();
        var jobClient = new Client();
        if (jobToInsert.Client.Id is null)
        {
            job.Client = _mapper.Map<Client>(jobToInsert.Client);
        }
        else
        {
            jobClient = await _context.Clients.Where(c => c.Id == jobToInsert.Client.Id).Select(c => new Client { Id = c.Id }).FirstOrDefaultAsync();
            if (jobClient is null)
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
            if (jobLocation is null)
            {
                throw new NullReferenceException("Location cannot be found");
            }
            job.LocationId = jobLocation.Id;
        }

        // get the list of line item ids
        var lineItemIds = jobToInsert.JobItems.Select(item => item.LineItemId);
        decimal totalPrice = 0;
        var lineItems = (await _context.LineItems.Where(item => lineItemIds.Any(l => l == item.Id)).ToListAsync()).ToDictionary(kvp => kvp.Id, kvp => kvp);
        var lines = new List<InvoiceLine>();
        foreach (var item in jobToInsert.JobItems)
        {
            var line = new InvoiceLine();
            lineItems.TryGetValue(item.LineItemId, out var dbItem);
            if (dbItem is null)
            {
                throw new Exception($"No valid item found with id of {item.LineItemId}");
            }
            if (dbItem.BasePrice == null && item.Price == null)
            {
                throw new Exception($"Price for item {dbItem.Name} must be manually entered");
            }
            if (item.Quantity == 0)
            {
                throw new Exception($"Quanity must be entered for line item {dbItem.Name}");
            }
            line.Price = (decimal)(item.Price == null && item.Price != 0 ? dbItem.BasePrice! * item.Quantity : item.Price! * item.Quantity);
            line.NumOfUnits = item.Quantity;
            line.ItemId = dbItem.Id;
            lines.Add(line);
            totalPrice += line.Price;
        }

        job.Invoice = new()
        {
            ConsigneeId = job.ClientId,
            ReferenceNumber = GetJobReferenceNumber(),
            Lines = lines,
            TotalPrice = totalPrice,
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        await trx.CommitAsync();

        return job;
    }

    public string GetJobReferenceNumber()
    {
        var today = DateTime.Now;
        var month = today.Month < 10 ? "0" + today.Month : today.Month.ToString();
        var day = today.Day < 10 ? "0" + today.Day : today.Day.ToString();
        var jobNum = _context.Jobs.Count();
        return month + day + "-" + jobNum;
    }
}
