using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmMain : Form
    {
        private readonly NguoiDung _nguoiDung;
        private readonly DashboardDAL _dal = new();
        private Form? _childForm;
        private System.Windows.Forms.Timer? _badgeTimer;

        public frmMain(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Text = $"DineSmart – {_nguoiDung.VaiTro}: {_nguoiDung.TenDangNhap}";
            ApDungPhanQuyen();
        }

        // ─── Phân quyền & wire-up nút sidebar ────────────────────────────────
        private void ApDungPhanQuyen()
        {
            string vt = _nguoiDung.VaiTro;

            btnDashboard.Visible = PhanQuyen.CoQuyen(vt, "Dashboard");
            btnBanAn.Visible     = PhanQuyen.CoQuyen(vt, "BanAn");
            btnDatMon.Visible    = PhanQuyen.CoQuyen(vt, "DatMon");
            btnThucDon.Visible   = PhanQuyen.CoQuyen(vt, "ThucDon");
            btnHoaDon.Visible    = PhanQuyen.CoQuyen(vt, "HoaDon");
            btnNhanVien.Visible  = PhanQuyen.CoQuyen(vt, "NhanVien");
            btnBaoCao.Visible    = PhanQuyen.CoQuyen(vt, "BaoCao");
            btnBep.Visible       = PhanQuyen.CoQuyen(vt, "BepMan");
            btnPhucVu.Visible    = PhanQuyen.CoQuyen(vt, "PhucVu");

            if (btnDashboard.Visible)
                btnDashboard.Click += (s, e) => NavigateTo(null, "Dashboard", btnDashboard);

            if (btnBanAn.Visible)
                btnBanAn.Click += (s, e) => NavigateTo(new frmBanAn(), "Bàn ăn", btnBanAn);

            if (btnDatMon.Visible)
                btnDatMon.Click += (s, e) => NavigateTo(new frmDatMon(), "Đặt món", btnDatMon);

            if (btnThucDon.Visible)
                btnThucDon.Click += (s, e) => NavigateTo(new frmThucDon(), "Thực đơn", btnThucDon);

            if (btnHoaDon.Visible)
                btnHoaDon.Click += (s, e) => NavigateTo(new frmHoaDon(_nguoiDung), "Hóa đơn", btnHoaDon);

            if (btnBep.Visible)
                btnBep.Click += (s, e) => NavigateTo(new frmBep(), "Màn hình bếp", btnBep);

            if (btnPhucVu.Visible)
                btnPhucVu.Click += (s, e) => NavigateTo(new frmNhanVienBep(), "Thông báo phục vụ", btnPhucVu);

            if (btnNhanVien.Visible)
                btnNhanVien.Click += (s, e) => NavigateTo(new frmNhanVien(), "Nhân viên", btnNhanVien);

            if (btnBaoCao.Visible)
                btnBaoCao.Click += (s, e) => NavigateTo(new frmBaoCao(), "Báo cáo", btnBaoCao);

            // Badge "Chờ phục vụ" trên nút sidebar — chỉ cho vai trò có quyền PhucVu
            if (PhanQuyen.CoQuyen(vt, "PhucVu"))
            {
                _badgeTimer = new System.Windows.Forms.Timer { Interval = 5000 };
                _badgeTimer.Tick += (s, e) => CapNhatBadgePhucVu();
                _badgeTimer.Start();
                CapNhatBadgePhucVu();
            }

            // Màn hình mặc định theo vai trò
            if (PhanQuyen.CoQuyen(vt, "Dashboard"))
                NavigateTo(null, "Dashboard", btnDashboard);
            else if (PhanQuyen.CoQuyen(vt, "BepMan"))
                NavigateTo(new frmBep(), "Màn hình bếp", btnBep);
            else if (PhanQuyen.CoQuyen(vt, "PhucVu"))
                NavigateTo(new frmNhanVienBep(), "Thông báo phục vụ", btnPhucVu);
            else if (PhanQuyen.CoQuyen(vt, "DatMon"))
                NavigateTo(new frmDatMon(), "Đặt món", btnDatMon);
            else if (PhanQuyen.CoQuyen(vt, "HoaDon"))
                NavigateTo(new frmHoaDon(_nguoiDung), "Hóa đơn", btnHoaDon);
        }

        private void CapNhatBadgePhucVu()
        {
            try
            {
                int so = _dal.GetChoPhucVuCount();
                btnPhucVu.Text = so > 0
                    ? $"  Thông báo ({so})"
                    : "  Thông báo phục vụ";
                btnPhucVu.ForeColor = so > 0 ? Color.FromArgb(255, 220, 100) : Color.White;
            }
            catch { /* bỏ qua lỗi polling */ }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _badgeTimer?.Stop();
            _badgeTimer?.Dispose();
            base.OnFormClosed(e);
        }

        // ─── Core navigation: nhúng form vào pnlContent ──────────────────────
        private void NavigateTo(Form? form, string pageTitle, Button activeBtn)
        {
            // Dọn form cũ
            if (_childForm != null && !_childForm.IsDisposed)
            {
                _childForm.Close();
                _childForm.Dispose();
            }
            _childForm = null;
            foreach (Control c in pnlContent.Controls.OfType<Form>().ToList())
                pnlContent.Controls.Remove(c);

            lblPageTitle.Text = pageTitle;
            SetActiveSidebarButton(activeBtn);

            if (form == null)
            {
                // Về Dashboard – refresh dữ liệu mới nhất
                pnlDashboard.Visible = true;
                LoadDashboard();
                return;
            }

            // Nhúng form vào panel chính
            pnlDashboard.Visible = false;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(form);
            form.BringToFront();
            form.Show();

            // Ẩn thanh tiêu đề riêng của form (đã có topbar frmMain)
            var innerTop = form.Controls.Find("pnlTop", false).FirstOrDefault();
            if (innerTop != null) innerTop.Visible = false;

            _childForm = form;
        }

        private void SetActiveSidebarButton(Button active)
        {
            Button[] all = { btnDashboard, btnBanAn, btnDatMon, btnThucDon,
                             btnHoaDon, btnBep, btnPhucVu, btnNhanVien, btnBaoCao };
            foreach (var b in all)
            {
                if (b == null) continue;
                bool isActive = b == active;
                b.BackColor = isActive ? Color.FromArgb(145, 35, 25) : Color.FromArgb(192, 57, 43);
                b.Font = new Font("Segoe UI", 10, isActive ? FontStyle.Bold : FontStyle.Regular);
            }
        }

        // ─── Dashboard content ────────────────────────────────────────────────
        private void LoadDashboard()
        {
            try
            {
                lblDoanhThu.Text = _dal.GetDoanhThuHomNay().ToString("N0") + " đ";
                var (busy, tong) = _dal.GetThongKeBan();
                lblBanAn.Text  = $"{busy} / {tong}";
                lblDonHang.Text = _dal.GetDonHangHomNay().ToString();
                lblNhanVien.Text = "—";
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
            foreach (var ban in _dal.GetAllBan())
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
                (btn.BackColor, btn.ForeColor, btn.FlatAppearance.BorderColor) = ban.TrangThai switch
                {
                    "Có khách"  => (Color.FromArgb(252, 235, 235), Color.FromArgb(163, 45, 45),  Color.FromArgb(226, 75, 74)),
                    "Đặt trước" => (Color.FromArgb(250, 238, 218), Color.FromArgb(99, 56, 6),    Color.FromArgb(239, 159, 39)),
                    _           => (Color.FromArgb(234, 243, 222), Color.FromArgb(39, 80, 10),   Color.FromArgb(99, 153, 34)),
                };
                flpBanAn.Controls.Add(btn);
            }
        }

        private void LoadDonHang()
        {
            lvDonHang.Items.Clear();
            foreach (var dh in _dal.GetDonHangGanDay())
            {
                var item = new ListViewItem(dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}");
                item.SubItems.Add($"{dh.SoMon} món");
                item.SubItems.Add(dh.TrangThai);
                item.SubItems.Add(dh.TongTien.ToString("N0") + "đ");
                item.ForeColor = dh.TrangThai switch
                {
                    "Hoàn thành"   => Color.FromArgb(39, 80, 10),
                    "Đã phục vụ"   => Color.FromArgb(8, 80, 65),
                    "Chờ phục vụ"  => Color.FromArgb(12, 68, 124),
                    _              => Color.FromArgb(99, 56, 6)
                };
                lvDonHang.Items.Add(item);
            }
        }
    }
}
