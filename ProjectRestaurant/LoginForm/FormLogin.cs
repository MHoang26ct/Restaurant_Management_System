using Guna.UI2.WinForms;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ProjectRestaurant
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        //PlaceholderText Username
        private void TextUsername_Enter(object sender, EventArgs e)
        {
            if (UsernameTextbox.PlaceholderText == "Username")
            {
                UsernameTextbox.Text = "";

            }
        }
        private void TextUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextbox.Text))
            {
                UsernameTextbox.PlaceholderText = "Username";

            }
        }
        //PlaceholderText Password 
        private void TextPassword_Enter(object sender, EventArgs e)
        {
            if (PasswordTextbox.PlaceholderText == "Password")
            {
                PasswordTextbox.Text = "";

            }
        }
        private void TextPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTextbox.Text))
            {
                PasswordTextbox.PlaceholderText = "Password";

            }
        }
        //Đảo trạng thái ẩn /hiện mật khẩu 
        private void EyeButton_Click(object sender, EventArgs e)
        {
            if (PasswordTextbox.PasswordChar == '\0')
            {
                PasswordTextbox.PasswordChar = '*';
                EyeButton.Image = Properties.Resources.eye_closed; // ẩn
            }
            else
            {
                PasswordTextbox.PasswordChar = '\0';
                EyeButton.Image = Properties.Resources.eye_open; // hiển thị
            }
        }
        //Chặn chữ có dấu 
        private void UsernameTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PasswordTextbox.Focus(); // Tự động nhảy xuống password khi Enter
            }

            if (!char.IsControl(e.KeyChar) && e.KeyChar > 127)
                e.Handled = true;
        }
        private void PasswordTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ContinueButton_Click(sender, e);
            }
            if (!char.IsControl(e.KeyChar) && e.KeyChar > 127)
                e.Handled = true;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.Show();
            this.Hide();
        }

        private void PictureBoxLogin1_Click(object sender, EventArgs e)
        {

        }
    }
}
