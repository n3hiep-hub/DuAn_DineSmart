namespace DineSmart.Core.Interfaces;

public interface ITableRepository
{
    Task UpdateStatusAsync(int tableId, string status, CancellationToken ct = default);
}
