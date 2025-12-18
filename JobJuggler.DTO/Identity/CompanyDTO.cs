namespace JobJuggler.DTO.Identity;

public class CompanyDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MainContactName { get; set; }
    public string MainContactEmail { get; set; }
    public string MainContactPhone { get; set; }
    public DateTime DateCreated { get; set; }
}