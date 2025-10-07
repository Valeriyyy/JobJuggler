namespace JobJuggler.Domain.MetaModels;

public class LineItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public decimal? OverridePrice { get; set; }
    public bool IsValid { get; set; }

    public virtual Invoice Invoice { get; set; }
    public virtual Product Product { get; set; }
}