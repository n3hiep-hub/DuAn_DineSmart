namespace DuAn_DineSmart.Forms
{
    partial class frmNhanVienBep
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
            lblTime = new Label();
            pnlStats = new Panel();
            cardCho = new Panel();
            lblChoLabel = new Label();
            lblChoVal = new Label();
            cardXong = new Panel();
            lblXongLabel = new Label();
            lblXongVal = new Label();
            pnlBody = new Panel();
            lblTitleCho = new Label();
            flpChoPhucVu = new FlowLayoutPanel();
            timer1 = new System.Windows.Forms.Timer();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblTime);

            lblTitle.Text = "Thông báo phục vụ";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            lblTime.Text = DateTime.Now.ToString("HH:mm");
            lblTime.Font = new Font("Segoe UI", 10);
            lblTime.ForeColor = Color.Gray;
            lblTime.Location = new Point(700, 18);
            lblTime.AutoSize = true;

            // pnlStats
            pnlStats.BackColor = Color.FromArgb(245, 245, 245);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Height = 80;
            pnlStats.Controls.Add(cardCho);
            pnlStats.Controls.Add(cardXong);

            SetupCard(cardCho, lblChoLabel, lblChoVal,
                "Chờ mang ra bàn", "0", 20, Color.FromArgb(12, 68, 124));
            SetupCard(cardXong, lblXongLabel, lblXongVal,
                "Đã phục vụ hôm nay", "0", 255, Color.FromArgb(39, 80, 10));

            // pnlBody
            pnlBody.BackColor = Color.FromArgb(245, 245, 245);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Padding = new Padding(16, 10, 16, 10);
            pnlBody.Controls.Add(flpChoPhucVu);
            pnlBody.Controls.Add(lblTitleCho);

            lblTitleCho.Text = "MÓN ĂN SẴN SÀNG – CẦN MANG RA BÀN";
            lblTitleCho.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleCho.ForeColor = Color.FromArgb(12, 68, 124);
            lblTitleCho.Location = new Point(16, 10);
            lblTitleCho.AutoSize = true;

            flpChoPhucVu.Location = new Point(16, 35);
            flpChoPhucVu.Size = new Size(900, 550);
            flpChoPhucVu.WrapContents = true;
            flpChoPhucVu.AutoScroll = true;
            flpChoPhucVu.BackColor = Color.FromArgb(245, 245, 245);

            // Timer refresh 4 giây — đủ nhanh để phát hiện món mới từ bếp
            timer1.Interval = 4000;
            timer1.Tick += (s, e) =>
            {
                LoadDonHang();
                lblTime.Text = DateTime.Now.ToString("HH:mm");
            };
            timer1.Start();

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Thông báo phục vụ – DineSmart";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(pnlBody);
            Controls.Add(pnlStats);
            Controls.Add(pnlTop);
            Load += frmNhanVienBep_Load;
            ResumeLayout(false);
        }

        private static void SetupCard(Panel card, Label lbl, Label val,
            string labelText, string valText, int x, Color valColor)
        {
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Location = new Point(x, 10);
            card.Size = new Size(220, 58);
            lbl.Text = labelText;
            lbl.Font = new Font("Segoe UI", 9);
            lbl.ForeColor = Color.Gray;
            lbl.Location = new Point(12, 8);
            lbl.AutoSize = true;
            val.Text = valText;
            val.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            val.ForeColor = valColor;
            val.Location = new Point(12, 28);
            val.AutoSize = true;
            card.Controls.Add(lbl);
            card.Controls.Add(val);
        }

        private Panel pnlTop, pnlStats, pnlBody;
        private Panel cardCho, cardXong;
        private Label lblTitle, lblTime, lblTitleCho;
        private Label lblChoLabel, lblChoVal, lblXongLabel, lblXongVal;
        private FlowLayoutPanel flpChoPhucVu;
        private System.Windows.Forms.Timer timer1;
    }
}