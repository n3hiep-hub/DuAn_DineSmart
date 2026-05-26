using DineSmart.Core.Entities;

namespace DineSmart.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order, CancellationToken ct = default);
    Task AddOrderItemsAsync(IEnumerable<OrderItem> items, CancellationToken ct = default);
    Task<Order?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<Order>> GetByStatusAsync(string status, CancellationToken ct = default);
    Task UpdateStatusAsync(int orderId, string status, CancellationToken ct = default);
}
