using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class Invoice : BaseEntity
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public InvoiceStatus Type { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public DateTime DateSent { get; set; }
    public DateTime DatePaid { get; set; }


    public virtual Subscription Subscription { get; set; }
    public virtual List<Product> ProductLineItems { get; set; }
}