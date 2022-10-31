namespace Application.DTOs;
public class ItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? BasePrice { get; set; }
    public string PriceType { get; set; }
}
