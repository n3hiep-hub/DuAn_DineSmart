namespace DineSmart.Core.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImagePath { get; set; }
    public bool IsActive { get; set; } = true;
}
