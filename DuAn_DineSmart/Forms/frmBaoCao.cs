using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao() { InitializeComponent(); }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            btnXem.Click += (s, ev) => LoadBaoCao();
            LoadBaoCao();
        }

        private void LoadBaoCao()
        {
            try
            {
                using var db = new AppDbContext();
                var tu = dtpTu.Value.Date;
                var den = dtpDen.Value.Date.AddDays(1);

                var dsHoanThanh = db.DonHangs
                    .Include(d => d.Ban)
                    .Where(d => d.TrangThai == "Hoàn thành"
                             && d.ThoiGian >= tu
                             && d.ThoiGian < den)
                    .OrderByDescending(d => d.ThoiGian)
                    .ToList();

                // Cập nhật cards
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

                // Render ListView
                lvDonHang.Items.Clear();
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

                if (soDon == 0)
                {
                    var empty = new ListViewItem("");
                    empty.SubItems.Add("Không có dữ liệu trong khoảng thời gian này");
                    empty.ForeColor = Color.Gray;
                    lvDonHang.Items.Add(empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }
    }
}