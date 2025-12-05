using Autofac;
using ProjectRestaurant.AdminControl;
using ProjectRestaurant.DAL.Repositories.Implementations;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;

namespace ProjectRestaurant
{
    public partial class frmMain : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly IUsersRepository _usersRepository;
        public static frmMain instance { get; private set; }
        private Form CurrentChildForm;
        public frmMain(ILifetimeScope scope)
        {
            InitializeComponent();
            _scope = scope;
            instance = this;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            DashboardButton.PerformClick();// Bắt đầu vào giao diện nút Dasdboard được hiện lên
        }

        public void OpenChildForm(Form ChilForm) // Mở Form con trên MainPanel
        {
            if (CurrentChildForm != null)
            {
                CurrentChildForm.Close(); // Đóng form con đang mở 
            }
            CurrentChildForm = ChilForm; // Gán form con đang mở = form con vừa thao tác

            ChilForm.TopLevel = false; // Chuyển form từ độc lập thành nằm trong một thứ khác  
            ChilForm.FormBorderStyle = FormBorderStyle.None; // Bỏ toàn bộ viền của cửa sổ
            ChilForm.Dock = DockStyle.Fill; // Lắp đầy không gian của vật chứa

            MainPanel.Controls.Add(ChilForm); // Thêm ChillForm vào danh sách control con của MainPanel
            MainPanel.Tag = ChilForm; // Lưu Tag MainPanel 
            ChilForm.BringToFront(); // MainPanel chứa nhiều control con, đảm bảo ChillForm này được đưa lên đầu
            ChilForm.Show(); // Hiển thị ChillForm
        }
        public void ResetAllButton()
        {
            //
            // DashBoardButton
            //
            DashboardButton.BackColor = Color.White;
            DashboardButton.FillColor = Color.White;
            DashboardButton.ForeColor = Color.Black;
            DashboardButton.Image = Properties.Resources.DashboardBlack; // Icon lúc chưa click
            //
            // MenuButton
            //
            MenuButton.BackColor = Color.White;
            MenuButton.FillColor = Color.White;
            MenuButton.ForeColor = Color.Black;
            MenuButton.Image = Properties.Resources.Menublack; // Icon lúc chưa click
            //
            // OrderButton
            //
            OrderButton.BackColor = Color.White;
            OrderButton.FillColor = Color.White;
            OrderButton.ForeColor = Color.Black;
            OrderButton.Image = Properties.Resources.OderBlack;
            // Icon lúc chưa click
            //
            // TableButton
            //
            TableButton.BackColor = Color.White;
            TableButton.FillColor = Color.White;
            TableButton.ForeColor = Color.Black;
            TableButton.Image = Properties.Resources.TableBlack; // Icon lúc chưa click
            //
            // CustomerButton
            //
            CustomerButton.BackColor = Color.White;
            CustomerButton.FillColor = Color.White;
            CustomerButton.ForeColor = Color.Black;
            CustomerButton.Image = Properties.Resources.CustomerBlack; // Icon lúc chưa click
            //
            // ReportsButton
            //
            ReportsButton.BackColor = Color.White;
            ReportsButton.FillColor = Color.White;
            ReportsButton.ForeColor = Color.Black;
            ReportsButton.Image = Properties.Resources.ReportBlack; // Icon lúc chưa click
            //
            // EmployeesButton
            //
            EmployeesButton.BackColor = Color.White;
            EmployeesButton.FillColor = Color.White;
            EmployeesButton.ForeColor = Color.Black;
            EmployeesButton.Image = Properties.Resources.EmployeesBlack; // Icon lúc chưa click
        }
        public void NavigationButton_Click(object sender, EventArgs e)
        {
            ResetAllButton(); // Đặt lại toàn bộ trạng thái của các nút

            Guna.UI2.WinForms.Guna2Button ClickedButton = (Guna.UI2.WinForms.Guna2Button)sender;
            ClickedButton.FillColor = Color.FromArgb(255, 128, 0);
            ClickedButton.ForeColor = Color.White;
            //Dashboard
            if (ClickedButton.Name == "DashboardButton")
            {
                ClickedButton.Image = Properties.Resources.DashboardWhite;
                FrmDashboard frmDashboard = _scope.Resolve<FrmDashboard>();
                OpenChildForm(frmDashboard); // Mở FormDashBoard
            }
            //Menu
            else if (ClickedButton.Name == "MenuButton")
            {
                ClickedButton.Image = Properties.Resources.Menuwhite;
                FrmMenu frmMenu = _scope.Resolve<FrmMenu>();
                OpenChildForm(frmMenu); // Mở FormMenu
            }
            //Order
            else if (ClickedButton.Name == "OrderButton")
            {
                ClickedButton.Image = Properties.Resources.OrderWhite;
                FrmOrder frmOrder = _scope.Resolve<FrmOrder>();
                OpenChildForm(frmOrder); // Mở FormOrder
            }
            //Table
            else if (ClickedButton.Name == "TableButton")
            {
                ClickedButton.Image = Properties.Resources.TableWhite;
                FormTable frmTable = _scope.Resolve<FormTable>();
                OpenChildForm(frmTable); // Mở FormTable
            }
            //Customer
            else if (ClickedButton.Name == "CustomerButton")
            {
                ClickedButton.Image = Properties.Resources.CustomerWhite;
                FrmCustomer frmCustomer = _scope.Resolve<FrmCustomer>();
                OpenChildForm(frmCustomer);// Mở FormCustomer
            }
            //Reports 
            else if (ClickedButton.Name == "ReportsButton")
            {
                ClickedButton.Image = Properties.Resources.ReportWhite;
                FrmReport frmReport = _scope.Resolve<FrmReport>();
                OpenChildForm(frmReport);// Mở FormReports
            }
            //Employees
            else if (ClickedButton.Name == "EmployeesButton")
            {
                ClickedButton.Image = Properties.Resources.EmployeesWhite;
                FrmEmployee frmEmployee = _scope.Resolve<FrmEmployee>();
                OpenChildForm(frmEmployee);// Mở FormEmployees
            }

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin formLogin = _scope.Resolve<FrmLogin>();
            formLogin.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
