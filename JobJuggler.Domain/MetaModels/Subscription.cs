using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.MetaModels.Enums;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class Subscription : BaseEntity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ProductId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public SubscriptionStatus Status { get; set; }
    public decimal? PriceOverride { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual AppCompany Company { get; set; }
}