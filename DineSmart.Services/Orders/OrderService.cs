using DineSmart.Core.DTOs;
using DineSmart.Core.Entities;
using DineSmart.Core.Interfaces;

namespace DineSmart.Services.Orders;

public class OrderService
{
    private readonly IOrderRepository _orders;
    private readonly IMenuRepository _menu;
    private readonly ITableRepository _tables;
    private readonly INotificationService _notifications;

    public OrderService(
        IOrderRepository orders,
        IMenuRepository menu,
        ITableRepository tables,
        INotificationService notifications)
    {
        _orders = orders;
        _menu = menu;
        _tables = tables;
        _notifications = notifications;
    }

    public async Task<int> CreateOrderAsync(CreateOrderRequest request, CancellationToken ct = default)
    {
        if (request.Items.Count == 0)
        {
            throw new InvalidOperationException("Giỏ món rỗng.");
        }

        var order = new Order
        {
            TableId = request.TableId,
            CreatedAt = DateTime.Now,
            Status = OrderStatus.Pending,
            SpecialNote = request.SpecialNote
        };

        var orderItems = new List<OrderItem>();
        decimal total = 0;

        foreach (var item in request.Items)
        {
            var menuItem = await _menu.GetItemAsync(item.MenuItemId, ct)
                ?? throw new InvalidOperationException($"Không tìm thấy món: {item.MenuItemId}");

            var lineTotal = menuItem.Price * item.Quantity;
            total += lineTotal;

            orderItems.Add(new OrderItem
            {
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = menuItem.Price,
                OptionsJson = item.OptionsJson
            });
        }

        order.TotalAmount = total;
        var saved = await _orders.AddOrderAsync(order, ct);

        foreach (var oi in orderItems)
        {
            oi.OrderId = saved.Id;
        }

        await _orders.AddOrderItemsAsync(orderItems, ct);
        await _tables.UpdateStatusAsync(request.TableId, "Có khách", ct);
        await _notifications.NotifyKitchenNewOrderAsync(saved.Id, ct);

        return saved.Id;
    }

    public async Task UpdateOrderStatusAsync(int orderId, string nextStatus, CancellationToken ct = default)
    {
        await _orders.UpdateStatusAsync(orderId, nextStatus, ct);

        if (nextStatus == OrderStatus.Ready)
        {
            await _notifications.NotifyWaiterOrderReadyAsync(orderId, ct);
        }
    }
}
