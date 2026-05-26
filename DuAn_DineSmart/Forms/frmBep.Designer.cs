namespace DuAn_DineSmart.Forms
{
    partial class frmBep
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
            cardCho = new Panel(); lblChoLabel = new Label(); lblChoVal = new Label();
            cardLam = new Panel(); lblLamLabel = new Label(); lblLamVal = new Label();
            cardXong = new Panel(); lblXongLabel = new Label(); lblXongVal = new Label();
            pnlBody = new Panel();
            pnlChoBep = new Panel();
            lblTitleCho = new Label();
            flpChoBep = new FlowLayoutPanel();
            pnlDangLam = new Panel();
            lblTitleLam = new Label();
            flpDangLam = new FlowLayoutPanel();
            timer1 = new System.Windows.Forms.Timer();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblTime);

            lblTitle.Text = "Màn hình bếp";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            lblTime.Text = DateTime.Now.ToString("HH:mm – dddd, dd/MM/yyyy");
            lblTime.Font = new Font("Segoe UI", 10);
            lblTime.ForeColor = Color.Gray;
            lblTime.Location = new Point(700, 18);
            lblTime.AutoSize = true;

            // pnlStats
            pnlStats.BackColor = Color.FromArgb(245, 245, 245);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Height = 80;
            pnlStats.Controls.AddRange(new Control[] { cardCho, cardLam, cardXong });

            void MakeCard(Panel card, Label lbl, Label val, string lt, string vt, int x, Color vc)
            {
                card.BackColor = Color.White; card.BorderStyle = BorderStyle.FixedSingle;
                card.Location = new Point(x, 10); card.Size = new Size(200, 58);
                lbl.Text = lt; lbl.Font = new Font("Segoe UI", 9); lbl.ForeColor = Color.Gray;
                lbl.Location = new Point(12, 8); lbl.AutoSize = true;
                val.Text = vt; val.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                val.ForeColor = vc; val.Location = new Point(12, 28); val.AutoSize = true;
                card.Controls.Add(lbl); card.Controls.Add(val);
            }
            MakeCard(cardCho, lblChoLabel, lblChoVal, "Chờ bếp", "0", 20, Color.FromArgb(99, 56, 6));
            MakeCard(cardLam, lblLamLabel, lblLamVal, "Đang làm", "0", 235, Color.FromArgb(12, 68, 124));
            MakeCard(cardXong, lblXongLabel, lblXongVal, "Hoàn thành hôm nay", "0", 450, Color.FromArgb(39, 80, 10));

            // pnlBody
            pnlBody.BackColor = Color.FromArgb(245, 245, 245);
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(pnlDangLam);
            pnlBody.Controls.Add(pnlChoBep);

            // pnlChoBep (trái)
            pnlChoBep.BackColor = Color.FromArgb(245, 245, 245);
            pnlChoBep.Location = new Point(10, 10);
            pnlChoBep.Size = new Size(500, 600);
            pnlChoBep.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            pnlChoBep.Controls.Add(lblTitleCho);
            pnlChoBep.Controls.Add(flpChoBep);

            lblTitleCho.Text = "CHỜ BẾP";
            lblTitleCho.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleCho.ForeColor = Color.FromArgb(99, 56, 6);
            lblTitleCho.Location = new Point(0, 8);
            lblTitleCho.AutoSize = true;

            flpChoBep.Location = new Point(0, 30);
            flpChoBep.Size = new Size(490, 560);
            flpChoBep.WrapContents = false;
            flpChoBep.FlowDirection = FlowDirection.TopDown;
            flpChoBep.AutoScroll = true;
            flpChoBep.BackColor = Color.FromArgb(245, 245, 245);

            // pnlDangLam (phải)
            pnlDangLam.BackColor = Color.FromArgb(245, 245, 245);
            pnlDangLam.Location = new Point(520, 10);
            pnlDangLam.Size = new Size(500, 600);
            pnlDangLam.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            pnlDangLam.Controls.Add(lblTitleLam);
            pnlDangLam.Controls.Add(flpDangLam);

            lblTitleLam.Text = "ĐANG LÀM";
            lblTitleLam.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTitleLam.ForeColor = Color.FromArgb(12, 68, 124);
            lblTitleLam.Location = new Point(0, 8);
            lblTitleLam.AutoSize = true;

            flpDangLam.Location = new Point(0, 30);
            flpDangLam.Size = new Size(490, 560);
            flpDangLam.WrapContents = false;
            flpDangLam.FlowDirection = FlowDirection.TopDown;
            flpDangLam.AutoScroll = true;
            flpDangLam.BackColor = Color.FromArgb(245, 245, 245);

            // Timer tự động refresh 30 giây
            timer1.Interval = 30000;
            timer1.Tick += (s, e) => { LoadDonHang(); lblTime.Text = DateTime.Now.ToString("HH:mm – dddd, dd/MM/yyyy"); };
            timer1.Start();

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 700);
            WindowState = FormWindowState.Maximized;
            Text = "Màn hình bếp – DineSmart";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(pnlBody);
            Controls.Add(pnlStats);
            Controls.Add(pnlTop);
            Load += frmBep_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlStats, pnlBody;
        private Panel pnlChoBep, pnlDangLam;
        private Panel cardCho, cardLam, cardXong;
        private Label lblTitle, lblTime;
        private Label lblChoLabel, lblChoVal, lblLamLabel, lblLamVal, lblXongLabel, lblXongVal;
        private Label lblTitleCho, lblTitleLam;
        private FlowLayoutPanel flpChoBep, flpDangLam;
        private System.Windows.Forms.Timer timer1;
    }
}