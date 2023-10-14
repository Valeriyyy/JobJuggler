namespace JobJuggler.Domain.Models;
public class PaymentIssue {
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Issue { get; set; }
}
