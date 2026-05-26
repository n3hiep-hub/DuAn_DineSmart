using DuAn_DineSmart.Models;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace DuAn_DineSmart.DAL
{
    public class DashboardDAL
    {
        // Lấy tất cả bàn ăn
        public List<BanAn> GetAllBan()
        {
            using var db = new AppDbContext();
            return db.BanAns.ToList();
        }

        // Lấy đơn hàng gần đây (10 đơn mới nhất)
        public List<DonHang> GetDonHangGanDay()
        {
            using var db = new AppDbContext();
            return db.DonHangs
                .Include(d => d.Ban)
                .OrderByDescending(d => d.ThoiGian)
                .Take(10)
                .ToList();
        }

        // Thống kê
        public decimal GetDoanhThuHomNay()
        {
            using var db = new AppDbContext();
            return db.DonHangs
                .Where(d => d.ThoiGian.Date == DateTime.Today
                         && d.TrangThai == "Hoàn thành")
                .Sum(d => (decimal?)d.TongTien) ?? 0;
        }

        public (int dangPhucVu, int tongBan) GetThongKeBan()
        {
            using var db = new AppDbContext();
            int tong = db.BanAns.Count();
            int busy = db.BanAns.Count(b => b.TrangThai == "Có khách");
            return (busy, tong);
        }

        public int GetDonHangHomNay()
        {
            using var db = new AppDbContext();
            return db.DonHangs
                .Count(d => d.ThoiGian.Date == DateTime.Today);
        }
    }
}