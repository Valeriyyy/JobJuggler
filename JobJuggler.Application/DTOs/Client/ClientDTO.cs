namespace JobJuggler.Application.DTOs.Client;
public class ClientDTO {
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}
