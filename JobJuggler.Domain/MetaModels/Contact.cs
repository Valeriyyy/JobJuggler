using JobJuggler.Domain.IdentityModels;
using JobJuggler.Domain.Models;

namespace JobJuggler.Domain.MetaModels;

public class Contact : BaseEntity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    
    public virtual AppCompany Company { get; set; }
}