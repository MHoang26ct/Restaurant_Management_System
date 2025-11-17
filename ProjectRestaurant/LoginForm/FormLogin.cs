using Guna.UI2.WinForms;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ProjectRestaurant
{
    public partial class FormLogin : Form {
        public FormLogin() {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void IconPanel_Click(object sender, EventArgs e) {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e) {

        }
        //PlaceholderText Username
        private void TextUsername_Enter(object sender, EventArgs e) {
            if (UsernameTextbox.PlaceholderText == "Username") {
                UsernameTextbox.Text = "";

            }
        }
        private void TextUsername_Leave(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(UsernameTextbox.Text)) {
                UsernameTextbox.PlaceholderText = "Username";

            }
        }
        //PlaceholderText Password 
        private void TextPassword_Enter(object sender, EventArgs e) {
            if (PasswordTextbox.PlaceholderText == "Password") {
                PasswordTextbox.Text = "";

            }
        }
        private void TextPassword_Leave(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(PasswordTextbox.Text)) {
                PasswordTextbox.PlaceholderText = "Password";

            }
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e) {

        }
        private void UsernameTextbox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                PasswordTextbox.Focus(); // Tự động nhảy xuống password khi Enter
            }

            if (!char.IsControl(e.KeyChar) && e.KeyChar > 127)
                e.Handled = true;
        }
        private void PasswordTextbox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Enter) {
                ContinueButton_Click(sender, e);
            }
            if (!char.IsControl(e.KeyChar) && e.KeyChar > 127)
                e.Handled = true;
        }

        private void ContinueButton_Click(object sender, EventArgs e) {
            frmMain frmMain = new frmMain();
            frmMain.Show();
            this.Hide();
        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e) {

        }

        private void ShowPWBox_CheckedChanged(object sender, EventArgs e) {
            if (PasswordTextbox.PasswordChar == '*') {
                PasswordTextbox.PasswordChar = '\0';
            }
            else {
                PasswordTextbox.PasswordChar = '*';
            }

        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
