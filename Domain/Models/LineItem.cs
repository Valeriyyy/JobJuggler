using Domain.Models.Enums;

namespace Domain.Models;
public class LineItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? BasePrice { get; set; }
    public PriceType PriceType { get; set; }

    public virtual ICollection<InvoiceLine> Invoices{ get; set; } = null!;
}
