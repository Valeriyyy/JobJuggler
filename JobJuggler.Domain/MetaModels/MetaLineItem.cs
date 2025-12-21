using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class MetaLineItem : BaseEntity
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    // Nullable to support non-subscription products
    public int? SubscriptionId { get; set; }
    public decimal? OverridePrice { get; set; }

    public virtual MetaInvoice Invoice { get; set; }
    public virtual Product Product { get; set; }
    public virtual Subscription? Subscription { get; set; }
}