namespace Application.DTOs;
public class JobInsertDTO
{
    public int ClientId { get; set; }
    public int LocationId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime ScheduledArrivalStartDate { get; set; }
    public DateTime ScheduledArrivalEndDate { get; set; }
}
