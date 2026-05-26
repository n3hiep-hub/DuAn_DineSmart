using DuAn_DineSmart.DAL;
using DuAn_DineSmart.Models;

namespace DuAn_DineSmart.Forms
{
    public partial class frmBanAn : Form
    {
        private DashboardDAL _dal = new DashboardDAL();
        private List<BanAn> _dsBan = new();
        private string _filterHienTai = "Tất cả";

        public frmBanAn()
        {
            InitializeComponent();
        }

        private void frmBanAn_Load(object sender, EventArgs e)
        {
            btnFilterTatCa.Click += (s, ev) => ApplyFilter("Tất cả");
            btnFilterTrong.Click += (s, ev) => ApplyFilter("Trống");
            btnFilterCoKhach.Click += (s, ev) => ApplyFilter("Có khách");
            btnFilterDatTruoc.Click += (s, ev) => ApplyFilter("Đặt trước");
            btnThemBan.Click += BtnThemBan_Click;
            LoadBanAn();
        }

        private void LoadBanAn()
        {
            try
            {
                _dsBan = _dal.GetAllBan();
                UpdateFilterButtons();
                RenderBanAn(_dsBan);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter(string filter)
        {
            _filterHienTai = filter;
            var ds = filter == "Tất cả"
                ? _dsBan
                : _dsBan.Where(b => b.TrangThai == filter).ToList();
            UpdateFilterButtons();
            RenderBanAn(ds);
        }

        private void UpdateFilterButtons()
        {
            var btns = new Dictionary<string, Button>
            {
                ["Tất cả"] = btnFilterTatCa,
                ["Trống"] = btnFilterTrong,
                ["Có khách"] = btnFilterCoKhach,
                ["Đặt trước"] = btnFilterDatTruoc
            };

            int tong = _dsBan.Count;
            int trong = _dsBan.Count(b => b.TrangThai == "Trống");
            int coKhach = _dsBan.Count(b => b.TrangThai == "Có khách");
            int datTruoc = _dsBan.Count(b => b.TrangThai == "Đặt trước");

            btnFilterTatCa.Text = $"Tất cả ({tong})";
            btnFilterTrong.Text = $"Trống ({trong})";
            btnFilterCoKhach.Text = $"Có khách ({coKhach})";
            btnFilterDatTruoc.Text = $"Đặt trước ({datTruoc})";

            foreach (var kv in btns)
            {
                kv.Value.BackColor = kv.Key == _filterHienTai
                    ? Color.FromArgb(192, 57, 43)
                    : Color.White;
                kv.Value.ForeColor = kv.Key == _filterHienTai
                    ? Color.White
                    : Color.FromArgb(80, 80, 80);
            }
        }

        private void RenderBanAn(List<BanAn> dsBan)
        {
            flpBanAn.Controls.Clear();

            foreach (var ban in dsBan)
            {
                var card = new Panel
                {
                    Size = new Size(130, 90),
                    Margin = new Padding(8),
                    Cursor = Cursors.Hand,
                    Tag = ban
                };

                var lblTen = new Label
                {
                    Text = ban.TenBan,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, 12),
                    AutoSize = true
                };

                var lblTT = new Label
                {
                    Text = ban.TrangThai,
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(10, 38),
                    AutoSize = true
                };

                switch (ban.TrangThai)
                {
                    case "Có khách":
                        card.BackColor = Color.FromArgb(252, 235, 235);
                        card.Paint += (s, e) => DrawBorder(s, e, Color.FromArgb(226, 75, 74));
                        lblTen.ForeColor = Color.FromArgb(163, 45, 45);
                        lblTT.ForeColor = Color.FromArgb(121, 31, 31);
                        break;
                    case "Đặt trước":
                        card.BackColor = Color.FromArgb(250, 238, 218);
                        card.Paint += (s, e) => DrawBorder(s, e, Color.FromArgb(239, 159, 39));
                        lblTen.ForeColor = Color.FromArgb(99, 56, 6);
                        lblTT.ForeColor = Color.FromArgb(133, 79, 11);
                        break;
                    default:
                        card.BackColor = Color.FromArgb(234, 243, 222);
                        card.Paint += (s, e) => DrawBorder(s, e, Color.FromArgb(99, 153, 34));
                        lblTen.ForeColor = Color.FromArgb(39, 80, 10);
                        lblTT.ForeColor = Color.FromArgb(59, 109, 17);
                        break;
                }

                card.Controls.Add(lblTen);
                card.Controls.Add(lblTT);
                card.Click += Card_Click;
                lblTen.Click += Card_Click;
                lblTT.Click += Card_Click;

                flpBanAn.Controls.Add(card);
            }
        }

        private void DrawBorder(object? sender, PaintEventArgs e, Color color)
        {
            if (sender is Panel p)
                ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle,
                    color, ButtonBorderStyle.Solid);
        }

        private void Card_Click(object? sender, EventArgs e)
        {
            var ban = sender is Panel p ? p.Tag as BanAn
                    : (sender as Control)?.Parent?.Tag as BanAn;
            if (ban != null) ShowDialog_SuaBan(ban);
        }

        private void BtnThemBan_Click(object? sender, EventArgs e)
        {
            ShowDialog_ThemBan();
        }

        private void ShowDialog_SuaBan(BanAn ban)
        {
            using var dlg = new frmBanAnDialog(ban);
            if (dlg.ShowDialog() == DialogResult.OK)
                LoadBanAn();
        }

        private void ShowDialog_ThemBan()
        {
            using var dlg = new frmBanAnDialog(null);
            if (dlg.ShowDialog() == DialogResult.OK)
                LoadBanAn();
        }
    }
}