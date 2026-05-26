namespace DuAn_DineSmart.Forms
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            lblLogo = new Label();
            lblSubtitle = new Label();
            btnDashboard = new Button();
            btnBanAn = new Button();
            btnDatMon = new Button();
            btnThucDon = new Button();
            btnHoaDon = new Button();
            btnNhanVien = new Button();
            btnBaoCao = new Button();
            btnBep = new Button();
            btnPhucVu = new Button();
            pnlTopbar = new Panel();
            lblPageTitle = new Label();
            pnlContent = new Panel();
            pnlDashboard = new Panel();
            cardDoanhThu = new Panel();
            lblTitleDoanhThu = new Label();
            lblDoanhThu = new Label();
            cardBanAn = new Panel();
            lblTitleBanAn = new Label();
            lblBanAn = new Label();
            cardDonHang = new Panel();
            lblTitleDonHang = new Label();
            lblDonHang = new Label();
            cardNhanVien = new Panel();
            lblTitleNhanVien = new Label();
            lblNhanVien = new Label();
            pnlBanAn = new Panel();
            lblTitleBanAnPanel = new Label();
            flpBanAn = new FlowLayoutPanel();
            pnlDonHang = new Panel();
            lblTitleDonHangPanel = new Label();
            lvDonHang = new ListView();
            colBan = new ColumnHeader();
            colMon = new ColumnHeader();
            colTrangThai = new ColumnHeader();
            colTongTien = new ColumnHeader();

            SuspendLayout();

            // pnlSidebar
            pnlSidebar.BackColor = Color.FromArgb(192, 57, 43);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 200;
            pnlSidebar.Controls.AddRange(new Control[] {
                lblLogo, lblSubtitle, btnDashboard, btnBanAn,
                btnDatMon, btnThucDon, btnHoaDon, btnBep, btnPhucVu, btnNhanVien, btnBaoCao
            });

            // lblLogo
            lblLogo.Text = "DineSmart";
            lblLogo.ForeColor = Color.White;
            lblLogo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblLogo.Location = new Point(16, 20);
            lblLogo.AutoSize = true;

            // lblSubtitle
            lblSubtitle.Text = "Quản lý nhà hàng";
            lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
            lblSubtitle.Font = new Font("Segoe UI", 9);
            lblSubtitle.Location = new Point(16, 52);
            lblSubtitle.AutoSize = true;

            // Sidebar buttons helper
            void SetupSidebarBtn(Button btn, string text, int y)
            {
                btn.Text = text;
                btn.Size = new Size(200, 45);
                btn.Location = new Point(0, y);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(192, 57, 43);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(15, 0, 0, 0);
            }

            SetupSidebarBtn(btnDashboard, "  Dashboard", 90);
            SetupSidebarBtn(btnBanAn, "  Bàn ăn", 135);
            SetupSidebarBtn(btnDatMon, "  Đặt món", 180);
            SetupSidebarBtn(btnThucDon, "  Thực đơn", 225);
            SetupSidebarBtn(btnHoaDon, "  Hóa đơn", 270);
            SetupSidebarBtn(btnBep, "  Màn hình bếp", 315);  // ← thêm dòng này
            SetupSidebarBtn(btnPhucVu, "  Thông báo phục vụ", 360); // ← thêm
            SetupSidebarBtn(btnNhanVien, "  Nhân viên", 405);
            SetupSidebarBtn(btnBaoCao, "  Báo cáo", 450);
            btnDashboard.BackColor = Color.FromArgb(160, 40, 30);

            // pnlTopbar
            pnlTopbar.BackColor = Color.White;
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Height = 55;
            pnlTopbar.Controls.Add(lblPageTitle);

            // lblPageTitle
            lblPageTitle.Text = "Dashboard";
            lblPageTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblPageTitle.Location = new Point(20, 14);
            lblPageTitle.AutoSize = true;

            // pnlDashboard – bọc tất cả widget dashboard, ẩn/hiện khi navigate
            pnlDashboard.BackColor = Color.FromArgb(245, 245, 245);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.AutoScroll = true;
            pnlDashboard.Controls.AddRange(new Control[] {
                cardDoanhThu, cardBanAn, cardDonHang, cardNhanVien,
                pnlBanAn, pnlDonHang
            });

            // pnlContent – container chứa dashboard hoặc form nhúng
            pnlContent.BackColor = Color.FromArgb(245, 245, 245);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(pnlDashboard);

            // Helper card
            void SetupCard(Panel card, Label title, Label value,
                           string titleText, string valueText,
                           int x, Color valueColor)
            {
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Location = new Point(x, 20);
                card.Size = new Size(200, 90);

                title.Text = titleText;
                title.Font = new Font("Segoe UI", 9);
                title.ForeColor = Color.Gray;
                title.Location = new Point(12, 12);
                title.AutoSize = true;

                value.Text = valueText;
                value.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                value.ForeColor = valueColor;
                value.Location = new Point(12, 40);
                value.AutoSize = true;

                card.Controls.Add(title);
                card.Controls.Add(value);
            }

            SetupCard(cardDoanhThu, lblTitleDoanhThu, lblDoanhThu,
                "Doanh thu hôm nay", "0 đ", 20,
                Color.FromArgb(192, 57, 43));

            SetupCard(cardBanAn, lblTitleBanAn, lblBanAn,
                "Bàn đang phục vụ", "0/0", 235,
                Color.FromArgb(64, 64, 64));

            SetupCard(cardDonHang, lblTitleDonHang, lblDonHang,
                "Đơn hàng hôm nay", "0", 450,
                Color.FromArgb(64, 64, 64));

            SetupCard(cardNhanVien, lblTitleNhanVien, lblNhanVien,
                "Nhân viên trực", "0", 665,
                Color.FromArgb(39, 80, 10));

            // pnlBanAn
            pnlBanAn.BackColor = Color.White;
            pnlBanAn.BorderStyle = BorderStyle.FixedSingle;
            pnlBanAn.Location = new Point(20, 130);
            pnlBanAn.Size = new Size(430, 280);
            pnlBanAn.Controls.Add(lblTitleBanAnPanel);
            pnlBanAn.Controls.Add(flpBanAn);

            lblTitleBanAnPanel.Text = "Trạng thái bàn ăn";
            lblTitleBanAnPanel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTitleBanAnPanel.Location = new Point(12, 12);
            lblTitleBanAnPanel.AutoSize = true;

            flpBanAn.Location = new Point(12, 42);
            flpBanAn.Size = new Size(400, 220);
            flpBanAn.BackColor = Color.White;
            flpBanAn.WrapContents = true;

            // pnlDonHang
            pnlDonHang.BackColor = Color.White;
            pnlDonHang.BorderStyle = BorderStyle.FixedSingle;
            pnlDonHang.Location = new Point(465, 130);
            pnlDonHang.Size = new Size(430, 280);
            pnlDonHang.Controls.Add(lblTitleDonHangPanel);
            pnlDonHang.Controls.Add(lvDonHang);

            lblTitleDonHangPanel.Text = "Đơn hàng gần đây";
            lblTitleDonHangPanel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTitleDonHangPanel.Location = new Point(12, 12);
            lblTitleDonHangPanel.AutoSize = true;

            // lvDonHang
            lvDonHang.Location = new Point(12, 42);
            lvDonHang.Size = new Size(400, 220);
            lvDonHang.View = View.Details;
            lvDonHang.FullRowSelect = true;
            lvDonHang.GridLines = true;
            lvDonHang.BackColor = Color.White;
            colBan.Text = "Bàn"; colBan.Width = 70;
            colMon.Text = "Món"; colMon.Width = 70;
            colTrangThai.Text = "Trạng thái"; colTrangThai.Width = 120;
            colTongTien.Text = "Tổng tiền"; colTongTien.Width = 120;
            lvDonHang.Columns.AddRange(new ColumnHeader[] {
                colBan, colMon, colTrangThai, colTongTien
            });

            // frmMain
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1100, 700);
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1024, 650);
            Text = "DineSmart – Quản lý";
            Controls.Add(pnlContent);
            Controls.Add(pnlTopbar);
            Controls.Add(pnlSidebar);
            Load += frmMain_Load;
            ResumeLayout(false);
        }

        private Panel pnlSidebar, pnlTopbar, pnlContent, pnlDashboard;
        private Panel cardDoanhThu, cardBanAn, cardDonHang, cardNhanVien;
        private Panel pnlBanAn, pnlDonHang;
        private Label lblLogo, lblSubtitle, lblPageTitle;
        private Label lblTitleDoanhThu, lblDoanhThu;
        private Label lblTitleBanAn, lblBanAn;
        private Label lblTitleDonHang, lblDonHang;
        private Label lblTitleNhanVien, lblNhanVien;
        private Label lblTitleBanAnPanel, lblTitleDonHangPanel;
        private Button btnDashboard, btnBanAn, btnDatMon;
        private Button btnThucDon, btnHoaDon, btnBep, btnNhanVien, btnPhucVu, btnBaoCao;
        private FlowLayoutPanel flpBanAn;
        private ListView lvDonHang;
        private ColumnHeader colBan, colMon, colTrangThai, colTongTien;
    }
}