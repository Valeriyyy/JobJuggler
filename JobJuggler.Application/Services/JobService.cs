using JobJuggler.Application.Services.Interfaces;
using JobJuggler.Persistence;
using Microsoft.Extensions.Logging;

namespace JobJuggler.Application.Services;

public class JobService : IJobService
{
    private readonly DataContext _context;
    private readonly ILogger<JobService> _logger;

    public JobService(DataContext context, ILogger<JobService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // public async Task<JobReadDTO?> GetJob(int jobId) {
    //     var job = await _context.Jobs
    //         .ProjectTo<JobReadDTO>(_mapper.ConfigurationProvider)
    //         .FirstOrDefaultAsync(j => j.Id == jobId);
    //     return job;
    // }
    //
    // public async Task<List<Job>> GetAllJobs() {
    //     var jobs = await _context.Jobs.ToListAsync();
    //     return jobs;
    // }
    //
    // public async Task<Job> CreateJob(JobInsertDTO jobToInsert) {
    //     if (jobToInsert.ScheduledArrivalEndDate < jobToInsert.ScheduledArrivalStartDate) {
    //         throw new ValidationException("Cannot set ScheduledArrivalEndDate before ScheduledArrivalStartDate");
    //     }
    //     var job = new Job {
    //         ScheduledDate = jobToInsert.ScheduledDate,
    //         ScheduledArrivalStartDate = jobToInsert.ScheduledArrivalStartDate,
    //         ScheduledArrivalEndDate = jobToInsert.ScheduledArrivalEndDate
    //     };
    //     using var trx = await _context.Database.BeginTransactionAsync();
    //     var jobClient = _mapper.Map<Client>(jobToInsert.Client);
    //     var someClient = ClientMapper.JobClientToClientModel(jobToInsert.Client);
    //     if (jobToInsert.Client.Id is null) {
    //         // assigning the uncreated client directly to the job creates the client along with the job
    //         // during context.savechanges
    //         job.Client = jobClient;
    //     } else {
    //         jobClient = await _context.Clients
    //             .Where(c => c.Id == jobToInsert.Client.Id)
    //             .Select(c => new Client { Id = c.Id })
    //             .FirstOrDefaultAsync();
    //         if (jobClient is null) {
    //             throw new RecordNotFoundException(typeof(Client), jobToInsert.Client.Id);
    //         }
    //         // assigning the id rather than the object creates the job with the already existing client rather than
    //         // creating a new client
    //         job.ClientId = jobClient.Id;
    //     }
    //
    //     var jobLocation = _mapper.Map<Location>(jobToInsert.Location);
    //     if (jobToInsert.Location.Id is null) {
    //         // assigning the location to the job this way creates the location
    //         job.Location = jobLocation;
    //     } else {
    //         jobLocation = await _context.Locations
    //             .Where(l => l.Id == jobLocation.Id)
    //             .Select(l => new Location { Id = l.Id })
    //             .FirstOrDefaultAsync();
    //         if (jobLocation is null) {
    //             throw new RecordNotFoundException($"No location found with Id {jobToInsert.Location.Id}");
    //         }
    //         // this alternative step sets the id of the supposedly already existing location on the job
    //         // this way it does not create a new location
    //         job.LocationId = jobLocation.Id;
    //     }
    //
    //     // get the list of line item ids
    //     var lineItemIds = jobToInsert.JobItems.Select(item => item.LineItemId);
    //     decimal totalPrice = 0;
    //     var lineItems = (await _context.LineItems
    //         .Where(item => lineItemIds
    //         .Any(l => l == item.Id))
    //         .ToListAsync())
    //         .ToDictionary(kvp => kvp.Id, kvp => kvp);
    //     var lines = new List<InvoiceLine>();
    //     foreach (var item in jobToInsert.JobItems) {
    //         var line = new InvoiceLine();
    //         lineItems.TryGetValue(item.LineItemId, out var dbItem);
    //         if (dbItem is null) {
    //             throw new Exception($"No valid item found with id of {item.LineItemId}");
    //         }
    //         if (dbItem.BasePrice == null && item.Price == null) {
    //             throw new Exception($"Price for item {dbItem.Name} must be manually entered");
    //         }
    //         if (item.Quantity == 0) {
    //             throw new Exception($"Quanity must be entered for line item {dbItem.Name}");
    //         }
    //         line.Price = (decimal)(item.Price == null && item.Price != 0 ? dbItem.BasePrice! * item.Quantity : item.Price! * item.Quantity);
    //         line.NumOfUnits = item.Quantity;
    //         line.ItemId = dbItem.Id;
    //         lines.Add(line);
    //         totalPrice += line.Price;
    //     }
    //
    //     job.Invoice = new() {
    //         ReferenceNumber = GetJobReferenceNumber(),
    //         Lines = lines,
    //         TotalPrice = totalPrice,
    //     };
    //     if (jobToInsert.Client.Id is null) {
    //         job.Invoice.Consignee = job.Client;
    //     } else {
    //         job.Invoice.ConsigneeId = job.ClientId;
    //     }
    //
    //     _context.Jobs.Add(job);
    //     await _context.SaveChangesAsync();
    //     await trx.CommitAsync();
    //
    //     return job;
    // }
    //
    // private string GetJobReferenceNumber() {
    //     var today = DateTime.Now;
    //     var month = today.Month < 10 ? "0" + today.Month : today.Month.ToString();
    //     var day = today.Day < 10 ? "0" + today.Day : today.Day.ToString();
    //     var jobNum = _context.Jobs.Count();
    //     return month + day + "-" + jobNum;
    // }
    //
    // public async Task<List<JobPro>> GetJobsByClientId(int clientId) {
    //     var clientJobs = await _context.Jobs
    //         .Include(l => l.Location)
    //         .Where(x => x.ClientId == clientId)
    //         .ToListAsync();
    //
    //     var jj = await _context.Jobs
    //         .Select(j => new JobPro {
    //             Id = j.Id,
    //             Price = j.Price,
    //             Notes = j.Notes,
    //             IsCompleted = j.IsCompleted,
    //             IsCanceled = j.IsCanceled,
    //             CancelReason = j.CancelReason,
    //             CanceledDate = j.CanceledDate,
    //             ScheduledDate = j.ScheduledDate,
    //             ScheduledArrivalStartDate = j.ScheduledArrivalStartDate,
    //             ScheduledArrivalEndDate = j.ScheduledArrivalEndDate,
    //             StartedDate = j.StartedDate,
    //             CompletedDate = j.CompletedDate,
    //             ClientId = j.ClientId,
    //             Location = new LocationWithoutVector {
    //                 LocationType = j.Location.LocationType,
    //                 Street1 = j.Location.Street1,
    //                 Street2 = j.Location.Street2,
    //                 City = j.Location.City,
    //                 State = j.Location.State,
    //                 PostalCode = j.Location.PostalCode,
    //                 Country = j.Location.Country,
    //                 GateCode = j.Location.GateCode,
    //                 Latitude = j.Location.Latitude,
    //                 Longitude = j.Location.Longitude
    //             }
    //         })
    //         .Where(x => x.ClientId == clientId)
    //         .ToListAsync();
    //     return jj;
    // }
}

public class JobPro
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public bool? IsCompleted { get; set; }
    public bool? IsCanceled { get; set; }
    public string? CancelReason { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime ScheduledArrivalStartDate { get; set; }
    public DateTime ScheduledArrivalEndDate { get; set; }
    public DateTime? StartedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public DateTime? CanceledDate { get; set; }

    public LocationWithoutVector Location { get; set; }
}

public class LocationWithoutVector
{
    public string? LocationType { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? GateCode { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}