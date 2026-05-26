using DineSmart.Core.Interfaces;

namespace DineSmart.Services.Sync;

public class NoOpNotificationService : INotificationService
{
    public Task NotifyKitchenNewOrderAsync(int orderId, CancellationToken ct = default) => Task.CompletedTask;
    public Task NotifyWaiterOrderReadyAsync(int orderId, CancellationToken ct = default) => Task.CompletedTask;
}
