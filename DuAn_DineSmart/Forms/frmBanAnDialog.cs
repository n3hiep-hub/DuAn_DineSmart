using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBanAnDialog : Form
    {
        private readonly BanAn? _ban;
        private TextBox txtTenBan = null!;
        private ComboBox cmbTrangThai = null!;
        private Button btnLuu = null!;
        private Button btnXoa = null!;

        public frmBanAnDialog(BanAn? ban)
        {
            _ban = ban;
            BuildUI();
            if (ban != null)
            {
                txtTenBan.Text = ban.TenBan;
                cmbTrangThai.SelectedItem = ban.TrangThai;
            }
        }

        private void BuildUI()
        {
            Text = _ban == null ? "Thêm bàn mới" : $"Sửa – {_ban.TenBan}";
            Size = new Size(320, 240);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lblTen = new Label { Text = "Tên bàn", Location = new Point(20, 20), AutoSize = true };
            txtTenBan = new TextBox { Location = new Point(20, 42), Size = new Size(260, 28) };

            var lblTT = new Label { Text = "Trạng thái", Location = new Point(20, 82), AutoSize = true };
            cmbTrangThai = new ComboBox
            {
                Location = new Point(20, 104),
                Size = new Size(260, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTrangThai.Items.AddRange(new[] { "Trống", "Có khách", "Đặt trước" });
            cmbTrangThai.SelectedIndex = 0;

            btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(160, 150),
                Size = new Size(120, 34),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Click += BtnLuu_Click;

            btnXoa = new Button
            {
                Text = "Xóa bàn",
                Location = new Point(20, 150),
                Size = new Size(100, 34),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(163, 45, 45),
                FlatStyle = FlatStyle.Flat,
                Visible = _ban != null
            };
            btnXoa.FlatAppearance.BorderColor = Color.FromArgb(226, 75, 74);
            btnXoa.Click += BtnXoa_Click;

            Controls.AddRange(new Control[] { lblTen, txtTenBan, lblTT, cmbTrangThai, btnLuu, btnXoa });
        }

        private void BtnLuu_Click(object? sender, EventArgs e)
        {
            var tenBan = txtTenBan.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenBan))
            {
                MessageBox.Show("Vui lòng nhập tên bàn!", "Thông báo");
                DialogResult = DialogResult.None;
                return;
            }

            using var db = new AppDbContext();
            bool tonTaiTen = db.BanAns.Any(b => b.TenBan == tenBan && b.MaBan != (_ban?.MaBan ?? 0));
            if (tonTaiTen)
            {
                MessageBox.Show("Tên bàn đã tồn tại. Vui lòng nhập tên khác!", "Thông báo");
                DialogResult = DialogResult.None;
                return;
            }

            if (_ban == null)
            {
                db.BanAns.Add(new BanAn
                {
                    TenBan = tenBan,
                    TrangThai = cmbTrangThai.SelectedItem!.ToString()!
                });
            }
            else
            {
                var b = db.BanAns.Find(_ban.MaBan);
                if (b != null)
                {
                    b.TenBan = tenBan;
                    b.TrangThai = cmbTrangThai.SelectedItem!.ToString()!;
                }
            }
            db.SaveChanges();
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show($"Xóa {_ban?.TenBan}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using var db = new AppDbContext();
            var b = db.BanAns.Find(_ban!.MaBan);
            if (b == null) return;

            bool dangCoDon = db.DonHangs.Any(d => d.MaBan == b.MaBan && d.TrangThai != BLL.TrangThaiDonHang.HoanThanh);
            if (dangCoDon)
            {
                MessageBox.Show("Bàn đang có đơn chưa hoàn thành, không thể xóa.", "Thông báo");
                return;
            }

            db.BanAns.Remove(b);
            db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
