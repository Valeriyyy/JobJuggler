using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class CompanyBillingInfo : BaseEntity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int ContactId { get; set; }
    public string PaymentToken { get; set; }
    public string Last4Digits { get; set; }
    public string CardBrand { get; set; }
    public string ExpMonth { get; set; }
    public string ExpYear { get; set; }
    
    public virtual AppCompany Company { get; set; }
    public virtual Contact Contact { get; set; }
}