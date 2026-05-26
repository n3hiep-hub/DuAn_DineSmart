using DineSmart.Core.Entities;
using DineSmart.Core.Interfaces;
using DineSmart.Data.Adapters;
using Microsoft.EntityFrameworkCore;

namespace DineSmart.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IAppDbContextFactory _factory;

    public OrderRepository(IAppDbContextFactory factory)
    {
        _factory = factory;
    }

    public Task<Order> AddOrderAsync(Order order, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var entity = new DuAn_DineSmart.Models.DonHang
        {
            MaBan = order.TableId,
            SoMon = 0,
            TrangThai = order.Status,
            TongTien = order.TotalAmount,
            ThoiGian = order.CreatedAt
        };

        db.DonHangs.Add(entity);
        db.SaveChanges();
        order.Id = entity.MaDonHang;
        return Task.FromResult(order);
    }

    public Task AddOrderItemsAsync(IEnumerable<OrderItem> items, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var list = items.ToList();

        foreach (var item in list)
        {
            db.ChiTietDonHangs.Add(new DuAn_DineSmart.Models.ChiTietDonHang
            {
                MaDonHang = item.OrderId,
                MaMon = item.MenuItemId,
                SoLuong = item.Quantity,
                DonGia = item.UnitPrice
            });
        }

        var grouped = list.GroupBy(x => x.OrderId)
            .Select(g => new { OrderId = g.Key, TotalQuantity = g.Sum(x => x.Quantity) });

        foreach (var g in grouped)
        {
            var order = db.DonHangs.Find(g.OrderId);
            if (order != null)
            {
                order.SoMon = g.TotalQuantity;
            }
        }

        db.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var entity = db.DonHangs.FirstOrDefault(x => x.MaDonHang == id);
        if (entity == null) return Task.FromResult<Order?>(null);

        return Task.FromResult<Order?>(new Order
        {
            Id = entity.MaDonHang,
            TableId = entity.MaBan,
            Status = entity.TrangThai,
            TotalAmount = entity.TongTien,
            CreatedAt = entity.ThoiGian
        });
    }

    public Task<List<Order>> GetByStatusAsync(string status, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var items = db.DonHangs
            .Where(x => x.TrangThai == status)
            .OrderBy(x => x.ThoiGian)
            .Select(x => new Order
            {
                Id = x.MaDonHang,
                TableId = x.MaBan,
                Status = x.TrangThai,
                TotalAmount = x.TongTien,
                CreatedAt = x.ThoiGian
            })
            .ToList();

        return Task.FromResult(items);
    }

    public Task UpdateStatusAsync(int orderId, string status, CancellationToken ct = default)
    {
        using var db = _factory.Create();
        var order = db.DonHangs.Find(orderId);
        if (order != null)
        {
            order.TrangThai = status;
            db.SaveChanges();
        }

        return Task.CompletedTask;
    }
}
