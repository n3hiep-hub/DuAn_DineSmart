using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;
using System.Drawing.Printing;
using System.Text;

namespace DuAn_DineSmart.Forms
{
    public partial class frmHoaDon : Form
    {
        private List<BanAn> _dsBan = new();
        private DonHang? _donHangHienTai;
        private string _phuongThuc = "Tiền mặt";
        private readonly NguoiDung _nguoiDung;
        private decimal _tamTinh;
        private decimal _giamGia;
        private decimal _tongThanhToan;
        private readonly PrintDocument _printDocument = new();
        private Button? _btnInBill;
        private Button? _btnExport;

        public frmHoaDon(NguoiDung nguoiDung)
        {
            InitializeComponent();
            _nguoiDung = nguoiDung;
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadBanCoKhach();
            lbBan.SelectedIndexChanged += LbBan_Changed;
            btnThanhToan.Click += BtnThanhToan_Click;

            foreach (Button btn in new[] { btnTienMat, btnThe, btnQR })
                btn.Click += PayBtn_Click;

            BuildExtraActionButtons();
        }

        private void BuildExtraActionButtons()
        {
            if (_btnInBill != null && _btnExport != null) return;

            _btnInBill = new Button
            {
                Text = "In hóa đơn",
                Location = new Point(btnThanhToan.Right + 12, btnThanhToan.Top),
                Size = new Size(140, 44),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(12, 68, 124),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            _btnInBill.FlatAppearance.BorderColor = Color.FromArgb(55, 138, 221);
            _btnInBill.Click += BtnInBill_Click;

            _btnExport = new Button
            {
                Text = "Export CSV",
                Location = new Point(_btnInBill.Right + 10, btnThanhToan.Top),
                Size = new Size(140, 44),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(39, 80, 10),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            _btnExport.FlatAppearance.BorderColor = Color.FromArgb(99, 153, 34);
            _btnExport.Click += BtnExport_Click;

            pnlRight.Controls.Add(_btnInBill);
            pnlRight.Controls.Add(_btnExport);
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
                    .Where(d => d.MaBan == b.MaBan && d.TrangThai != TrangThaiDonHang.HoanThanh)
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
                .Where(d => d.MaBan == ban.MaBan && d.TrangThai != TrangThaiDonHang.HoanThanh)
                .OrderByDescending(d => d.ThoiGian)
                .FirstOrDefault();

            lblBanInfo.Text = $"{ban.TenBan}  |  {DateTime.Now:dd/MM/yyyy HH:mm}";
            lblNVInfo.Text = $"Thu ngân: {_nguoiDung.TenDangNhap}";

            lvChiTiet.Items.Clear();
            _tamTinh = 0;

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
                    _tamTinh += thanh;
                    var item = new ListViewItem(mon.TenMon);
                    item.SubItems.Add(ct.SoLuong.ToString());
                    item.SubItems.Add(ct.DonGia.ToString("N0") + "đ");
                    item.SubItems.Add(thanh.ToString("N0") + "đ");
                    lvChiTiet.Items.Add(item);
                }
            }

            _giamGia = TinhGiamGiaTheoTong(_tamTinh);
            _tongThanhToan = _tamTinh - _giamGia;
            HienThiTongTien();
        }

        private static decimal TinhGiamGiaTheoTong(decimal tamTinh)
        {
            if (tamTinh >= 2_000_000) return tamTinh * 0.1m;
            if (tamTinh >= 1_000_000) return tamTinh * 0.05m;
            return 0;
        }

        private void HienThiTongTien()
        {
            lblTamTinh.Text = $"Tạm tính: {_tamTinh:N0}đ";
            lblGiamGia.Text = $"Giảm giá: {_giamGia:N0}đ";
            lblTongTT.Text = $"Tổng thanh toán: {_tongThanhToan:N0}đ";
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

        private void BtnInBill_Click(object? sender, EventArgs e)
        {
            if (_donHangHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để in hóa đơn.", "Thông báo");
                return;
            }

            using var preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 900,
                Height = 700
            };
            preview.ShowDialog();
        }

        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            string content = BuildBillText();
            using var font = new Font("Consolas", 10);
            e.Graphics.DrawString(content, font, Brushes.Black, new RectangleF(20, 20, e.MarginBounds.Width, e.MarginBounds.Height));
        }

        private void BtnExport_Click(object? sender, EventArgs e)
        {
            if (_donHangHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để export.", "Thông báo");
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"HoaDon_{_donHangHienTai.MaDonHang}_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            File.WriteAllText(sfd.FileName, BuildBillCsv(), Encoding.UTF8);
            MessageBox.Show("Đã export hóa đơn CSV thành công.", "Thành công");
        }

        private string BuildBillText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("DineSmart - HOA DON THANH TOAN");
            sb.AppendLine($"Ngay: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine(lblBanInfo.Text);
            sb.AppendLine(lblNVInfo.Text);
            sb.AppendLine(new string('-', 42));
            sb.AppendLine("Mon                 SL      Don gia    Thanh tien");

            foreach (ListViewItem item in lvChiTiet.Items)
            {
                string tenMon = item.SubItems[0].Text;
                string sl = item.SubItems[1].Text;
                string donGia = item.SubItems[2].Text;
                string thanhTien = item.SubItems[3].Text;
                sb.AppendLine($"{tenMon.PadRight(18).Substring(0, Math.Min(18, tenMon.Length)).PadRight(18)} {sl.PadLeft(2)} {donGia.PadLeft(10)} {thanhTien.PadLeft(11)}");
            }

            sb.AppendLine(new string('-', 42));
            sb.AppendLine(lblTamTinh.Text);
            sb.AppendLine(lblGiamGia.Text);
            sb.AppendLine(lblTongTT.Text);
            sb.AppendLine($"Phuong thuc: {_phuongThuc}");
            return sb.ToString();
        }

        private string BuildBillCsv()
        {
            var sb = new StringBuilder();
            sb.AppendLine("TenMon,SoLuong,DonGia,ThanhTien");
            foreach (ListViewItem item in lvChiTiet.Items)
            {
                sb.AppendLine($"\"{item.SubItems[0].Text}\",{item.SubItems[1].Text},\"{item.SubItems[2].Text}\",\"{item.SubItems[3].Text}\"");
            }
            sb.AppendLine($"TAM_TINH,,,\"{_tamTinh:N0}đ\"");
            sb.AppendLine($"GIAM_GIA,,,\"{_giamGia:N0}đ\"");
            sb.AppendLine($"TONG_THANH_TOAN,,,\"{_tongThanhToan:N0}đ\"");
            sb.AppendLine($"PHUONG_THUC,,,\"{_phuongThuc}\"");
            return sb.ToString();
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

                var dh = db.DonHangs.Find(_donHangHienTai.MaDonHang);
                if (dh != null)
                {
                    dh.TrangThai = TrangThaiDonHang.HoanThanh;
                    dh.TongTien = _tongThanhToan;
                }

                var ban = db.BanAns.Find(_donHangHienTai.MaBan);
                if (ban != null) ban.TrangThai = "Trống";

                db.SaveChanges();

                MessageBox.Show("Thanh toán thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _donHangHienTai = null;
                lvChiTiet.Items.Clear();
                _tamTinh = 0;
                _giamGia = 0;
                _tongThanhToan = 0;
                HienThiTongTien();
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
