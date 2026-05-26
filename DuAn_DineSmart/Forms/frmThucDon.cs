using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmThucDon : Form
    {
        private List<ThucDon> _dsMonAn = new();

        public frmThucDon() { InitializeComponent(); }

        private void frmThucDon_Load(object sender, EventArgs e)
        {
            LoadThucDon();
            txtSearch.TextChanged += (s, ev) => FilterAndRender();
            cmbDanhMuc.SelectedIndexChanged += (s, ev) => FilterAndRender();
            btnThem.Click += (s, ev) => ShowDialog(null);
            lvThucDon.DoubleClick += LvThucDon_DoubleClick;
        }

        private void LoadThucDon()
        {
            using var db = new AppDbContext();
            _dsMonAn = db.ThucDons.OrderBy(m => m.DanhMuc).ThenBy(m => m.TenMon).ToList();
            FilterAndRender();
        }

        private void FilterAndRender()
        {
            var ds = _dsMonAn.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                ds = ds.Where(m => m.TenMon.Contains(
                    txtSearch.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (cmbDanhMuc.SelectedIndex > 0)
                ds = ds.Where(m => m.DanhMuc == cmbDanhMuc.SelectedItem?.ToString());

            RenderList(ds.ToList());
        }

        private void RenderList(List<ThucDon> ds)
        {
            lvThucDon.Items.Clear();
            int stt = 1;
            foreach (var m in ds)
            {
                var item = new ListViewItem(stt++.ToString());
                item.SubItems.Add(m.TenMon);
                item.SubItems.Add(m.DanhMuc);
                item.SubItems.Add(m.GiaTien.ToString("N0") + "đ");
                item.SubItems.Add(m.TrangThai ? "Đang bán" : "Tạm ngưng");
                item.SubItems.Add("Sửa   |   Xóa");
                item.Tag = m;
                item.ForeColor = m.TrangThai
                    ? Color.FromArgb(39, 80, 10)
                    : Color.Gray;
                lvThucDon.Items.Add(item);
            }
            lblCount.Text = $"  Hiển thị {ds.Count} / {_dsMonAn.Count} món";
        }

        private void LvThucDon_DoubleClick(object? sender, EventArgs e)
        {
            if (lvThucDon.SelectedItems.Count > 0
                && lvThucDon.SelectedItems[0].Tag is ThucDon mon)
                ShowDialog(mon);
        }

        private void ShowDialog(ThucDon? mon)
        {
            using var dlg = new frmThucDonDialog(mon);
            if (dlg.ShowDialog() == DialogResult.OK)
                LoadThucDon();
        }
    }
}