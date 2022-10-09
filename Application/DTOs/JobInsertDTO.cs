namespace Application.DTOs;
public class JobInsertDTO
{
    public JobClientDTO Client { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime ScheduledArrivalStartDate { get; set; }
    public DateTime ScheduledArrivalEndDate { get; set; }
    public JobLocationDTO Location { get; set; }
    public List<JobItem> JobItems { get; set; }
}

public class JobItem
{
    public int LineItemId { get; set; }
    public int Quantity { get; set; }
}

public class JobClientDTO
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}

public class JobLocationDTO
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? LocationType { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? GateCode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Notes { get; set; }
}