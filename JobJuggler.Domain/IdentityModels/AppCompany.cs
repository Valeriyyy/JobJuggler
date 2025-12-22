using System.ComponentModel.DataAnnotations.Schema;
using JobJuggler.Domain.MetaModels;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.IdentityModels;

public class AppCompany : BaseEntity {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? PrimaryContactId { get; set; }

    //Metadata Fields
    // public DateTime DateCreated { get; set; }
    // public int CreatedById { get; set; }
    // public DateTime? DateLastModified { get; set; }
    // public int? LastModifiedById { get; set; }
    // public bool IsDeleted { get; set; }
    // public DateTime? DateDeleted { get; set; }
    // public int? DeletedById { get; set; }

    public virtual List<AppUser> Users { get; set; } = [];
    public virtual List<Subscription> Subscriptions { get; set; } = [];
    public virtual List<Contact> Contacts { get; set; } = [];

    // Collection of all billing infos (history). This is mapped by EF Core.
    public virtual List<CompanyBillingInfo> BillingInfos { get; set; } = [];

    // Expose the current active billing info as a computed, non-mapped property.
    // EF Core will not treat this as a navigation so it won't create a second relationship.
    [NotMapped]
    public CompanyBillingInfo? BillingInfo => BillingInfos?.FirstOrDefault(b => !b.IsDeleted);
    public virtual Contact? PrimaryContact { get; set; }
}