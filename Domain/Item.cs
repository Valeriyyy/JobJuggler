using Domain.Models.Enums;

namespace Domain;
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public PriceType PriceType { get; set; }
}
