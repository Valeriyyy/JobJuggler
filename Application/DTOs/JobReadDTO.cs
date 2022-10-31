namespace Application.DTOs;
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
    public DateTime? CanceledDate { get; set; }
    public ClientDTO Client { get; set; } = null!;
    public LocationDTO Location { get; set; } = null!;
    public InvoiceDTO Invoice { get; set; } = null!;
}
