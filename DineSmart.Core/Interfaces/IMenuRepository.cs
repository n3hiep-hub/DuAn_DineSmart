using DineSmart.Core.Entities;

namespace DineSmart.Core.Interfaces;

public interface IMenuRepository
{
    Task<List<MenuCategory>> GetCategoriesAsync(CancellationToken ct = default);
    Task<List<MenuItem>> GetActiveItemsAsync(CancellationToken ct = default);
    Task<MenuItem?> GetItemAsync(int menuItemId, CancellationToken ct = default);
}
