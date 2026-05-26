using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao() { InitializeComponent(); }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            btnXem.Click += (s, ev) => LoadBaoCao();
            cmbLoai.SelectedIndexChanged += (s, ev) => LoadBaoCao();
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
    }
}
