using DineSmart.Core.Entities;
using DineSmart.Core.Interfaces;
using DineSmart.Data.Adapters;

namespace DineSmart.Data.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IAppDbContextFactory _factory;

    public MenuRepository(IAppDbContextFactory factory)
    {
        _factory = factory;
    }

    public Task<List<MenuCategory>> GetCategoriesAsync(CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var categories = db.ThucDons
            .Where(m => m.TrangThai)
            .Select(m => m.DanhMuc)
            .Distinct()
            .OrderBy(x => x)
            .Select((name, index) => new MenuCategory
            {
                Id = index + 1,
                Name = name,
                IsActive = true
            })
            .ToList();

        return Task.FromResult(categories);
    }

    public Task<List<MenuItem>> GetActiveItemsAsync(CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var items = db.ThucDons
            .Where(m => m.TrangThai)
            .Select(m => new MenuItem
            {
                Id = m.MaMon,
                Name = m.TenMon,
                Description = string.Empty,
                Price = m.GiaTien,
                CategoryId = 0,
                ImagePath = null,
                IsActive = m.TrangThai
            })
            .ToList();

        return Task.FromResult(items);
    }

    public Task<MenuItem?> GetItemAsync(int menuItemId, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var item = db.ThucDons
            .Where(m => m.MaMon == menuItemId && m.TrangThai)
            .Select(m => new MenuItem
            {
                Id = m.MaMon,
                Name = m.TenMon,
                Description = string.Empty,
                Price = m.GiaTien,
                CategoryId = 0,
                ImagePath = null,
                IsActive = m.TrangThai
            })
            .FirstOrDefault();

        return Task.FromResult(item);
    }
}
