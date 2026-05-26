using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmNhanVienDialog : Form
    {
        private readonly NguoiDung? _nv;
        private TextBox txtTen = null!;
        private TextBox txtMK = null!;
        private ComboBox cmbVaiTro = null!;
        private ComboBox cmbTrangThai = null!;
        private Button btnLuu = null!;
        private Button btnXoa = null!;
        private Button btnHuy = null!;

        public frmNhanVienDialog(NguoiDung? nv)
        {
            _nv = nv;
            BuildUI();
            if (nv != null)
            {
                Text = $"Sửa – {nv.TenDangNhap}";
                txtTen.Text = nv.TenDangNhap;
                txtMK.PlaceholderText = "Để trống nếu không đổi mật khẩu";
                cmbVaiTro.SelectedItem = nv.VaiTro;
                cmbTrangThai.SelectedIndex = nv.TrangThai ? 0 : 1;
            }
        }

        private void BuildUI()
        {
            Text = "Thêm nhân viên mới";
            Size = new Size(400, 310);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            BackColor = Color.White;

            int y = 20;

            Controls.Add(new Label { Text = "Tên đăng nhập", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            y += 20;
            txtTen = new TextBox { Location = new Point(20, y), Size = new Size(340, 28), Font = new Font("Segoe UI", 10) };
            Controls.Add(txtTen); y += 40;

            Controls.Add(new Label { Text = "Mật khẩu", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            y += 20;
            txtMK = new TextBox { Location = new Point(20, y), Size = new Size(340, 28), Font = new Font("Segoe UI", 10), UseSystemPasswordChar = true };
            Controls.Add(txtMK); y += 40;

            Controls.Add(new Label { Text = "Vai trò", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            Controls.Add(new Label { Text = "Trạng thái", Location = new Point(200, y), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray });
            y += 20;

            cmbVaiTro = new ComboBox { Location = new Point(20, y), Size = new Size(165, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbVaiTro.Items.AddRange(new[] { "Nhân viên", "Bếp", "Thu ngân", "Quản lý" });
            cmbVaiTro.SelectedIndex = 0;

            cmbTrangThai = new ComboBox { Location = new Point(200, y), Size = new Size(160, 28), DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbTrangThai.Items.AddRange(new[] { "Hoạt động", "Tạm khóa" });
            cmbTrangThai.SelectedIndex = 0;
            Controls.Add(cmbVaiTro); Controls.Add(cmbTrangThai); y += 48;

            btnLuu = new Button { Text = "Lưu", Location = new Point(220, y), Size = new Size(140, 36), BackColor = Color.FromArgb(192, 57, 43), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), DialogResult = DialogResult.OK };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            btnXoa = new Button { Text = "Xóa", Location = new Point(20, y), Size = new Size(80, 36), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(163, 45, 45), Font = new Font("Segoe UI", 10), Visible = _nv != null && _nv.VaiTro != "Quản lý" };
            btnXoa.FlatAppearance.BorderColor = Color.FromArgb(240, 149, 149);
            btnXoa.Click += BtnXoa_Click;

            btnHuy = new Button { Text = "Hủy", Location = new Point(110, y), Size = new Size(100, 36), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10), DialogResult = DialogResult.Cancel };
            Controls.AddRange(new Control[] { btnLuu, btnXoa, btnHuy });
            ClientSize = new Size(380, y + 55);
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo");
                DialogResult = DialogResult.None; return;
            }
            if (_nv == null && string.IsNullOrWhiteSpace(txtMK.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo");
                DialogResult = DialogResult.None; return;
            }

            using var db = new AppDbContext();
            if (_nv == null)
            {
                // Kiểm tra tên đăng nhập trùng
                if (db.NguoiDungs.Any(n => n.TenDangNhap == txtTen.Text.Trim()))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo");
                    DialogResult = DialogResult.None; return;
                }
                db.NguoiDungs.Add(new NguoiDung
                {
                    TenDangNhap = txtTen.Text.Trim(),
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(txtMK.Text),
                    VaiTro = cmbVaiTro.SelectedItem!.ToString()!,
                    TrangThai = cmbTrangThai.SelectedIndex == 0
                });
            }
            else
            {
                var n = db.NguoiDungs.Find(_nv.MaNguoiDung);
                if (n != null)
                {
                    n.TenDangNhap = txtTen.Text.Trim();
                    n.VaiTro = cmbVaiTro.SelectedItem!.ToString()!;
                    n.TrangThai = cmbTrangThai.SelectedIndex == 0;
                    if (!string.IsNullOrWhiteSpace(txtMK.Text))
                        n.MatKhau = BCrypt.Net.BCrypt.HashPassword(txtMK.Text);
                }
            }
            db.SaveChanges();
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show($"Xóa nhân viên \"{_nv?.TenDangNhap}\"?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using var db = new AppDbContext();
                var n = db.NguoiDungs.Find(_nv!.MaNguoiDung);
                if (n != null) db.NguoiDungs.Remove(n);
                db.SaveChanges();
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
