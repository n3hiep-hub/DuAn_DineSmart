namespace DuAn_DineSmart.BLL
{
    public static class PhanQuyen
    {
        // Mỗi vai trò chỉ có đúng những màn hình thuộc trách nhiệm của họ
        private static readonly Dictionary<string, List<string>> _quyenHan = new()
        {
            ["Quản lý"] =
            [
                "Dashboard", "BanAn", "DatMon", "PhucVu",
                "ThucDon", "BepMan", "HoaDon", "NhanVien", "BaoCao"
            ],

            // Nhân viên phục vụ: đặt món cho khách + nhận thông báo bếp xong
            ["Nhân viên"] = ["BanAn", "DatMon", "PhucVu"],

            // Bếp: chỉ màn hình bếp, không cần xem/sửa thực đơn (việc của quản lý)
            ["Bếp"] = ["BepMan"],

            // Thu ngân: xem bàn + hóa đơn thanh toán
            ["Thu ngân"] = ["BanAn", "HoaDon"],
        };

        public static bool CoQuyen(string vaiTro, string manHinh) =>
            _quyenHan.TryGetValue(vaiTro, out var ds) && ds.Contains(manHinh);
    }
}
