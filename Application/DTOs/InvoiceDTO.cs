using Domain.Models;

namespace Application.DTOs;
public class InvoiceDTO
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ConsigneeId { get; set; }
    public string? ReferenceNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? DateInvoiced { get; set; }
    public DateTime? DatePaid { get; set; }
    public DateTime? DateClosed { get; set; }
    public PaymentMethodDTO PaymentMethod { get; set; } = null!;
    public ICollection<LineDTO> Lines { get; set; } = null!;
}
