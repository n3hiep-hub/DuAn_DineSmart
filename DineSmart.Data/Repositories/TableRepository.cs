using DineSmart.Core.Interfaces;
using DineSmart.Data.Adapters;

namespace DineSmart.Data.Repositories;

public class TableRepository : ITableRepository
{
    private readonly IAppDbContextFactory _factory;

    public TableRepository(IAppDbContextFactory factory)
    {
        _factory = factory;
    }

    public Task UpdateStatusAsync(int tableId, string status, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var table = db.BanAns.Find(tableId);
        if (table != null)
        {
            table.TrangThai = status;
            db.SaveChanges();
        }

        return Task.CompletedTask;
    }
}
