namespace JobJuggler.DTO.Client;

// ReSharper disable once InconsistentNaming
public class ClientInsertDTO {
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
}
