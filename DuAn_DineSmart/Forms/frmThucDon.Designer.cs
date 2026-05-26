namespace DuAn_DineSmart.Forms
{
    partial class frmThucDon
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
            pnlFilter = new Panel();
            txtSearch = new TextBox();
            cmbDanhMuc = new ComboBox();
            lvThucDon = new ListView();
            colSTT = new ColumnHeader();
            colTen = new ColumnHeader();
            colDM = new ColumnHeader();
            colGia = new ColumnHeader();
            colTT = new ColumnHeader();
            colAct = new ColumnHeader();
            lblCount = new Label();
            SuspendLayout();

            // pnlTop
            pnlTop.BackColor = Color.White;
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Height = 55;
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(btnThem);

            lblTitle.Text = "Quản lý thực đơn";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.Location = new Point(20, 14);
            lblTitle.AutoSize = true;

            btnThem.Text = "+ Thêm món mới";
            btnThem.Size = new Size(140, 34);
            btnThem.Location = new Point(820, 10);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.BackColor = Color.FromArgb(192, 57, 43);
            btnThem.ForeColor = Color.White;
            btnThem.Font = new Font("Segoe UI", 10);

            // pnlFilter
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 50;
            pnlFilter.Controls.Add(txtSearch);
            pnlFilter.Controls.Add(cmbDanhMuc);

            txtSearch.Location = new Point(20, 12);
            txtSearch.Size = new Size(300, 28);
            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.PlaceholderText = "Tìm kiếm tên món...";

            cmbDanhMuc.Location = new Point(335, 12);
            cmbDanhMuc.Size = new Size(160, 28);
            cmbDanhMuc.Font = new Font("Segoe UI", 10);
            cmbDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDanhMuc.Items.AddRange(new[] {
                "Tất cả danh mục", "Khai vị",
                "Món chính", "Tráng miệng", "Đồ uống"
            });
            cmbDanhMuc.SelectedIndex = 0;

            // lvThucDon
            lvThucDon.Dock = DockStyle.Fill;
            lvThucDon.View = View.Details;
            lvThucDon.FullRowSelect = true;
            lvThucDon.GridLines = true;
            lvThucDon.Font = new Font("Segoe UI", 10);
            lvThucDon.BackColor = Color.White;
            colSTT.Text = "#"; colSTT.Width = 40;
            colTen.Text = "Tên món"; colTen.Width = 220;
            colDM.Text = "Danh mục"; colDM.Width = 120;
            colGia.Text = "Giá tiền"; colGia.Width = 110;
            colTT.Text = "Trạng thái"; colTT.Width = 110;
            colAct.Text = "Thao tác"; colAct.Width = 150;
            lvThucDon.Columns.AddRange(new[] { colSTT, colTen, colDM, colGia, colTT, colAct });

            // lblCount
            lblCount.Text = "";
            lblCount.Font = new Font("Segoe UI", 9);
            lblCount.ForeColor = Color.Gray;
            lblCount.Dock = DockStyle.Bottom;
            lblCount.Height = 25;
            lblCount.Padding = new Padding(20, 4, 0, 0);

            // frmThucDon
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 650);
            WindowState = FormWindowState.Maximized;
            Text = "Quản lý thực đơn";
            BackColor = Color.FromArgb(245, 245, 245);
            Controls.Add(lvThucDon);
            Controls.Add(pnlFilter);
            Controls.Add(pnlTop);
            Controls.Add(lblCount);
            Load += frmThucDon_Load;
            ResumeLayout(false);
        }

        private Panel pnlTop, pnlFilter;
        private Label lblTitle, lblCount;
        private Button btnThem;
        private TextBox txtSearch;
        private ComboBox cmbDanhMuc;
        private ListView lvThucDon;
        private ColumnHeader colSTT, colTen, colDM, colGia, colTT, colAct;
    }
}