using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
        var jobClient = _mapper.Map<Client>(jobToInsert.Client);
        if (jobToInsert.Client.Id is null)
        {
            job.Client = jobClient;
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
        var lineItems = _context.LineItems.Where(item => lineItemIds.Any(l => l == item.Id)).AsEnumerable().ToDictionary(kvp => kvp.Id, kvp => kvp);
        var lines = new List<InvoiceLine>();
        foreach (var item in jobToInsert.JobItems)
        {
            var line = new InvoiceLine();
            lineItems.TryGetValue(item.LineItemId, out var dbItem);
            if (dbItem is null)
            {
                throw new Exception($"No valid item found with id of {item.LineItemId}");
            }
            if(dbItem.BasePrice == null && item.Price == null)
            {
                throw new Exception($"Price for item {dbItem.Name} must be manually entered");
            }
            if(item.Quantity == 0)
            {
                throw new Exception($"Quanity must be entered for line item {dbItem.Name}");
            }
            line.Price = (decimal)(item.Price == null ? dbItem.BasePrice! * item.Quantity : item.Price! * item.Quantity);
            line.NumOfUnits = item.Quantity;
            line.ItemId = dbItem.Id;
            lines.Add(line);
            totalPrice += line.Price;
        }
        job.Invoices = new List<Invoice>
        {
            new Invoice
            {
                ConsigneeId = job.ClientId,
                ReferenceNumber = "boogaloo",
                Lines = lines,
                PaymentMethodId = 1,
                TotalPrice = totalPrice,
            }
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }
}
