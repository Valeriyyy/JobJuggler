using Application.DTOs.Client;
using Application.DTOs.Location;

namespace Application.DTOs.Job;
// Have to figure out how to map to records
// maybe cannot be done because records are init only
// and maybe automapper does not do it on init
/*public record JobReadDTO(
    int Id,
    Guid Guid,
    decimal Price,
    string? Notes,
    bool? IsCompleted,
    bool? IsCanceled,
    string? CancelReason,
    DateTime ScheduledDate,
    DateTime ScheduledArrivalStartDate,
    DateTime ScheduledArrivalEndDate,
    DateTime? StartedDate,
    DateTime? CompletedDate,
    DateTime? CanceledDate,
    ClientDTO Client,
    LocationDTO Location,
    InvoiceDTO Invoice
);*/

public class JobReadDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
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
    public DateTime? CanceledDat { get; set; }
    public ClientDTO Client { get; set; }
    public LocationDTO Location { get; set; }
    public InvoiceDTO Invoice { get; set; }
}