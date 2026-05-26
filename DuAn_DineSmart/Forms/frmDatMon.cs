using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmDatMon : Form
    {
        private DashboardDAL _dalBan = new();
        private List<ThucDon> _dsMonAn = new();
        private Dictionary<int, (ThucDon mon, int soLuong)> _gioMon = new();
        private int _maBanChon = -1;
        private string _filterDanhMuc = "Tất cả";

        public frmDatMon() { InitializeComponent(); }

        private void frmDatMon_Load(object sender, EventArgs e)
        {
            LoadBanAn();
            LoadThucDon();
            lbBanAn.SelectedIndexChanged += LbBanAn_Changed;
            btnGuiBep.Click += BtnGuiBep_Click;
            btnXoaTat.Click += (s, ev) => { _gioMon.Clear(); RefreshGio(); };
        }

        private void LoadBanAn()
        {
            lbBanAn.Items.Clear();
            var dsBan = _dalBan.GetAllBan();
            foreach (var b in dsBan)
                lbBanAn.Items.Add($"{b.TenBan} – {b.TrangThai}");
            lbBanAn.Tag = dsBan;
        }

        private void LbBanAn_Changed(object? sender, EventArgs e)
        {
            if (lbBanAn.Tag is not List<BanAn> dsBan) return;
            int idx = lbBanAn.SelectedIndex;
            if (idx < 0) return;
            var ban = dsBan[idx];
            _maBanChon = ban.MaBan;
            lblBanHienTai.Text = $"{ban.TenBan} – {ban.TrangThai}";
            lblTitleGio.Text = $"GIỎ MÓN – {ban.TenBan}";
            _gioMon.Clear();
            RefreshGio();
        }

        private void LoadThucDon()
        {
            using var db = new AppDbContext();
            _dsMonAn = db.ThucDons.Where(m => m.TrangThai).ToList();

            // Tạo nút danh mục
            pnlDanhMuc.Controls.Clear();
            var danhMucs = new[] { "Tất cả" }
                .Concat(_dsMonAn.Select(m => m.DanhMuc).Distinct())
                .ToList();

            int x = 12;
            foreach (var dm in danhMucs)
            {
                var btn = new Button
                {
                    Text = dm,
                    Size = new Size(100, 28),
                    Location = new Point(x, 6),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9),
                    Tag = dm
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                SetCatBtnStyle(btn, dm == _filterDanhMuc);
                btn.Click += (s, ev) =>
                {
                    _filterDanhMuc = btn.Tag?.ToString() ?? "Tất cả";
                    foreach (Control c in pnlDanhMuc.Controls)
                        if (c is Button b) SetCatBtnStyle(b, b.Tag?.ToString() == _filterDanhMuc);
                    RenderMonAn();
                };
                pnlDanhMuc.Controls.Add(btn);
                x += 108;
            }
            RenderMonAn();
        }

        private void SetCatBtnStyle(Button btn, bool active)
        {
            btn.BackColor = active ? Color.FromArgb(192, 57, 43) : Color.White;
            btn.ForeColor = active ? Color.White : Color.FromArgb(80, 80, 80);
        }

        private void RenderMonAn()
        {
            flpMonAn.Controls.Clear();
            var ds = _filterDanhMuc == "Tất cả"
                ? _dsMonAn
                : _dsMonAn.Where(m => m.DanhMuc == _filterDanhMuc).ToList();

            foreach (var mon in ds)
            {
                var card = new Panel
                {
                    Size = new Size(140, 90),
                    Margin = new Padding(6),
                    BackColor = Color.White,
                    Tag = mon
                };
                card.Paint += (s, e) =>
                    ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                        Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);

                var lblTen = new Label
                {
                    Text = mon.TenMon,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Location = new Point(8, 8),
                    Size = new Size(124, 32),
                };

                var lblGia = new Label
                {
                    Text = mon.GiaTien.ToString("N0") + "đ",
                    Font = new Font("Segoe UI", 9),
                    ForeColor = Color.FromArgb(192, 57, 43),
                    Location = new Point(8, 40),
                    AutoSize = true
                };

                var btnThem = new Button
                {
                    Text = "+ Thêm",
                    Size = new Size(124, 26),
                    Location = new Point(8, 58),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(192, 57, 43),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8),
                    Tag = mon
                };
                btnThem.FlatAppearance.BorderSize = 0;
                btnThem.Click += BtnThem_Click;

                card.Controls.AddRange(new Control[] { lblTen, lblGia, btnThem });
                flpMonAn.Controls.Add(card);
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (_maBanChon < 0)
            {
                MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo");
                return;
            }
            if (sender is Button btn && btn.Tag is ThucDon mon)
            {
                if (_gioMon.ContainsKey(mon.MaMon))
                    _gioMon[mon.MaMon] = (mon, _gioMon[mon.MaMon].soLuong + 1);
                else
                    _gioMon[mon.MaMon] = (mon, 1);
                RefreshGio();
            }
        }

        private void RefreshGio()
        {
            lvGioMon.Items.Clear();
            decimal tong = 0;
            foreach (var kv in _gioMon)
            {
                var (mon, sl) = kv.Value;
                decimal thanh = mon.GiaTien * sl;
                tong += thanh;
                var item = new ListViewItem(mon.TenMon);
                item.SubItems.Add(sl.ToString());
                item.SubItems.Add(thanh.ToString("N0") + "đ");
                item.Tag = kv.Key;
                lvGioMon.Items.Add(item);
            }
            lblTongTien.Text = $"Tổng: {tong:N0}đ";
        }

        private void BtnGuiBep_Click(object? sender, EventArgs e)
        {
            if (_maBanChon < 0) { MessageBox.Show("Vui lòng chọn bàn!"); return; }
            if (_gioMon.Count == 0) { MessageBox.Show("Giỏ món đang trống!"); return; }

            try
            {
                using var db = new AppDbContext();

                // Tạo đơn hàng mới
                var donHang = new DonHang
                {
                    MaBan = _maBanChon,
                    SoMon = _gioMon.Values.Sum(v => v.soLuong),
                    TrangThai = "Chờ bếp",
                    TongTien = _gioMon.Values.Sum(v => v.mon.GiaTien * v.soLuong),
                    ThoiGian = DateTime.Now
                };
                db.DonHangs.Add(donHang);
                db.SaveChanges();

                // Thêm chi tiết
                foreach (var kv in _gioMon)
                {
                    db.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        MaDonHang = donHang.MaDonHang,
                        MaMon = kv.Value.mon.MaMon,
                        SoLuong = kv.Value.soLuong,
                        DonGia = kv.Value.mon.GiaTien
                    });
                }

                // Cập nhật trạng thái bàn
                var ban = db.BanAns.Find(_maBanChon);
                if (ban != null) ban.TrangThai = "Có khách";
                db.SaveChanges();

                MessageBox.Show("Đã gửi bếp thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _gioMon.Clear();
                RefreshGio();
                LoadBanAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }
    }
}