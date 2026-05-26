using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmHoaDon : Form
    {
        private List<BanAn> _dsBan = new();
        private DonHang? _donHangHienTai;
        private string _phuongThuc = "Tiền mặt";
        private NguoiDung _nguoiDung;

        public frmHoaDon(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadBanCoKhach();
            lbBan.SelectedIndexChanged += LbBan_Changed;
            btnThanhToan.Click += BtnThanhToan_Click;

            foreach (Button btn in new[] { btnTienMat, btnThe, btnQR })
                btn.Click += PayBtn_Click;
        }

        private void LoadBanCoKhach()
        {
            using var db = new AppDbContext();
            _dsBan = db.BanAns
                .Where(b => b.TrangThai == "Có khách")
                .OrderBy(b => b.TenBan)
                .ToList();

            lbBan.Items.Clear();
            foreach (var b in _dsBan)
            {
                int soMon = db.DonHangs
                    .Where(d => d.MaBan == b.MaBan && d.TrangThai != "Hoàn thành")
                    .Sum(d => (int?)d.SoMon) ?? 0;
                lbBan.Items.Add($"{b.TenBan}  ({soMon} món)");
            }
        }

        private void LbBan_Changed(object? sender, EventArgs e)
        {
            int idx = lbBan.SelectedIndex;
            if (idx < 0 || idx >= _dsBan.Count) return;
            var ban = _dsBan[idx];
            LoadHoaDon(ban);
        }

        private void LoadHoaDon(BanAn ban)
        {
            using var db = new AppDbContext();

            _donHangHienTai = db.DonHangs
                .Where(d => d.MaBan == ban.MaBan && d.TrangThai != "Hoàn thành")
                .OrderByDescending(d => d.ThoiGian)
                .FirstOrDefault();

            lblBanInfo.Text = $"{ban.TenBan}  |  {DateTime.Now:dd/MM/yyyy HH:mm}";
            lblNVInfo.Text = $"Thu ngân: {_nguoiDung.TenDangNhap}";

            lvChiTiet.Items.Clear();
            decimal tong = 0;

            if (_donHangHienTai != null)
            {
                var chiTiet = db.ChiTietDonHangs
                    .Where(c => c.MaDonHang == _donHangHienTai.MaDonHang)
                    .ToList();

                foreach (var ct in chiTiet)
                {
                    var mon = db.ThucDons.Find(ct.MaMon);
                    if (mon == null) continue;
                    decimal thanh = ct.DonGia * ct.SoLuong;
                    tong += thanh;
                    var item = new ListViewItem(mon.TenMon);
                    item.SubItems.Add(ct.SoLuong.ToString());
                    item.SubItems.Add(ct.DonGia.ToString("N0") + "đ");
                    item.SubItems.Add(thanh.ToString("N0") + "đ");
                    lvChiTiet.Items.Add(item);
                }
            }

            lblTamTinh.Text = $"Tạm tính: {tong:N0}đ";
            lblGiamGia.Text = "Giảm giá: 0đ";
            lblTongTT.Text = $"Tổng thanh toán: {tong:N0}đ";
        }

        private void PayBtn_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;
            _phuongThuc = clicked.Tag?.ToString() ?? "Tiền mặt";

            foreach (Button btn in new[] { btnTienMat, btnThe, btnQR })
            {
                bool active = btn == clicked;
                btn.BackColor = active ? Color.FromArgb(234, 243, 222) : Color.White;
                btn.ForeColor = active ? Color.FromArgb(39, 80, 10) : Color.FromArgb(80, 80, 80);
                btn.FlatAppearance.BorderColor = active
                    ? Color.FromArgb(99, 153, 34)
                    : Color.FromArgb(200, 200, 200);
            }
        }

        private void BtnThanhToan_Click(object? sender, EventArgs e)
        {
            if (_donHangHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo");
                return;
            }

            var xn = MessageBox.Show(
                $"Xác nhận thanh toán?\nPhương thức: {_phuongThuc}\n{lblTongTT.Text}",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (xn != DialogResult.Yes) return;

            try
            {
                using var db = new AppDbContext();

                // Cập nhật trạng thái đơn hàng
                var dh = db.DonHangs.Find(_donHangHienTai.MaDonHang);
                if (dh != null) dh.TrangThai = "Hoàn thành";

                // Cập nhật trạng thái bàn về Trống
                var ban = db.BanAns.Find(_donHangHienTai.MaBan);
                if (ban != null) ban.TrangThai = "Trống";

                db.SaveChanges();

                MessageBox.Show("Thanh toán thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload danh sách bàn
                _donHangHienTai = null;
                lvChiTiet.Items.Clear();
                lblTamTinh.Text = "Tạm tính: 0đ";
                lblGiamGia.Text = "Giảm giá: 0đ";
                lblTongTT.Text = "Tổng thanh toán: 0đ";
                lblBanInfo.Text = "Chọn bàn để xem hóa đơn";
                LoadBanCoKhach();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }
    }
}