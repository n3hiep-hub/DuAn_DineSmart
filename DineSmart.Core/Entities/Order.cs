namespace DineSmart.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public string? SpecialNote { get; set; }
}
