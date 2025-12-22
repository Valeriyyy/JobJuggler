namespace JobJuggler.Domain.MetaModels;

/// <summary>
/// This is the representation of the json stored in the product options column of the products table.
/// </summary>
public class ProductOptions
{
    public int MaxUsers { get; set; }
    public int MaxJobs { get; set; }
}