using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmNhanVien : Form
    {
        private List<NguoiDung> _dsNV = new();

        public frmNhanVien() { InitializeComponent(); }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            txtSearch.TextChanged += (s, ev) => FilterAndRender();
            cmbVaiTro.SelectedIndexChanged += (s, ev) => FilterAndRender();
            btnThem.Click += (s, ev) => ShowDialog(null);
            lvNhanVien.DoubleClick += (s, ev) =>
            {
                if (lvNhanVien.SelectedItems.Count > 0
                    && lvNhanVien.SelectedItems[0].Tag is NguoiDung nv)
                    ShowDialog(nv);
            };
        }

        private void LoadNhanVien()
        {
            using var db = new AppDbContext();
            _dsNV = db.NguoiDungs.OrderBy(n => n.VaiTro).ThenBy(n => n.TenDangNhap).ToList();
            UpdateStats();
            FilterAndRender();
        }

        private void UpdateStats()
        {
            lblTongVal.Text = _dsNV.Count.ToString();
            lblQLVal.Text = _dsNV.Count(n => n.VaiTro == "Quản lý").ToString();
            lblNVVal.Text = _dsNV.Count(n => n.VaiTro == "Nhân viên" || n.VaiTro == "Bếp").ToString();
            lblActVal.Text = _dsNV.Count(n => n.TrangThai).ToString();
        }

        private void FilterAndRender()
        {
            var ds = _dsNV.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                ds = ds.Where(n => n.TenDangNhap.Contains(
                    txtSearch.Text.Trim(), StringComparison.OrdinalIgnoreCase));
            if (cmbVaiTro.SelectedIndex > 0)
                ds = ds.Where(n => n.VaiTro == cmbVaiTro.SelectedItem?.ToString());
            RenderList(ds.ToList());
        }

        private void RenderList(List<NguoiDung> ds)
        {
            lvNhanVien.Items.Clear();
            int stt = 1;
            foreach (var nv in ds)
            {
                var item = new ListViewItem(stt++.ToString());
                item.SubItems.Add(nv.TenDangNhap);
                item.SubItems.Add(nv.VaiTro);
                item.SubItems.Add(nv.TrangThai ? "Hoạt động" : "Tạm khóa");
                item.SubItems.Add("Sửa   |   Đổi MK" + (nv.VaiTro != "Quản lý" ? "   |   Xóa" : ""));
                item.Tag = nv;

                item.ForeColor = nv.VaiTro switch
                {
                    "Quản lý" => Color.FromArgb(163, 45, 45),
                    "Nhân viên" => Color.FromArgb(12, 68, 124),
                    "Bếp" => Color.FromArgb(99, 56, 6),
                    _ => Color.FromArgb(39, 80, 10)
                };
                lvNhanVien.Items.Add(item);
            }
            lblCount.Text = $"  Hiển thị {ds.Count} / {_dsNV.Count} nhân viên";
        }

        private void ShowDialog(NguoiDung? nv)
        {
            using var dlg = new frmNhanVienDialog(nv);
            if (dlg.ShowDialog() == DialogResult.OK)
                LoadNhanVien();
        }
    }
}