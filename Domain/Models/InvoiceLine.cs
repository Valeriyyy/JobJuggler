namespace Domain.Models;
public class InvoiceLine
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int InvoiceId { get; set; }
    public int NumOfUnits { get; set; }
    public int ItemId { get; set; }
    public decimal Price { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
    public virtual LineItem Item { get; set; } = null!;
}
