namespace DuAn_DineSmart.Forms
{
    partial class frmNhanVien
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
            btnThem = new Button();
            pnlStats = new Panel();
            cardTong = new Panel(); lblTongLabel = new Label(); lblTongVal = new Label();
            cardQL = new Panel(); lblQLLabel = new Label(); lblQLVal = new Label();
            cardNV = new Panel(); lblNVLabel = new Label(); lblNVVal = new Label();
            cardActive = new Panel(); lblActLabel = new Label(); lblActVal = new Label();
            pnlFilter = new Panel();
            txtSearch = new TextBox();
            cmbVaiTro = new ComboBox();
            lvNhanVien = new ListView();
            colSTT = new ColumnHeader(); colTen = new ColumnHeader();
            colVT = new ColumnHeader(); colTT = new ColumnHeader();
            colAct = new ColumnHeader();
            lblCount = new Label();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(btnThem);
            lblTitle.Text = "Quản lý nhân viên";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;
            btnThem.Text = "+ Thêm nhân viên";
            btnThem.Size = new Size(150, 34);
            btnThem.Location = new Point(810, 10);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.BackColor = Color.FromArgb(192, 57, 43);
            btnThem.ForeColor = Color.White;
            btnThem.Font = new Font("Segoe UI", 10);

            // pnlStats
            pnlStats.BackColor = Color.FromArgb(245, 245, 245);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Height = 85;
            pnlStats.Controls.AddRange(new Control[] { cardTong, cardQL, cardNV, cardActive });

            void MakeCard(Panel card, Label lbl, Label val, string labelText, string valText, int x, Color valColor)
            {
                card.BackColor = Color.White;
                card.BorderStyle = BorderStyle.FixedSingle;
                card.Location = new Point(x, 12);
                card.Size = new Size(190, 60);
                lbl.Text = labelText; lbl.Font = new Font("Segoe UI", 9); lbl.ForeColor = Color.Gray;
                lbl.Location = new Point(12, 8); lbl.AutoSize = true;
                val.Text = valText; val.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                val.ForeColor = valColor; val.Location = new Point(12, 28); val.AutoSize = true;
                card.Controls.Add(lbl); card.Controls.Add(val);
            }
            MakeCard(cardTong, lblTongLabel, lblTongVal, "Tổng nhân viên", "0", 20, Color.FromArgb(50, 50, 50));
            MakeCard(cardQL, lblQLLabel, lblQLVal, "Quản lý", "0", 225, Color.FromArgb(163, 45, 45));
            MakeCard(cardNV, lblNVLabel, lblNVVal, "Nhân viên / Bếp", "0", 430, Color.FromArgb(12, 68, 124));
            MakeCard(cardActive, lblActLabel, lblActVal, "Đang hoạt động", "0", 635, Color.FromArgb(39, 80, 10));

            // pnlFilter
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 48;
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(cmbVaiTro);
            txtSearch.Location = new Point(20, 10);
            txtSearch.Size = new Size(280, 28);
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.PlaceholderText = "Tìm kiếm tên đăng nhập...";
            cmbVaiTro.Location = new Point(315, 10);
            cmbVaiTro.Size = new Size(160, 28);
            cmbVaiTro.Font = new Font("Segoe UI", 10);
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaiTro.Items.AddRange(new[] { "Tất cả vai trò", "Quản lý", "Nhân viên", "Bếp", "Thu ngân" });
            cmbVaiTro.SelectedIndex = 0;

            // lvNhanVien
            lvNhanVien.Dock = DockStyle.Fill;
            lvNhanVien.View = View.Details;
            lvNhanVien.FullRowSelect = true;
            lvNhanVien.GridLines = true;
            lvNhanVien.Font = new Font("Segoe UI", 10);
            lvNhanVien.BackColor = Color.White;
            colSTT.Text = "#"; colSTT.Width = 40;
            colTen.Text = "Tên đăng nhập"; colTen.Width = 200;
            colVT.Text = "Vai trò"; colVT.Width = 140;
            colTT.Text = "Trạng thái"; colTT.Width = 120;
            colAct.Text = "Thao tác"; colAct.Width = 180;
            lvNhanVien.Columns.AddRange(new[] { colSTT, colTen, colVT, colTT, colAct });

            lblCount.Text = "";
            lblCount.Font = new Font("Segoe UI", 9);
            lblCount.ForeColor = Color.Gray;
            lblCount.Dock = DockStyle.Bottom;
            lblCount.Height = 25;
            lblCount.Padding = new Padding(20, 4, 0, 0);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Quản lý nhân viên";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(lvNhanVien);
            Controls.Add(pnlFilter);
            Controls.Add(pnlStats);
            Controls.Add(pnlTop);
            Controls.Add(lblCount);
            Load += frmNhanVien_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlStats, pnlFilter;
        private Panel cardTong, cardQL, cardNV, cardActive;
        private Label lblTitle, lblCount;
        private Label lblTongLabel, lblTongVal, lblQLLabel, lblQLVal;
        private Label lblNVLabel, lblNVVal, lblActLabel, lblActVal;
        private Button btnThem;
        private TextBox txtSearch;
        private ComboBox cmbVaiTro;
        private ListView lvNhanVien;
        private ColumnHeader colSTT, colTen, colVT, colTT, colAct;
    }
}