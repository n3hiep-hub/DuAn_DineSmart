using DuAn_DineSmart.Forms;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Chạy 1 lần để lấy hash → copy vào MySQL → xóa đi
            //string hash = BCrypt.Net.BCrypt.HashPassword("123456");
            //MessageBox.Show(hash, "Hash mật khẩu");


            ApplicationConfiguration.Initialize();
            Application.Run(new DuAn_DineSmart.Forms.frmLogin());

            // Test tạm frmMain với user giả
 /*           ApplicationConfiguration.Initialize();
            var testUser = new NguoiDung
            {
                TenDangNhap = "admin",
                VaiTro = "Quản lý"
            };
            Application.Run(new frmMain(testUser));*/
        }
    }
}