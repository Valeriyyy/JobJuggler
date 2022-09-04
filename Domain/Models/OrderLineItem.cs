namespace Domain.Models;
public class OrderLineItem
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }
    public int NumOfUnits { get; set; }
    public int ItemId { get; set; }
    public decimal Price { get; set; }

    public Job Order { get; set; } = null!;
    public Item Item { get; set; } = null!;
}
