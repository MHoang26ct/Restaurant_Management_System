using Autofac;
using FoodOrderManagement.AdminControl;
using FoodOrderManagement.UI.Forms.MenuManagement;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FoodOrderManagement
{
    public partial class FormMain : Form
    {
        private readonly ILifetimeScope _scope;

        // Inject tất cả các Form con vào đây để quản lý Singleton
        private readonly FormDashboard _formDashboard;
        private readonly FrmMenu _formMenu;
        private readonly FormOrder _formOrder;
        private readonly FormTable _formTable;
        private readonly FormCustomer _formCustomer;
        private readonly FormReservation _formReservation;
        private readonly FormEmployee _formEmployee;

        private Form CurrentChildForm;
        public static FormMain instance { get; private set; }

        // Constructor nhận tất cả các Form từ Autofac
        public FormMain(
            ILifetimeScope scope,
            FormDashboard formDashboard,
            FrmMenu frmMenu,
            FormOrder formOrder,
            FormTable formTable,
            FormCustomer formCustomer,
            FormReservation formReservation,
            FormEmployee formEmployee)
        {
            InitializeComponent();
            _scope = scope;
            _formDashboard = formDashboard;
            _formMenu = frmMenu;
            _formOrder = formOrder;
            _formTable = formTable;
            _formCustomer = formCustomer;
            _formReservation = formReservation;
            _formEmployee = formEmployee;

            instance = this;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Mặc định mở Dashboard khi vừa vào
            DashboardButton.PerformClick();
        }

        public void OpenChildForm(Form childForm)
        {
            // Nếu form đang chọn đã hiển thị rồi thì không làm gì cả
            if (CurrentChildForm == childForm) return;

            // Ẩn form cũ thay vì Close để giữ RAM ổn định (Singleton)
            if (CurrentChildForm != null)
            {
                CurrentChildForm.Hide();
            }

            CurrentChildForm = childForm;

            // Thiết lập các thuộc tính để nhúng vào Panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Chỉ thêm vào Controls nếu chưa có
            if (!MainPanel.Controls.Contains(childForm))
            {
                MainPanel.Controls.Add(childForm);
            }

            childForm.BringToFront();
            childForm.Show();

            // Nếu bạn có Interface IRefreshable, có thể gọi cập nhật dữ liệu ở đây:
            // if (childForm is IRefreshable f) f.RefreshData();
        }

        public void NavigationButton_Click(object sender, EventArgs e)
        {
            ResetAllButton();
            Guna2Button clickedButton = (Guna2Button)sender;

            // Cập nhật giao diện nút được nhấn
            clickedButton.FillColor = Color.FromArgb(255, 128, 0);
            clickedButton.ForeColor = Color.White;

            // Điều hướng dựa trên tên nút
            switch (clickedButton.Name)
            {
                case "DashboardButton":
                    clickedButton.Image = Properties.Resources.DashboardWhite;
                    OpenChildForm(_formDashboard);
                    break;
                case "MenuButton":
                    clickedButton.Image = Properties.Resources.Menuwhite;
                    OpenChildForm(_formMenu);
                    break;
                case "OrderButton":
                    clickedButton.Image = Properties.Resources.OrderWhite;
                    OpenChildForm(_formOrder);
                    break;
                case "TableButton":
                    clickedButton.Image = Properties.Resources.TableWhite;
                    OpenChildForm(_formTable);
                    break;
                case "CustomerButton":
                    clickedButton.Image = Properties.Resources.CustomerWhite;
                    OpenChildForm(_formCustomer);
                    break;
                case "ReservationButton":
                    clickedButton.Image = Properties.Resources.ReservedWhite;
                    OpenChildForm(_formReservation);
                    break;
                case "EmployeesButton":
                    clickedButton.Image = Properties.Resources.EmployeesWhite;
                    OpenChildForm(_formEmployee);
                    break;
            }
        }

        public void ResetAllButton()
        {
            // Hàm này giữ nguyên logic của bạn hoặc tối ưu bằng cách dùng List<Guna2Button>
            DashboardButton.FillColor = Color.White;
            DashboardButton.ForeColor = Color.Black;
            DashboardButton.Image = Properties.Resources.DashboardBlack;

            MenuButton.FillColor = Color.White;
            MenuButton.ForeColor = Color.Black;
            MenuButton.Image = Properties.Resources.Menublack;

            OrderButton.FillColor = Color.White;
            OrderButton.ForeColor = Color.Black;
            OrderButton.Image = Properties.Resources.OderBlack;

            TableButton.FillColor = Color.White;
            TableButton.ForeColor = Color.Black;
            TableButton.Image = Properties.Resources.TableBlack;

            CustomerButton.FillColor = Color.White;
            CustomerButton.ForeColor = Color.Black;
            CustomerButton.Image = Properties.Resources.CustomerBlack;

            ReservationButton.FillColor = Color.White;
            ReservationButton.ForeColor = Color.Black;
            ReservationButton.Image = Properties.Resources.Reserved;

            EmployeesButton.FillColor = Color.White;
            EmployeesButton.ForeColor = Color.Black;
            EmployeesButton.Image = Properties.Resources.EmployeesBlack;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            // Resolve FormLogin vì thường Login không cần Singleton (để reset trạng thái mỗi lần đăng xuất)
            var formLogin = _scope.Resolve<FormLogin>();
            formLogin.Show();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}