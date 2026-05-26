namespace DuAn_DineSmart.BLL
{
    public static class PhanQuyen
    {
        public static bool CoQuyen(string vaiTro, string manHinh)
        {
            var quyenHan = new Dictionary<string, List<string>>
            {
                ["Quản lý"] = new()
                {
                    "Dashboard", "BanAn", "DatMon", "PhucVu", "ThucDon",
                    "BepMan", "HoaDon", "NhanVien", "BaoCao"
                },
                ["Nhân viên"] = new()
                {
                    "BanAn", "DatMon"
                },
                ["Bếp"] = new()
                {
                    "ThucDon", "BepMan"
                },
                ["Thu ngân"] = new()
                {
                    "BanAn", "HoaDon"
                }
            };

            return quyenHan.TryGetValue(vaiTro, out var ds)
                && ds.Contains(manHinh);
        }
    }
}