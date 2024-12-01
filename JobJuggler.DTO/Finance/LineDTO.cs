namespace JobJuggler.DTO.Finance;

public class LineDTO {
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int NumOfUnits { get; set; }
    public decimal Price { get; set; }
    public ItemDTO Item { get; set; } = null!;
}
