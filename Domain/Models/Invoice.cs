namespace Domain.Models;
public class Invoice
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ConsigneeId { get; set; }
    public string? ReferenceNumber { get; set; }
    public decimal Total { get; set; }
    public int PaymentMethodId { get; set; }
    public bool IsPaid { get; set; }
    public DateTime DateInvoiced { get; set; }
    public DateTime DatePaid { get; set; }
    public DateTime DateClosed { get; set; }
}
