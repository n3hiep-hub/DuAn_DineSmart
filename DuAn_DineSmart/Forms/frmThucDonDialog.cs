using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmThucDonDialog : Form
    {
        private readonly ThucDon? _mon;
        private TextBox txtTenMon = null!;
        private ComboBox cmbDanhMuc = null!;
        private ComboBox cmbTrangThai = null!;
        private NumericUpDown numGia = null!;
        private Button btnLuu = null!;
        private Button btnXoa = null!;
        private Button btnHuy = null!;

        public frmThucDonDialog(ThucDon? mon)
        {
            _mon = mon;
            BuildUI();
            if (mon != null)
            {
                Text = $"Sửa món – {mon.TenMon}";
                txtTenMon.Text = mon.TenMon;
                numGia.Value = mon.GiaTien;
                cmbDanhMuc.SelectedItem = mon.DanhMuc;
                cmbTrangThai.SelectedIndex = mon.TrangThai ? 0 : 1;
            }
        }

        private void BuildUI()
        {
            Text = "Thêm món mới";
            Size = new Size(380, 300);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            int y = 20;
            Label Lbl(string t) => new Label { Text = t, Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray };

            Controls.Add(Lbl("Tên món"));
            y += 20;
            txtTenMon = new TextBox { Location = new Point(20, y), Size = new Size(320, 28), Font = new Font("Segoe UI", 10) };
            Controls.Add(txtTenMon);
            y += 40;

            Controls.Add(new Label { Text = "Danh mục", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            Controls.Add(new Label { Text = "Giá tiền (đ)", Location = new Point(200, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            y += 20;
            cmbDanhMuc = new ComboBox { Location = new Point(20, y), Size = new Size(155, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbDanhMuc.Items.AddRange(new[] { "Khai vị", "Món chính", "Tráng miệng", "Đồ uống" });
            cmbDanhMuc.SelectedIndex = 0;
            numGia = new NumericUpDown { Location = new Point(200, y), Size = new Size(140, 28), Font = new Font("Segoe UI", 10), Minimum = 0, Maximum = 10000000, Increment = 1000, ThousandsSeparator = true };
            Controls.Add(cmbDanhMuc);
            Controls.Add(numGia);
            y += 45;

            Controls.Add(new Label { Text = "Trạng thái", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            y += 20;
            cmbTrangThai = new ComboBox { Location = new Point(20, y), Size = new Size(320, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbTrangThai.Items.AddRange(new[] { "Đang bán", "Tạm ngưng" });
            cmbTrangThai.SelectedIndex = 0;
            Controls.Add(cmbTrangThai);
            y += 45;

            btnLuu = new Button { Text = "Lưu món", Location = new Point(200, y), Size = new Size(140, 36), BackColor = Color.FromArgb(192, 57, 43), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), DialogResult = DialogResult.OK };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            btnXoa = new Button { Text = "Xóa", Location = new Point(20, y), Size = new Size(80, 36), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(163, 45, 45), Font = new Font("Segoe UI", 10), Visible = _mon != null };
            btnXoa.FlatAppearance.BorderColor = Color.FromArgb(240, 149, 149);
            btnXoa.Click += BtnXoa_Click;

            btnHuy = new Button { Text = "Hủy", Location = new Point(110, y), Size = new Size(80, 36), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), DialogResult = DialogResult.Cancel };
            Controls.AddRange(new Control[] { btnLuu, btnXoa, btnHuy });
            ClientSize = new Size(360, y + 55);
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            var tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenMon))
            {
                MessageBox.Show("Vui lòng nhập tên món!", "Thông báo");
                DialogResult = DialogResult.None;
                return;
            }

            using var db = new AppDbContext();
            bool tonTaiTen = db.ThucDons.Any(m => m.TenMon == tenMon && m.MaMon != (_mon?.MaMon ?? 0));
            if (tonTaiTen)
            {
                MessageBox.Show("Tên món đã tồn tại. Vui lòng nhập tên khác!", "Thông báo");
                DialogResult = DialogResult.None;
                return;
            }

            if (_mon == null)
            {
                db.ThucDons.Add(new ThucDon
                {
                    TenMon = tenMon,
                    DanhMuc = cmbDanhMuc.SelectedItem!.ToString()!,
                    GiaTien = numGia.Value,
                    TrangThai = cmbTrangThai.SelectedIndex == 0
                });
            }
            else
            {
                var m = db.ThucDons.Find(_mon.MaMon);
                if (m != null)
                {
                    m.TenMon = tenMon;
                    m.DanhMuc = cmbDanhMuc.SelectedItem!.ToString()!;
                    m.GiaTien = numGia.Value;
                    m.TrangThai = cmbTrangThai.SelectedIndex == 0;
                }
            }
            db.SaveChanges();
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show($"Xóa món \"{_mon?.TenMon}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using var db = new AppDbContext();
            var m = db.ThucDons.Find(_mon!.MaMon);
            if (m == null) return;

            bool daPhatSinhDon = db.ChiTietDonHangs.Any(c => c.MaMon == m.MaMon);
            if (daPhatSinhDon)
            {
                m.TrangThai = false;
                db.SaveChanges();
                MessageBox.Show("Món đã phát sinh đơn nên sẽ được chuyển sang 'Tạm ngưng' thay vì xóa.", "Thông báo");
            }
            else
            {
                db.ThucDons.Remove(m);
                db.SaveChanges();
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
