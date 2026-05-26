using System;
using System.Windows.Forms;
using DuAn_DineSmart.DAL;

namespace DuAn_DineSmart.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
                lblError.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(role))
            {
                lblError.Text = "⚠ Vui lòng chọn vai trò!";
                lblError.Visible = true;
                return;
            }

            try
            {
                var dal = new NguoiDungDAL();
                var nguoiDung = dal.KiemTraDangNhap(username, password, role);

                if (nguoiDung != null)
                {
                    lblError.Visible = false;
                    var main = new frmMain(nguoiDung);
                    main.Show();
                    this.Hide();
                    main.FormClosed += (s, ev) => this.Close();
                }
                else
                {
                    lblError.Text = "⚠ Sai thông tin đăng nhập hoặc không đúng vai trò!";
                    lblError.Visible = true;
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database:\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /*        private void btnLogin_Click(object sender, EventArgs e)
                {
                    string username = txtUsername.Text.Trim();
                    string password = txtPassword.Text.Trim();
                    string role = cmbRole.Text;

                    // Kiểm tra để trống
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        lblError.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
                        lblError.Visible = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(role))
                    {
                        lblError.Text = "⚠ Vui lòng chọn vai trò!";
                        lblError.Visible = true;
                        return;
                    }

                    // Tạm thời dùng tài khoản mặc định để test
                    // (sau này sẽ thay bằng truy vấn MySQL)
                    if (username == "admin" && password == "123456")
                    {
                        lblError.Visible = false;
                        MessageBox.Show($"Chào mừng {username} - {role}!",
                            "Đăng nhập thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // TODO: Mở form chính sau khi đăng nhập
                        // frmMain main = new frmMain();
                        // main.Show();
                        // this.Hide();
                    }
                    else
                    {
                        lblError.Text = "⚠ Sai tên đăng nhập hoặc mật khẩu!";
                        lblError.Visible = true;
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
        */
        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Chọn mặc định vai trò đầu tiên
            if (cmbRole.Items.Count > 0)
                cmbRole.SelectedIndex = 0;
        }
    }
}