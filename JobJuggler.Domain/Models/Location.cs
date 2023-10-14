using NpgsqlTypes;

namespace JobJuggler.Domain.Models;
public class Location {
    //public Location() { }

    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string? Name { get; set; }
    public string? LocationType { get; set; }
    //public LocationType LocationType { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? GateCode { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Notes { get; set; }
    public NpgsqlTsVector VectorAddress { get; set; }

    public virtual ICollection<Job> Jobs { get; set; }

    public override string ToString() {
        return $"{Name} {LocationType} {Street1} {Street2} {City} {State} {PostalCode} {Country} {GateCode} {Notes}";
    }
}
