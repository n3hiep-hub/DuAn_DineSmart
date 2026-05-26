using ClosedXML.Excel;
using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBaoCao : Form
    {
        private List<DuAn_DineSmart.Models.DonHang> _dsHoanThanhHienTai = new();

        public frmBaoCao() { InitializeComponent(); }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            btnXem.Click += (s, ev) => LoadBaoCao();
            cmbLoai.SelectedIndexChanged += (s, ev) => LoadBaoCao();

            var btnExcel = new Button
            {
                Text = "Xuất Excel",
                Location = new Point(btnXem.Right + 12, btnXem.Top),
                Size = new Size(120, 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(39, 80, 10),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            btnExcel.FlatAppearance.BorderSize = 0;
            btnExcel.Click += BtnExcel_Click;
            pnlFilter.Controls.Add(btnExcel);

            LoadBaoCao();
        }

        private void LoadBaoCao()
        {
            try
            {
                using var db = new AppDbContext();
                var tu = dtpTu.Value.Date;
                var den = dtpDen.Value.Date.AddDays(1);

                if (tu >= den)
                {
                    MessageBox.Show("Khoảng thời gian không hợp lệ (Từ ngày phải <= Đến ngày).", "Thông báo");
                    return;
                }

                var dsHoanThanh = db.DonHangs
                    .Include(d => d.Ban)
                    .Where(d => d.TrangThai == TrangThaiDonHang.HoanThanh
                             && d.ThoiGian >= tu
                             && d.ThoiGian < den)
                    .OrderByDescending(d => d.ThoiGian)
                    .ToList();

                _dsHoanThanhHienTai = dsHoanThanh;

                decimal tongDT = dsHoanThanh.Sum(d => d.TongTien);
                int soDon = dsHoanThanh.Count;
                decimal tbDon = soDon > 0 ? tongDT / soDon : 0;
                decimal homNay = dsHoanThanh
                    .Where(d => d.ThoiGian.Date == DateTime.Today)
                    .Sum(d => d.TongTien);

                lblDTVal.Text = tongDT.ToString("N0") + "đ";
                lblDonVal.Text = soDon.ToString();
                lblTBVal.Text = tbDon.ToString("N0") + "đ";
                lblNgayVal.Text = homNay.ToString("N0") + "đ";

                lvDonHang.Items.Clear();
                if (cmbLoai.SelectedIndex == 1)
                {
                    RenderTheoThang(dsHoanThanh);
                }
                else
                {
                    RenderTheoNgay(dsHoanThanh);
                }

                if (soDon == 0)
                {
                    var empty = new ListViewItem("-");
                    empty.SubItems.Add("Không có dữ liệu trong khoảng thời gian này");
                    empty.SubItems.Add("-");
                    empty.SubItems.Add("-");
                    empty.SubItems.Add("0đ");
                    empty.SubItems.Add("-");
                    empty.ForeColor = Color.Gray;
                    lvDonHang.Items.Add(empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void RenderTheoNgay(List<DuAn_DineSmart.Models.DonHang> dsHoanThanh)
        {
            int stt = 1;
            foreach (var dh in dsHoanThanh)
            {
                var item = new ListViewItem(stt++.ToString());
                item.SubItems.Add(dh.ThoiGian.ToString("dd/MM HH:mm"));
                item.SubItems.Add(dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}");
                item.SubItems.Add($"{dh.SoMon} món");
                item.SubItems.Add(dh.TongTien.ToString("N0") + "đ");
                item.SubItems.Add(dh.TrangThai);
                item.ForeColor = Color.FromArgb(39, 80, 10);
                lvDonHang.Items.Add(item);
            }
        }

        private void RenderTheoThang(List<DuAn_DineSmart.Models.DonHang> dsHoanThanh)
        {
            int stt = 1;
            var theoThang = dsHoanThanh
                .GroupBy(d => new { d.ThoiGian.Year, d.ThoiGian.Month })
                .OrderByDescending(g => g.Key.Year)
                .ThenByDescending(g => g.Key.Month)
                .ToList();

            foreach (var g in theoThang)
            {
                var tongTien = g.Sum(x => x.TongTien);
                var tongMon = g.Sum(x => x.SoMon);
                var soDon = g.Count();

                var item = new ListViewItem(stt++.ToString());
                item.SubItems.Add($"Tháng {g.Key.Month:00}/{g.Key.Year}");
                item.SubItems.Add("Tổng hợp");
                item.SubItems.Add($"{tongMon} món");
                item.SubItems.Add(tongTien.ToString("N0") + "đ");
                item.SubItems.Add($"{soDon} đơn");
                item.ForeColor = Color.FromArgb(12, 68, 124);
                lvDonHang.Items.Add(item);
            }
        }

        private void BtnExcel_Click(object? sender, EventArgs e)
        {
            if (_dsHoanThanhHienTai.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = $"BaoCao_DineSmart_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Báo cáo doanh thu");

                // Header
                ws.Cell(1, 1).Value = "DineSmart – Báo cáo doanh thu";
                ws.Range(1, 1, 1, 6).Merge();
                var titleStyle = ws.Cell(1, 1).Style;
                titleStyle.Font.Bold = true;
                titleStyle.Font.FontSize = 14;
                titleStyle.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                ws.Cell(2, 1).Value = $"Từ {dtpTu.Value:dd/MM/yyyy} đến {dtpDen.Value:dd/MM/yyyy}";
                ws.Range(2, 1, 2, 6).Merge();

                // Thống kê tóm tắt
                ws.Cell(4, 1).Value = "Tổng doanh thu";
                ws.Cell(4, 2).Value = _dsHoanThanhHienTai.Sum(d => d.TongTien);
                ws.Cell(4, 2).Style.NumberFormat.Format = "#,##0";

                ws.Cell(5, 1).Value = "Số đơn hoàn thành";
                ws.Cell(5, 2).Value = _dsHoanThanhHienTai.Count;

                ws.Cell(6, 1).Value = "Giá trị trung bình / đơn";
                decimal tb = _dsHoanThanhHienTai.Count > 0
                    ? _dsHoanThanhHienTai.Sum(d => d.TongTien) / _dsHoanThanhHienTai.Count
                    : 0;
                ws.Cell(6, 2).Value = tb;
                ws.Cell(6, 2).Style.NumberFormat.Format = "#,##0";

                // Cột tiêu đề
                int row = 8;
                string[] headers = { "STT", "Thời gian", "Bàn", "Số món", "Tổng tiền (đ)", "Trạng thái" };
                for (int col = 1; col <= headers.Length; col++)
                {
                    ws.Cell(row, col).Value = headers[col - 1];
                    ws.Cell(row, col).Style.Font.Bold = true;
                    ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.FromArgb(192, 57, 43);
                    ws.Cell(row, col).Style.Font.FontColor = XLColor.White;
                    ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Dữ liệu
                int stt = 1;
                foreach (var dh in _dsHoanThanhHienTai)
                {
                    row++;
                    ws.Cell(row, 1).Value = stt++;
                    ws.Cell(row, 2).Value = dh.ThoiGian.ToString("dd/MM/yyyy HH:mm");
                    ws.Cell(row, 3).Value = dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}";
                    ws.Cell(row, 4).Value = dh.SoMon;
                    ws.Cell(row, 5).Value = dh.TongTien;
                    ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0";
                    ws.Cell(row, 6).Value = dh.TrangThai;

                    if (stt % 2 == 0)
                        ws.Row(row).Style.Fill.BackgroundColor = XLColor.FromArgb(245, 245, 245);
                }

                ws.Columns().AdjustToContents();
                wb.SaveAs(sfd.FileName);

                if (MessageBox.Show("Xuất Excel thành công!\nMở file ngay?", "Thành công",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = sfd.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel:\n{ex.Message}", "Lỗi");
            }
        }
    }
}
