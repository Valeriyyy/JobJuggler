using JobJuggler.Domain.MetaModels.Enums;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductType Type { get; set; }
    public BillingPeriod BillingPeriod { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public ProductOptions ProductOptions { get; set; }
    
    // public virtual List<Subscription> Subscriptions { get; set; }
}