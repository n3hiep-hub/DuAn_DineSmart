namespace DineSmart.Core.Interfaces;

public interface INotificationService
{
    Task NotifyKitchenNewOrderAsync(int orderId, CancellationToken ct = default);
    Task NotifyWaiterOrderReadyAsync(int orderId, CancellationToken ct = default);
}
