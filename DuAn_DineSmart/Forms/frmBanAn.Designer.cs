namespace DuAn_DineSmart.Forms
{
    partial class frmBanAn
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
            btnThemBan = new Button();
            pnlFilter = new Panel();
            btnFilterTatCa = new Button();
            btnFilterTrong = new Button();
            btnFilterCoKhach = new Button();
            btnFilterDatTruoc = new Button();
            flpBanAn = new FlowLayoutPanel();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(btnThemBan);

            lblTitle.Text = "Quản lý bàn ăn";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            btnThemBan.Text = "+ Thêm bàn mới";
            btnThemBan.Size = new Size(140, 34);
            btnThemBan.Location = new Point(820, 10);
            btnThemBan.FlatStyle = FlatStyle.Flat;
            btnThemBan.FlatAppearance.BorderSize = 0;
            btnThemBan.BackColor = Color.FromArgb(192, 57, 43);
            btnThemBan.ForeColor = Color.White;
            btnThemBan.Font = new Font("Segoe UI", 10);

            // pnlFilter
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 45;
            pnlFilter.Controls.AddRange(new Control[] {
                btnFilterTatCa, btnFilterTrong,
                btnFilterCoKhach, btnFilterDatTruoc
            });

            void SetFilter(Button btn, string text, int x)
            {
                btn.Text = text;
                btn.Size = new Size(120, 30);
                btn.Location = new Point(x, 7);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 9);
                btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                btn.FlatAppearance.BorderSize = 1;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(80, 80, 80);
            }

            SetFilter(btnFilterTatCa, "Tất cả", 20);
            SetFilter(btnFilterTrong, "Trống", 150);
            SetFilter(btnFilterCoKhach, "Có khách", 280);
            SetFilter(btnFilterDatTruoc, "Đặt trước", 410);
            btnFilterTatCa.BackColor = Color.FromArgb(192, 57, 43);
            btnFilterTatCa.ForeColor = Color.White;

            // flpBanAn
            flpBanAn.Dock = DockStyle.Fill;
            flpBanAn.BackColor = Color.FromArgb(245, 245, 245);
            flpBanAn.Padding = new Padding(15);
            flpBanAn.WrapContents = true;
            flpBanAn.AutoScroll = true;

            // frmBanAn
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1000, 650);
            Text = "Quản lý bàn ăn";
            Controls.Add(flpBanAn);
            Controls.Add(pnlFilter);
            Controls.Add(pnlTop);
            Load += frmBanAn_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlFilter;
        private Label lblTitle;
        private Button btnThemBan;
        private Button btnFilterTatCa, btnFilterTrong;
        private Button btnFilterCoKhach, btnFilterDatTruoc;
        private FlowLayoutPanel flpBanAn;
    }
}