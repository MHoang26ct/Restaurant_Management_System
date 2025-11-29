using Autofac;
using Guna.UI2.WinForms;
using ProjectRestaurant.DAL.Models.Entities;
using ProjectRestaurant.DAL.Repositories.Implementations;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.Properties;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace ProjectRestaurant
{

    public partial class FormLogin : Form
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILifetimeScope _scope;
        public FormLogin(IUsersRepository usersRepository, ILifetimeScope scope)
        {
            InitializeComponent();
            _usersRepository = usersRepository;
            _scope = scope;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void IconPanel_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
        //PlaceholderText Username
        private void TextUsername_Enter(object sender, EventArgs e)
        {
            if (UsernameTextbox.Text == "Username")
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
            if (PasswordTextbox.Text == "Password")
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

        private async void ContinueButton_Click(object sender, EventArgs e)
        {

            Users LoginUser = new Users();
            LoginUser.Username = UsernameTextbox.Text;
            LoginUser.PasswordHash = PasswordTextbox.Text;
            bool LoginResult = await _usersRepository.LoginAsync(LoginUser);
            if (LoginResult)
            {
                if (RememberBox.Checked)
                {
                    var Settings = Properties.Settings.Default;
                    Settings.RememberMe = true;
                    Settings.Username = UsernameTextbox.Text;
                    Settings.Password = PasswordTextbox.Text;
                    Settings.Save();
                }
                else
                {
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.Username = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.Save();
                }
                frmMain frmMain = _scope.Resolve<frmMain>();
                frmMain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng, vui lòng nhập lại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShowPWBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PasswordTextbox.PasswordChar == '*')
            {
                PasswordTextbox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextbox.PasswordChar = '*';
            }

        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_FormLoad(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberMe)
            {
                UsernameTextbox.Text = Properties.Settings.Default.Username;
                PasswordTextbox.Text = Properties.Settings.Default.Password;
                RememberBox.CheckState = CheckState.Checked;
            }
            else
            {
                UsernameTextbox.Text = string.Empty;
                PasswordTextbox.Text = string.Empty;
                RememberBox.CheckState = CheckState.Unchecked;
            }
        }

        private void UsernameTextbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
