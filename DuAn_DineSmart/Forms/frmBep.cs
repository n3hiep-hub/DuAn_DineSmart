using DuAn_DineSmart.BLL;
using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;
using DineSmart.Services.Sync;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBep : Form
    {
        private readonly PollingSyncService _polling = new(5000);

        public frmBep() { InitializeComponent(); }

        private void frmBep_Load(object sender, EventArgs e)
        {
            LoadDonHang();
            _polling.Tick += Polling_Tick;
            _polling.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _polling.Tick -= Polling_Tick;
            _polling.Dispose();
            base.OnFormClosed(e);
        }

        private void Polling_Tick(object? sender, EventArgs e)
        {
            if (IsDisposed || !IsHandleCreated) return;
            BeginInvoke(new Action(() =>
            {
                lblTime.Text = DateTime.Now.ToString("HH:mm – dddd, dd/MM/yyyy");
                LoadDonHang();
            }));
        }

        private void LoadDonHang()
        {
            try
            {
                using var db = new AppDbContext();

                var dsChoBep = db.DonHangs
                    .Include(d => d.Ban)
                    .Where(d => d.TrangThai == TrangThaiDonHang.ChoBep)
                    .OrderBy(d => d.ThoiGian)
                    .ToList();

                var dsDangLam = db.DonHangs
                    .Include(d => d.Ban)
                    .Where(d => d.TrangThai == TrangThaiDonHang.DangLam)
                    .OrderBy(d => d.ThoiGian)
                    .ToList();

                int xong = db.DonHangs
                    .Count(d => d.TrangThai == TrangThaiDonHang.HoanThanh
                             && d.ThoiGian.Date == DateTime.Today);

                lblChoVal.Text = dsChoBep.Count.ToString();
                lblLamVal.Text = dsDangLam.Count.ToString();
                lblXongVal.Text = xong.ToString();

                RenderCards(flpChoBep, dsChoBep, false, db);
                RenderCards(flpDangLam, dsDangLam, true, db);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderCards(FlowLayoutPanel flp, List<DonHang> ds,
                                  bool isDangLam, AppDbContext db)
        {
            flp.Controls.Clear();

            foreach (var dh in ds)
            {
                var chiTiet = db.ChiTietDonHangs
                    .Where(c => c.MaDonHang == dh.MaDonHang)
                    .Include(c => c.Mon)
                    .ToList();

                int phutCho = (int)(DateTime.Now - dh.ThoiGian).TotalMinutes;
                bool urgent = !isDangLam && phutCho > 10;

                int cardHeight = 160 + chiTiet.Count * 22;
                var card = new Panel
                {
                    Size = new Size(470, cardHeight),
                    Margin = new Padding(0, 0, 0, 10),
                    BackColor = Color.White,
                    Tag = dh
                };

                card.Paint += (s, e) =>
                {
                    if (urgent)
                        ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                            Color.FromArgb(226, 75, 74), ButtonBorderStyle.Solid);
                    else if (isDangLam)
                        ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                            Color.FromArgb(55, 138, 221), ButtonBorderStyle.Solid);
                    else
                        ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                            Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);
                };

                int y = 12;
                var lblBan = new Label
                {
                    Text = dh.Ban?.TenBan ?? $"Bàn {dh.MaBan}",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Location = new Point(14, y),
                    AutoSize = true
                };
                card.Controls.Add(lblBan);
                y += 26;

                string tgText = isDangLam
                    ? $"Bắt đầu {phutCho} phút trước"
                    : urgent ? $"⚠ Đã chờ {phutCho} phút"
                    : $"{phutCho} phút trước";

                var lblTG = new Label
                {
                    Text = tgText,
                    Font = new Font("Segoe UI", 9),
                    ForeColor = urgent ? Color.FromArgb(163, 45, 45) : Color.Gray,
                    Location = new Point(14, y),
                    AutoSize = true
                };
                card.Controls.Add(lblTG);
                y += 28;

                foreach (var ct in chiTiet)
                {
                    var lblMon = new Label
                    {
                        Text = $"• {ct.Mon?.TenMon ?? "Món"} ×{ct.SoLuong}",
                        Font = new Font("Segoe UI", 10),
                        ForeColor = Color.FromArgb(60, 60, 60),
                        Location = new Point(14, y),
                        AutoSize = true
                    };
                    card.Controls.Add(lblMon);
                    y += 22;
                }

                y += 8;
                var btn = new Button
                {
                    Text = isDangLam ? "✓ Hoàn thành – Gọi phục vụ" : "▶ Bắt đầu làm",
                    Location = new Point(12, y),
                    Size = new Size(444, 36),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Tag = dh.MaDonHang
                };
                btn.FlatAppearance.BorderSize = 0;

                if (isDangLam)
                {
                    btn.BackColor = Color.FromArgb(234, 243, 222);
                    btn.ForeColor = Color.FromArgb(39, 80, 10);
                    btn.Click += BtnHoanThanh_Click;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(230, 241, 251);
                    btn.ForeColor = Color.FromArgb(12, 68, 124);
                    btn.Click += BtnBatDau_Click;
                }

                card.Controls.Add(btn);
                flp.Controls.Add(card);
            }
        }

        private void BtnBatDau_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not int maDH) return;
            using var db = new AppDbContext();
            var dh = db.DonHangs.Find(maDH);
            if (dh != null)
            {
                dh.TrangThai = TrangThaiDonHang.DangLam;
                db.SaveChanges();
            }
            LoadDonHang();
        }

        private void BtnHoanThanh_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not int maDH) return;
            using var db = new AppDbContext();
            var dh = db.DonHangs.Find(maDH);
            if (dh != null)
            {
                dh.TrangThai = TrangThaiDonHang.ChoPhucVu;
                db.SaveChanges();
            }
            MessageBox.Show("Đã xong! Nhân viên sẽ mang ra bàn.", "Bếp",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDonHang();
        }
    }
}
