using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.DAL
{
    public static class DataSeeder
    {
        public static void SeedIfEmpty(AppDbContext db)
        {
            if (db.NguoiDungs.Any()) return;

            var tables = Enumerable.Range(1, 10)
                .Select(i => new BanAn { TenBan = $"Bàn {i:D2}", TrangThai = "Trống" })
                .ToList();
            db.BanAns.AddRange(tables);

            db.ThucDons.AddRange(new[]
            {
                new ThucDon { TenMon = "Salad rau trộn",     DanhMuc = "Khai vị",     GiaTien = 45000, TrangThai = true },
                new ThucDon { TenMon = "Súp bí đỏ",          DanhMuc = "Khai vị",     GiaTien = 35000, TrangThai = true },
                new ThucDon { TenMon = "Chả giò chiên",      DanhMuc = "Khai vị",     GiaTien = 55000, TrangThai = true },
                new ThucDon { TenMon = "Cơm sườn nướng",     DanhMuc = "Món chính",   GiaTien = 85000, TrangThai = true },
                new ThucDon { TenMon = "Bún bò Huế",         DanhMuc = "Món chính",   GiaTien = 75000, TrangThai = true },
                new ThucDon { TenMon = "Phở bò tái",         DanhMuc = "Món chính",   GiaTien = 70000, TrangThai = true },
                new ThucDon { TenMon = "Cơm gà chiên mắm",   DanhMuc = "Món chính",   GiaTien = 90000, TrangThai = true },
                new ThucDon { TenMon = "Bánh flan",           DanhMuc = "Tráng miệng", GiaTien = 30000, TrangThai = true },
                new ThucDon { TenMon = "Chè đậu xanh",       DanhMuc = "Tráng miệng", GiaTien = 25000, TrangThai = true },
                new ThucDon { TenMon = "Kem dừa",             DanhMuc = "Tráng miệng", GiaTien = 35000, TrangThai = true },
                new ThucDon { TenMon = "Nước cam tươi",       DanhMuc = "Đồ uống",    GiaTien = 30000, TrangThai = true },
                new ThucDon { TenMon = "Trà đá",              DanhMuc = "Đồ uống",    GiaTien = 10000, TrangThai = true },
                new ThucDon { TenMon = "Cà phê sữa đá",      DanhMuc = "Đồ uống",    GiaTien = 35000, TrangThai = true },
                new ThucDon { TenMon = "Sinh tố bơ",          DanhMuc = "Đồ uống",    GiaTien = 45000, TrangThai = true },
            });

            string hash = BCrypt.Net.BCrypt.HashPassword("123456");
            db.NguoiDungs.AddRange(new[]
            {
                new NguoiDung { TenDangNhap = "admin",      MatKhau = hash, VaiTro = "Quản lý",  TrangThai = true },
                new NguoiDung { TenDangNhap = "bep01",      MatKhau = hash, VaiTro = "Bếp",      TrangThai = true },
                new NguoiDung { TenDangNhap = "nv01",       MatKhau = hash, VaiTro = "Nhân viên", TrangThai = true },
                new NguoiDung { TenDangNhap = "thungan01",  MatKhau = hash, VaiTro = "Thu ngân", TrangThai = true },
            });

            db.SaveChanges();
        }
    }
}
