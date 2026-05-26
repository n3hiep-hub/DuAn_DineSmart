using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.DAL
{
    public class NguoiDungDAL
    {
        public NguoiDung? KiemTraDangNhap(string tenDangNhap, string matKhau, string vaiTro)
        {
            using var db = new AppDbContext();

            var nguoiDung = db.NguoiDungs.FirstOrDefault(u =>
                u.TenDangNhap == tenDangNhap &&
                u.VaiTro == vaiTro &&
                u.TrangThai == true);

            if (nguoiDung == null) return null;

            // Xác thực mật khẩu BCrypt
            bool hopLe = BCrypt.Net.BCrypt.Verify(matKhau, nguoiDung.MatKhau);
            return hopLe ? nguoiDung : null;
        }
    }
}