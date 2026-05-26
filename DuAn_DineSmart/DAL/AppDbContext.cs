using DuAn_DineSmart.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DineSmart.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<BanAn> BanAns { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ThucDon> ThucDons { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString = "Server=localhost;Port=3306;Database=DineSmart;User=root;Password=;";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}