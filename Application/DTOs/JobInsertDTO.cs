namespace Application.DTOs;
public class JobInsertDTO
{
    public ClientInsertDTO Client { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime ScheduledArrivalStartDate { get; set; }
    public DateTime ScheduledArrivalEndDate { get; set; }
    public LocationInsertDTO Location { get; set; }
    public List<JobItem> JobItems { get; set; }
}

public class JobItem
{
    public int LineItemId { get; set; }
    public int Quantity { get; set; }
}