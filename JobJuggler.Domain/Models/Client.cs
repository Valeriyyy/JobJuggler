using JobJuggler.Domain.IdentityModels;

namespace JobJuggler.Domain.Models;

public class Client : BaseEntity {
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    
    public virtual List<Job> Jobs { get; set; }
    public virtual List<Invoice> Invoices { get; set; }

    public override string ToString() {
        return $"{Name} {Phone} {Email}";
    }
}
