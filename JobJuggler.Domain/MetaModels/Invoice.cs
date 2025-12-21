using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class MetaInvoice : BaseEntity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public InvoiceStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total
    {
        get => SubTotal + Tax;
        private set;
    }
    public DateTime? DatePaid { get; set; }
    
    public virtual List<MetaLineItem>? LineItems { get; set; }
}