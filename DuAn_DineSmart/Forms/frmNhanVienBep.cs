using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;
using Microsoft.EntityFrameworkCore;
using System.Media;

namespace DuAn_DineSmart.Forms
{
    public partial class frmNhanVienBep : Form
    {
        private int _prevChoPhucVuCount = -1;

        public frmNhanVienBep() { InitializeComponent(); }

        private void frmNhanVienBep_Load(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            try
            {
                using var db = new AppDbContext();

                var dsChoPhucVu = db.DonHangs
                    .Include(d => d.Ban)
                    .Where(d => d.TrangThai == TrangThaiDonHang.ChoPhucVu)
                    .OrderBy(d => d.ThoiGian)
                    .ToList();

                int daPhucVu = db.DonHangs
                    .Count(d => d.TrangThai == TrangThaiDonHang.DaPhucVu
                             && d.ThoiGian.Date == DateTime.Today);

                int soMon = dsChoPhucVu.Count;
                if (_prevChoPhucVuCount >= 0 && soMon > _prevChoPhucVuCount)
                {
                    // Có món mới từ bếp — phát tiếng + nháy đỏ thẻ "Chờ mang ra bàn"
                    SystemSounds.Beep.Play();
                    FlashCard(cardCho, Color.FromArgb(220, 50, 50), Color.White);
                }
                _prevChoPhucVuCount = soMon;

                lblChoVal.Text = soMon.ToString();
                lblXongVal.Text = daPhucVu.ToString();

                flpChoPhucVu.Controls.Clear();

                foreach (var dh in dsChoPhucVu)
                {
                    var chiTiet = db.ChiTietDonHangs
                        .Where(c => c.MaDonHang == dh.MaDonHang)
                        .Include(c => c.Mon)
                        .ToList();

                    int phut = (int)(DateTime.Now - dh.ThoiGian).TotalMinutes;

                    var card = new Panel
                    {
                        Size = new Size(280, 160 + chiTiet.Count * 22),
                        Margin = new Padding(8),
                        BackColor = Color.FromArgb(230, 241, 251)
                    };

                    card.Paint += (s, e) =>
                        ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                            Color.FromArgb(55, 138, 221), ButtonBorderStyle.Solid);

                    int y = 12;

                    var lblBan = new Label
                    {
                        Text = dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}",
                        Font = new Font("Segoe UI", 13, FontStyle.Bold),
                        ForeColor = Color.FromArgb(12, 68, 124),
                        Location = new Point(14, y),
                        AutoSize = true
                    };
                    card.Controls.Add(lblBan);
                    y += 28;

                    var lblTG = new Label
                    {
                        Text = $"Bếp xong {phut} phút trước",
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.FromArgb(12, 68, 124),
                        Location = new Point(14, y),
                        AutoSize = true
                    };
                    card.Controls.Add(lblTG);
                    y += 26;

                    foreach (var ct in chiTiet)
                    {
                        var lblMon = new Label
                        {
                            Text = $"• {ct.Mon?.TenMon ?? "Món"} ×{ct.SoLuong}",
                            Font = new Font("Segoe UI", 10),
                            ForeColor = Color.FromArgb(12, 68, 124),
                            Location = new Point(14, y),
                            AutoSize = true
                        };
                        card.Controls.Add(lblMon);
                        y += 22;
                    }

                    y += 8;

                    var btn = new Button
                    {
                        Text = "✓ Đã mang ra bàn",
                        Location = new Point(12, y),
                        Size = new Size(254, 36),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        BackColor = Color.FromArgb(12, 68, 124),
                        ForeColor = Color.White,
                        Tag = dh.MaDonHang
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += BtnDaMang_Click;
                    card.Controls.Add(btn);
                    card.Size = new Size(280, y + 50);

                    flpChoPhucVu.Controls.Add(card);
                }

                if (dsChoPhucVu.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "Không có món nào cần mang ra bàn",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    flpChoPhucVu.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }

        private void BtnDaMang_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not int maDH) return;
            using var db = new AppDbContext();
            var dh = db.DonHangs.Find(maDH);
            if (dh != null)
            {
                dh.TrangThai = TrangThaiDonHang.DaPhucVu;
                db.SaveChanges();
            }
            LoadDonHang();
        }

        private static void FlashCard(Panel card, Color flashBg, Color flashFg)
        {
            Color origBg = card.BackColor;
            Color origFg = card.Controls.OfType<Label>().FirstOrDefault()?.ForeColor ?? Color.Black;
            card.BackColor = flashBg;
            foreach (Control c in card.Controls) if (c is Label l) l.ForeColor = flashFg;

            var t = new System.Windows.Forms.Timer { Interval = 1200 };
            t.Tick += (s, e) =>
            {
                card.BackColor = origBg;
                foreach (Control c in card.Controls) if (c is Label l) l.ForeColor = origFg;
                t.Stop();
                t.Dispose();
            };
            t.Start();
        }
    }
}
