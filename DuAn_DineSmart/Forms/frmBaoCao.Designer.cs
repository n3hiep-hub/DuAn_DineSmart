namespace DuAn_DineSmart.Forms
{
    partial class frmBaoCao
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
            pnlFilter = new Panel();
            cmbLoai = new ComboBox();
            dtpTu = new DateTimePicker();
            dtpDen = new DateTimePicker();
            btnXem = new Button();
            pnlStats = new Panel();
            cardDT = new Panel(); lblDTLabel = new Label(); lblDTVal = new Label();
            cardDon = new Panel(); lblDonLabel = new Label(); lblDonVal = new Label();
            cardTB = new Panel(); lblTBLabel = new Label(); lblTBVal = new Label();
            cardNgay = new Panel(); lblNgayLabel = new Label(); lblNgayVal = new Label();
            pnlContent = new Panel();
            lvDonHang = new ListView();
            colSTT = new ColumnHeader(); colTime = new ColumnHeader();
            colBan = new ColumnHeader(); colMon = new ColumnHeader();
            colTong = new ColumnHeader(); colTT = new ColumnHeader();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            lblTitle.Text = "Báo cáo doanh thu";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            // pnlFilter
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 50;
            pnlFilter.Controls.AddRange(new Control[] { cmbLoai, dtpTu, dtpDen, btnXem });

            cmbLoai.Location = new Point(20, 12);
            cmbLoai.Size = new Size(130, 28);
            cmbLoai.Font = new Font("Segoe UI", 10);
            cmbLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLoai.Items.AddRange(new[] { "Theo ngày", "Theo tháng" });
            cmbLoai.SelectedIndex = 0;

            dtpTu.Location = new Point(165, 12);
            dtpTu.Size = new Size(150, 28);
            dtpTu.Font = new Font("Segoe UI", 10);
            dtpTu.Format = DateTimePickerFormat.Short;
            dtpTu.Value = DateTime.Today.AddDays(-6);

            dtpDen.Location = new Point(330, 12);
            dtpDen.Size = new Size(150, 28);
            dtpDen.Font = new Font("Segoe UI", 10);
            dtpDen.Format = DateTimePickerFormat.Short;
            dtpDen.Value = DateTime.Today;

            btnXem.Text = "Xem báo cáo";
            btnXem.Location = new Point(495, 10);
            btnXem.Size = new Size(130, 32);
            btnXem.FlatStyle = FlatStyle.Flat;
            btnXem.FlatAppearance.BorderSize = 0;
            btnXem.BackColor = Color.FromArgb(192, 57, 43);
            btnXem.ForeColor = Color.White;
            btnXem.Font = new Font("Segoe UI", 10);

            // pnlStats
            pnlStats.BackColor = Color.FromArgb(245, 245, 245);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Height = 85;
            pnlStats.Controls.AddRange(new Control[] { cardDT, cardDon, cardTB, cardNgay });

            void MakeCard(Panel card, Label lbl, Label val, string lt, string vt, int x, Color vc)
            {
                card.BackColor = Color.White; card.BorderStyle = BorderStyle.FixedSingle;
                card.Location = new Point(x, 12); card.Size = new Size(200, 60);
                lbl.Text = lt; lbl.Font = new Font("Segoe UI", 9); lbl.ForeColor = Color.Gray;
                lbl.Location = new Point(12, 8); lbl.AutoSize = true;
                val.Text = vt; val.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                val.ForeColor = vc; val.Location = new Point(12, 28); val.AutoSize = true;
                card.Controls.Add(lbl); card.Controls.Add(val);
            }
            MakeCard(cardDT, lblDTLabel, lblDTVal, "Tổng doanh thu", "0đ", 20, Color.FromArgb(192, 57, 43));
            MakeCard(cardDon, lblDonLabel, lblDonVal, "Đơn hoàn thành", "0", 235, Color.FromArgb(50, 50, 50));
            MakeCard(cardTB, lblTBLabel, lblTBVal, "Trung bình/đơn", "0đ", 450, Color.FromArgb(12, 68, 124));
            MakeCard(cardNgay, lblNgayLabel, lblNgayVal, "Doanh thu hôm nay", "0đ", 665, Color.FromArgb(39, 80, 10));

            // pnlContent (chứa ListView)
            pnlContent.BackColor = Color.FromArgb(245, 245, 245);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Padding = new Padding(16, 10, 16, 10);
            pnlContent.Controls.Add(lvDonHang);

            lvDonHang.Dock = DockStyle.Fill;
            lvDonHang.View = View.Details;
            lvDonHang.FullRowSelect = true;
            lvDonHang.GridLines = true;
            lvDonHang.Font = new Font("Segoe UI", 10);
            lvDonHang.BackColor = Color.White;
            colSTT.Text = "#"; colSTT.Width = 40;
            colTime.Text = "Thời gian"; colTime.Width = 150;
            colBan.Text = "Bàn"; colBan.Width = 90;
            colMon.Text = "Số món"; colMon.Width = 80;
            colTong.Text = "Tổng tiền"; colTong.Width = 130;
            colTT.Text = "Trạng thái"; colTT.Width = 120;
            lvDonHang.Columns.AddRange(new[] { colSTT, colTime, colBan, colMon, colTong, colTT });

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Báo cáo doanh thu";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(pnlContent);
            Controls.Add(pnlStats);
            Controls.Add(pnlFilter);
            Controls.Add(pnlTop);
            Load += frmBaoCao_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlFilter, pnlStats, pnlContent;
        private Label lblTitle;
        private ComboBox cmbLoai;
        private DateTimePicker dtpTu, dtpDen;
        private Button btnXem;
        private Panel cardDT, cardDon, cardTB, cardNgay;
        private Label lblDTLabel, lblDTVal, lblDonLabel, lblDonVal;
        private Label lblTBLabel, lblTBVal, lblNgayLabel, lblNgayVal;
        private ListView lvDonHang;
        private ColumnHeader colSTT, colTime, colBan, colMon, colTong, colTT;
    }
}