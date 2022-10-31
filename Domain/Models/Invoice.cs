namespace Domain.Models;
public class Invoice
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int JobId { get; set; }
    public int ConsigneeId { get; set; }
    public string? ReferenceNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public int? PaymentMethodId { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? DateInvoiced { get; set; }
    public DateTime? DatePaid { get; set; }
    public DateTime? DateClosed { get; set; }

    public virtual Job Job { get; set; } = null!;
    public virtual Client Consignee { get; set; } = null!;
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    public virtual ICollection<InvoiceLine> Lines { get; set; } = null!;
}
