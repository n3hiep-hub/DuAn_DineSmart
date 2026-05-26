namespace DuAn_DineSmart.Forms
{
    partial class frmDatMon
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblTitle = new Label();
            lblBanHienTai = new Label();
            pnlLeft = new Panel();
            lblTitleBan = new Label();
            lbBanAn = new ListBox();
            pnlMiddle = new Panel();
            lblTitleMenu = new Label();
            pnlDanhMuc = new Panel();
            flpMonAn = new FlowLayoutPanel();
            pnlRight = new Panel();
            lblTitleGio = new Label();
            lvGioMon = new ListView();
            colTen = new ColumnHeader();
            colSL = new ColumnHeader();
            colGia = new ColumnHeader();
            lblTongTien = new Label();
            btnGuiBep = new Button();
            btnXoaTat = new Button();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 50;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblBanHienTai);

            lblTitle.Text = "Đặt món";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 12);
            lblTitle.AutoSize = true;

            lblBanHienTai.Text = "← Chọn bàn";
            lblBanHienTai.Font = new Font("Segoe UI", 10);
            lblBanHienTai.ForeColor = Color.FromArgb(192, 57, 43);
            lblBanHienTai.Location = new Point(160, 15);
            lblBanHienTai.AutoSize = true;

            // pnlLeft
            pnlLeft.BackColor = Color.White;
            pnlLeft.BorderStyle = BorderStyle.FixedSingle;
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Width = 180;
            pnlLeft.Controls.Add(lblTitleBan);
            pnlLeft.Controls.Add(lbBanAn);

            lblTitleBan.Text = "CHỌN BÀN";
            lblTitleBan.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleBan.ForeColor = Color.Gray;
            lblTitleBan.Location = new Point(12, 12);
            lblTitleBan.AutoSize = true;

            lbBanAn.Location = new Point(8, 35);
            lbBanAn.Size = new Size(162, 500);
            lbBanAn.BorderStyle = BorderStyle.None;
            lbBanAn.Font = new Font("Segoe UI", 10);
            lbBanAn.ItemHeight = 32;

            // pnlRight
            pnlRight.BackColor = Color.White;
            pnlRight.BorderStyle = BorderStyle.FixedSingle;
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Width = 240;
            pnlRight.Controls.AddRange(new Control[] {
                lblTitleGio, lvGioMon, lblTongTien, btnGuiBep, btnXoaTat
            });

            lblTitleGio.Text = "GIỎ MÓN";
            lblTitleGio.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleGio.ForeColor = Color.Gray;
            lblTitleGio.Location = new Point(12, 12);
            lblTitleGio.AutoSize = true;

            lvGioMon.Location = new Point(8, 35);
            lvGioMon.Size = new Size(220, 360);
            lvGioMon.View = View.Details;
            lvGioMon.FullRowSelect = true;
            lvGioMon.GridLines = true;
            lvGioMon.Font = new Font("Segoe UI", 9);
            colTen.Text = "Món"; colTen.Width = 100;
            colSL.Text = "SL"; colSL.Width = 35;
            colGia.Text = "Giá"; colGia.Width = 75;
            lvGioMon.Columns.AddRange(new[] { colTen, colSL, colGia });

            lblTongTien.Text = "Tổng: 0đ";
            lblTongTien.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(192, 57, 43);
            lblTongTien.Location = new Point(12, 402);
            lblTongTien.AutoSize = true;

            btnGuiBep.Text = "Gửi bếp";
            btnGuiBep.Size = new Size(220, 38);
            btnGuiBep.Location = new Point(8, 430);
            btnGuiBep.BackColor = Color.FromArgb(192, 57, 43);
            btnGuiBep.ForeColor = Color.White;
            btnGuiBep.FlatStyle = FlatStyle.Flat;
            btnGuiBep.FlatAppearance.BorderSize = 0;
            btnGuiBep.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            btnXoaTat.Text = "Xóa tất cả";
            btnXoaTat.Size = new Size(220, 30);
            btnXoaTat.Location = new Point(8, 474);
            btnXoaTat.FlatStyle = FlatStyle.Flat;
            btnXoaTat.BackColor = Color.WhiteSmoke;
            btnXoaTat.ForeColor = Color.Gray;
            btnXoaTat.Font = new Font("Segoe UI", 9);

            // pnlMiddle
            pnlMiddle.BackColor = Color.FromArgb(245, 245, 245);
            pnlMiddle.Dock = DockStyle.Fill;
            pnlMiddle.Controls.Add(flpMonAn);
            pnlMiddle.Controls.Add(pnlDanhMuc);
            pnlMiddle.Controls.Add(lblTitleMenu);

            lblTitleMenu.Text = "THỰC ĐƠN";
            lblTitleMenu.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleMenu.ForeColor = Color.Gray;
            lblTitleMenu.Location = new Point(12, 12);
            lblTitleMenu.AutoSize = true;

            pnlDanhMuc.BackColor = Color.FromArgb(245, 245, 245);
            pnlDanhMuc.Location = new Point(0, 30);
            pnlDanhMuc.Size = new Size(600, 40);

            flpMonAn.Location = new Point(8, 75);
            flpMonAn.Size = new Size(590, 500);
            flpMonAn.WrapContents = true;
            flpMonAn.AutoScroll = true;
            flpMonAn.BackColor = Color.FromArgb(245, 245, 245);

            // frmDatMon
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Đặt món";
            Controls.Add(pnlMiddle);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlTop);
            Load += frmDatMon_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlLeft, pnlMiddle, pnlRight, pnlDanhMuc;
        private Label lblTitle, lblBanHienTai, lblTitleBan;
        private Label lblTitleMenu, lblTitleGio, lblTongTien;
        private ListBox lbBanAn;
        private FlowLayoutPanel flpMonAn;
        private ListView lvGioMon;
        private ColumnHeader colTen, colSL, colGia;
        private Button btnGuiBep, btnXoaTat;
    }
}