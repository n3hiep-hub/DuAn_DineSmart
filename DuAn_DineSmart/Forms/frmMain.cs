using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmMain : Form
    {
        private NguoiDung _nguoiDung;
        private DashboardDAL _dal = new DashboardDAL();

        public frmMain(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
        }

        /*        private void frmMain_Load(object sender, EventArgs e)
                {
                    this.Text = $"DineSmart – {_nguoiDung.VaiTro}: {_nguoiDung.TenDangNhap}";
                    // Kết nối các nút sidebar
                    btnBanAn.Click += (s, ev) => new frmBanAn().Show();
                    btnDatMon.Click += (s, ev) => new frmDatMon().Show();
                    btnThucDon.Click += (s, ev) => new frmThucDon().Show();
                    btnHoaDon.Click += (s, ev) => new frmHoaDon(_nguoiDung).Show();
                    btnNhanVien.Click += (s, ev) => new frmNhanVien().Show();
                    btnBaoCao.Click += (s, ev) => new frmBaoCao().Show();
                    LoadDashboard();
                }*/
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"DineSmart – {_nguoiDung.VaiTro}: {_nguoiDung.TenDangNhap}";
            ApDungPhanQuyen();
            LoadDashboard();
        }
        private void ApDungPhanQuyen()
        {
            string vt = _nguoiDung.VaiTro;

            // Ẩn/hiện nút sidebar theo vai trò
            btnDashboard.Visible = PhanQuyen.CoQuyen(vt, "Dashboard");
            btnBanAn.Visible = PhanQuyen.CoQuyen(vt, "BanAn");
            btnDatMon.Visible = PhanQuyen.CoQuyen(vt, "DatMon");
            btnThucDon.Visible = PhanQuyen.CoQuyen(vt, "ThucDon");
            btnHoaDon.Visible = PhanQuyen.CoQuyen(vt, "HoaDon");
            btnNhanVien.Visible = PhanQuyen.CoQuyen(vt, "NhanVien");
            btnBaoCao.Visible = PhanQuyen.CoQuyen(vt, "BaoCao");
            btnBep.Visible = PhanQuyen.CoQuyen(vt, "BepMan");
            btnPhucVu.Visible = PhanQuyen.CoQuyen(vt, "PhucVu");

            // Kết nối sự kiện click
            if (PhanQuyen.CoQuyen(vt, "BanAn"))
                btnBanAn.Click += (s, ev) => new frmBanAn().Show();

            if (PhanQuyen.CoQuyen(vt, "DatMon"))
                btnDatMon.Click += (s, ev) => new frmDatMon().Show();

            if (PhanQuyen.CoQuyen(vt, "ThucDon"))
                btnThucDon.Click += (s, ev) => new frmThucDon().Show();

            if (PhanQuyen.CoQuyen(vt, "HoaDon"))
                btnHoaDon.Click += (s, ev) => new frmHoaDon(_nguoiDung).Show();

            if (PhanQuyen.CoQuyen(vt, "NhanVien"))
                btnNhanVien.Click += (s, ev) => new frmNhanVien().Show();

            if (PhanQuyen.CoQuyen(vt, "BaoCao"))
                btnBaoCao.Click += (s, ev) => new frmBaoCao().Show();

            if (PhanQuyen.CoQuyen(vt, "BepMan"))
                btnBep.Click += (s, ev) => new frmBep().Show();

            if (PhanQuyen.CoQuyen(vt, "PhucVu"))
                btnPhucVu.Click += (s, ev) => new frmNhanVienBep().Show();
            
            // Nếu không có quyền Dashboard, chuyển thẳng sang màn hình phù hợp
            if (!PhanQuyen.CoQuyen(vt, "Dashboard"))
            {
                pnlContent.Visible = false;
                lblPageTitle.Text = $"Xin chào, {_nguoiDung.TenDangNhap}!";

                if (PhanQuyen.CoQuyen(vt, "DatMon"))
                    btnDatMon.PerformClick();
                else if (PhanQuyen.CoQuyen(vt, "HoaDon"))
                    btnHoaDon.PerformClick();
                else if (PhanQuyen.CoQuyen(vt, "BepMan"))
                    MessageBox.Show("Chào mừng đến màn hình bếp!", "Bếp");
            }
        }
        private void LoadDashboard()
        {
            try
            {
                lblDoanhThu.Text = _dal.GetDoanhThuHomNay().ToString("N0") + " đ";
                var (busy, tong) = _dal.GetThongKeBan();
                lblBanAn.Text = $"{busy} / {tong}";
                lblDonHang.Text = _dal.GetDonHangHomNay().ToString();
                LoadBanAn();
                LoadDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBanAn()
        {
            flpBanAn.Controls.Clear();
            var dsBan = _dal.GetAllBan();

            foreach (var ban in dsBan)
            {
                var btn = new Button
                {
                    Text = $"{ban.TenBan}\n{ban.TrangThai}",
                    Size = new Size(80, 55),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8),
                    Margin = new Padding(4),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                btn.FlatAppearance.BorderSize = 1;

                switch (ban.TrangThai)
                {
                    case "Có khách":
                        btn.BackColor = Color.FromArgb(252, 235, 235);
                        btn.ForeColor = Color.FromArgb(163, 45, 45);
                        btn.FlatAppearance.BorderColor = Color.FromArgb(226, 75, 74);
                        break;
                    case "Đặt trước":
                        btn.BackColor = Color.FromArgb(250, 238, 218);
                        btn.ForeColor = Color.FromArgb(99, 56, 6);
                        btn.FlatAppearance.BorderColor = Color.FromArgb(239, 159, 39);
                        break;
                    default:
                        btn.BackColor = Color.FromArgb(234, 243, 222);
                        btn.ForeColor = Color.FromArgb(39, 80, 10);
                        btn.FlatAppearance.BorderColor = Color.FromArgb(99, 153, 34);
                        break;
                }
                flpBanAn.Controls.Add(btn);
            }
        }

        private void LoadDonHang()
        {
            lvDonHang.Items.Clear();
            var dsOrder = _dal.GetDonHangGanDay();

            foreach (var dh in dsOrder)
            {
                var item = new ListViewItem(dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}");
                item.SubItems.Add($"{dh.SoMon} món");
                item.SubItems.Add(dh.TrangThai);
                item.SubItems.Add(dh.TongTien.ToString("N0") + "đ");

                item.ForeColor = dh.TrangThai switch
                {
                    "Hoàn thành" => Color.FromArgb(39, 80, 10),
                    "Đang phục vụ" => Color.FromArgb(8, 80, 65),
                    _ => Color.FromArgb(99, 56, 6)
                };
                lvDonHang.Items.Add(item);
            }
        }
    }
}