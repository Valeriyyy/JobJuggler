namespace JobJuggler.DTO.Client;
public class ClientProfile {
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }

    public IEnumerable<ClientProfileJob> Jobs { get; set; }
}


public class ClientProfileJob {
    public int Id { get; set; }
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

    public ClientProfileLocation Location { get; set; }
}

public class ClientProfileLocation {
    public string? LocationType { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}