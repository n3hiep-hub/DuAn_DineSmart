namespace DineSmart.Core.Entities;

public class MenuCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
