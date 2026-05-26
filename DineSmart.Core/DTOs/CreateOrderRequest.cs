namespace DineSmart.Core.DTOs;

public class CreateOrderRequest
{
    public int TableId { get; set; }
    public string? SpecialNote { get; set; }
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public string? OptionsJson { get; set; }
}
