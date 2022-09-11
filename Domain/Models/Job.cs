namespace Domain.Models;
public class Job
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public int LocationId { get; set; }
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


    public virtual Client Client { get; set; } = null!;
    public virtual Location Location { get; set; } = null!;
    public virtual ICollection<Invoice> Invoices { get; set; } = null!;
}
