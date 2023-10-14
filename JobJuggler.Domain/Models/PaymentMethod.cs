namespace JobJuggler.Domain.Models;
public class PaymentMethod {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = null!;
}
