namespace Application.DTOs.Job;
public class JobListDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string? Client { get; set; }
    public string? ClientPhone { get; set; }
    public string? PostalCode { get; set; }
    public int MyProperty { get; set; }
}
