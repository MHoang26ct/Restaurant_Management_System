using Autofac;
using Guna.UI2.WinForms;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.Properties;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace FoodOrderManagement
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

                FormMain FormMain = _scope.Resolve<FormMain>();
                FormMain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng, vui lòng nhập lại", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void UsernameTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                PasswordTextbox.Focus();
            }
        }
        private void PasswordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ContinueButton.PerformClick();
            }
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               DialogResult result =  MessageBox.Show("Bạn có chắc muốn đóng ứng dụng?", "Thông báo", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK) 
                {
                    Application.Exit();
                }
            }
        }
    }
}
