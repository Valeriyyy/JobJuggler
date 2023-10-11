namespace JobJuggler.Application.DTOs.Location;
public class LocationDTO {
    public int Id { get; set; }
    public Guid Guid { get; set; }
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
