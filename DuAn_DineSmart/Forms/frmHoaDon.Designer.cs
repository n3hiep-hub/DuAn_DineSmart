namespace DuAn_DineSmart.Forms
{
    partial class frmHoaDon
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
            pnlLeft = new Panel();
            lblTitleBan = new Label();
            lbBan = new ListBox();
            pnlRight = new Panel();
            lblRestName = new Label();
            lblRestSub = new Label();
            lblBanInfo = new Label();
            lblNVInfo = new Label();
            lvChiTiet = new ListView();
            colMon = new ColumnHeader();
            colSL = new ColumnHeader();
            colDon = new ColumnHeader();
            colTT = new ColumnHeader();
            lblTamTinh = new Label();
            lblGiamGia = new Label();
            lblTongTT = new Label();
            lblPTTT = new Label();
            pnlPhuongThuc = new Panel();
            btnTienMat = new Button();
            btnThe = new Button();
            btnQR = new Button();
            btnThanhToan = new Button();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);

            lblTitle.Text = "Hóa đơn & Thanh toán";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            // pnlLeft
            pnlLeft.BackColor = Color.White;
            pnlLeft.BorderStyle = BorderStyle.FixedSingle;
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Width = 210;
            pnlLeft.Controls.Add(lblTitleBan);
            pnlLeft.Controls.Add(lbBan);

            lblTitleBan.Text = "BÀN ĐANG CÓ KHÁCH";
            lblTitleBan.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblTitleBan.ForeColor = Color.Gray;
            lblTitleBan.Location = new Point(12, 12);
            lblTitleBan.AutoSize = true;

            lbBan.Location = new Point(8, 35);
            lbBan.Size = new Size(192, 500);
            lbBan.BorderStyle = BorderStyle.None;
            lbBan.Font = new Font("Segoe UI", 10);
            lbBan.ItemHeight = 32;

            // pnlRight
            pnlRight.BackColor = Color.FromArgb(245, 245, 245);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.AutoScroll = true;
            pnlRight.Padding = new Padding(20);
            pnlRight.Controls.AddRange(new Control[] {
                lblRestName, lblRestSub, lblBanInfo, lblNVInfo,
                lvChiTiet, lblTamTinh, lblGiamGia, lblTongTT,
                lblPTTT, pnlPhuongThuc, btnThanhToan
            });

            int y = 20;
            lblRestName.Text = "DineSmart";
            lblRestName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblRestName.ForeColor = Color.FromArgb(192, 57, 43);
            lblRestName.Location = new Point(20, y);
            lblRestName.AutoSize = true; y += 32;

            lblRestSub.Text = "Nhà hàng DineSmart";
            lblRestSub.Font = new Font("Segoe UI", 9);
            lblRestSub.ForeColor = Color.Gray;
            lblRestSub.Location = new Point(20, y);
            lblRestSub.AutoSize = true; y += 22;

            lblBanInfo.Text = "Chọn bàn để xem hóa đơn";
            lblBanInfo.Font = new Font("Segoe UI", 9);
            lblBanInfo.ForeColor = Color.Gray;
            lblBanInfo.Location = new Point(20, y);
            lblBanInfo.AutoSize = true; y += 20;

            lblNVInfo.Text = "";
            lblNVInfo.Font = new Font("Segoe UI", 9);
            lblNVInfo.ForeColor = Color.Gray;
            lblNVInfo.Location = new Point(20, y);
            lblNVInfo.AutoSize = true; y += 30;

            lvChiTiet.Location = new Point(20, y);
            lvChiTiet.Size = new Size(680, 220);
            lvChiTiet.View = View.Details;
            lvChiTiet.FullRowSelect = true;
            lvChiTiet.GridLines = true;
            lvChiTiet.Font = new Font("Segoe UI", 10);
            lvChiTiet.BackColor = Color.White;
            colMon.Text = "Tên món"; colMon.Width = 260;
            colSL.Text = "SL"; colSL.Width = 50;
            colDon.Text = "Đơn giá"; colDon.Width = 110;
            colTT.Text = "Thành tiền"; colTT.Width = 120;
            lvChiTiet.Columns.AddRange(new[] { colMon, colSL, colDon, colTT });
            y += 235;

            lblTamTinh.Text = "Tạm tính: 0đ";
            lblTamTinh.Font = new Font("Segoe UI", 10);
            lblTamTinh.ForeColor = Color.FromArgb(80, 80, 80);
            lblTamTinh.Location = new Point(20, y);
            lblTamTinh.AutoSize = true; y += 24;

            lblGiamGia.Text = "Giảm giá: 0đ";
            lblGiamGia.Font = new Font("Segoe UI", 10);
            lblGiamGia.ForeColor = Color.FromArgb(80, 80, 80);
            lblGiamGia.Location = new Point(20, y);
            lblGiamGia.AutoSize = true; y += 28;

            lblTongTT.Text = "Tổng thanh toán: 0đ";
            lblTongTT.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTongTT.ForeColor = Color.FromArgb(192, 57, 43);
            lblTongTT.Location = new Point(20, y);
            lblTongTT.AutoSize = true; y += 36;

            lblPTTT.Text = "Phương thức thanh toán:";
            lblPTTT.Font = new Font("Segoe UI", 9);
            lblPTTT.ForeColor = Color.Gray;
            lblPTTT.Location = new Point(20, y);
            lblPTTT.AutoSize = true; y += 22;

            pnlPhuongThuc.Location = new Point(20, y);
            pnlPhuongThuc.Size = new Size(320, 38);
            pnlPhuongThuc.BackColor = Color.FromArgb(245, 245, 245);
            pnlPhuongThuc.Controls.AddRange(new Control[] { btnTienMat, btnThe, btnQR });
            y += 50;

            void SetPayBtn(Button btn, string text, int x, bool active)
            {
                btn.Text = text;
                btn.Size = new Size(100, 34);
                btn.Location = new Point(x, 0);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 9);
                btn.Tag = text;
                if (active) { btn.BackColor = Color.FromArgb(234, 243, 222); btn.ForeColor = Color.FromArgb(39, 80, 10); btn.FlatAppearance.BorderColor = Color.FromArgb(99, 153, 34); }
                else { btn.BackColor = Color.White; btn.ForeColor = Color.FromArgb(80, 80, 80); btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200); }
            }
            SetPayBtn(btnTienMat, "Tiền mặt", 0, true);
            SetPayBtn(btnThe, "Thẻ", 108, false);
            SetPayBtn(btnQR, "QR Code", 216, false);

            btnThanhToan.Text = "Xác nhận thanh toán";
            btnThanhToan.Location = new Point(20, y);
            btnThanhToan.Size = new Size(320, 44);
            btnThanhToan.BackColor = Color.FromArgb(192, 57, 43);
            btnThanhToan.ForeColor = Color.White;
            btnThanhToan.FlatStyle = FlatStyle.Flat;
            btnThanhToan.FlatAppearance.BorderSize = 0;
            btnThanhToan.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // frmHoaDon
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Hóa đơn & Thanh toán";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
            Controls.Add(pnlTop);
            Load += frmHoaDon_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlLeft, pnlRight, pnlPhuongThuc;
        private Label lblTitle, lblTitleBan;
        private Label lblRestName, lblRestSub, lblBanInfo, lblNVInfo;
        private Label lblTamTinh, lblGiamGia, lblTongTT, lblPTTT;
        private ListBox lbBan;
        private ListView lvChiTiet;
        private ColumnHeader colMon, colSL, colDon, colTT;
        private Button btnTienMat, btnThe, btnQR, btnThanhToan;
    }
}