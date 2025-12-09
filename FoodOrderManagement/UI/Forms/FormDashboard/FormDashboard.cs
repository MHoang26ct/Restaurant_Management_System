using FoodOrderManagement.UI.Forms.MenuManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System.Globalization; // Xài tiếng việt cho ngày 
namespace FoodOrderManagement.AdminControl
{
    public partial class FormDashboard : Form
    {
        // Lưu lại toàn bộ control với địa chỉ của chúng ban đầu
        private Dictionary<Control, Padding> DiOriginalLocations = new Dictionary<Control, Padding>();
        private readonly ILifetimeScope _scope;
        public FormDashboard(ILifetimeScope scope)
        {

            InitializeComponent();
            TimeShow();
            _scope = scope;
            //
            // MenuPicture
            //
            MenuPicture.Controls.Add(BlurryMenuPanel);
            BlurryMenuPanel.Dock = DockStyle.Fill;
            //
            // OrderPicture
            //
            OrderPicture.Controls.Add(BlurryOrderPanel);
            BlurryOrderPanel.Dock = DockStyle.Fill;


            //
            // TablePicture
            //
            TablePicture.Controls.Add(BlurryTablePanel);
            BlurryTablePanel.Dock = DockStyle.Fill;
                                                    //   
            // CustomerPicture
                                                       // 
            CustomerPicture.Controls.Add(BlurryCustomerPanel);
            BlurryCustomerPanel.Dock = DockStyle.Fill;
        //
            // ReportPicture
        //
            ReportPicture.Controls.Add(BlurryReportPanel);
            BlurryReportPanel.Dock = DockStyle.Fill;
            //
            // EmployeePicture
            //
            EmployeePicture.Controls.Add(BlurryEmployeePanel);
            BlurryEmployeePanel.Dock = DockStyle.Fill;
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            Control MainContainer = FindHoverContainer((Control)sender); // HoverContainer

            if (MainContainer != null && DiOriginalLocations.ContainsKey(MainContainer)) // Có HoverContainer và
                                                                                         // HoverCointainer của Control có
                                                                                         // trong Dictionary
            {
                Padding OriginalLocation = DiOriginalLocations[MainContainer];
                Point MousePosition = MainContainer.PointToClient(Cursor.Position); // Địa chỉ chuột đang trỏ đến

        private void TimeShow()
                {
            DateTime now = DateTime.Now;
            TimeLabel.Text = now.ToString("HH:mm:ss tt");
            DateLabel.Text = now.ToString("dddd, dd/MM/yyyy", new CultureInfo("vi-VN"));
        }
        //
        // Lấy thời gian đồng hồ
        //
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeShow();
        }
        //
        // Sự kiện click Ảnh Menu
        //
        private void MenuPicture_Click(object sender, EventArgs e)
        {
            FrmMenu FormMenu = _scope.Resolve<FrmMenu>();
            FormMain.instance.MenuButton.PerformClick(); // Chỉnh modifiers của Menubutton thành public rồi gọi 
        }
        //
        // Sự kiện click Ảnh Order
        //
        private void OrderPicture_Click(object sender, EventArgs e)
        {
            FormOrder FormOrder = new FormOrder();
            FormMain.instance.OrderButton.PerformClick();
        }
        //
        // Sự kiện click Ảnh Table
        //
        private void TablePicture_Click(object sender, EventArgs e)
        {
            FormTable FormTable = new FormTable();
            FormMain.instance.TableButton.PerformClick();
        }
        //
        // Sự kiện click Ảnh Customer
        //
        private void CustomerPicture_Click(object sender, EventArgs e)
        {
            FormCustomer FormCustomer = new FormCustomer();
            FormMain.instance.CustomerButton.PerformClick();
        }
        //
        // Sự kiện click Ảnh Report
        //
        private void ReportPicture_Click(object sender, EventArgs e)
        {
            FormReport FormReport = new FormReport();
            FormMain.instance.ReportsButton.PerformClick();
        }
        //
        // Sự kiện click Ảnh Employee
        //
        private void EmployeePicture_Click(object sender, EventArgs e)
        {
            FormEmployee FormEmployee = new FormEmployee();
            FormMain.instance.EmployeesButton.PerformClick();
        }
    }
}
