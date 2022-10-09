namespace Domain.Models;

public class Client
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public virtual ICollection<Job> Jobs { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }

    public override string ToString()
    {
        return $"{Name} {Phone} {Email}";
    }
}
