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

namespace ProjectRestaurant.AdminControl
{
    public partial class FormDashboard : Form 
    {
        // Lưu lại toàn bộ control với địa chỉ của chúng ban đầu
        private Dictionary<Control, Point> DiOriginalLocations = new Dictionary<Control, Point>();
        public FormDashboard()
        {

            InitializeComponent();
            //
            // MenuPicture
            //
            MenuPicture.Controls.Add(BlurryMenuPanel); // Làm tối hình
            BlurryMenuPanel.Dock = DockStyle.Fill;
            BlurryMenuPanel.Controls.Add(MenuGradient);
            BlurryMenuPanel.Controls.Add(MenuLabel);
            MenuGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            MenuLabel.Location = new Point(110, 270);   // Đặt vị trí mới so với cha 

           
            //
            //OrderPicture
            //
            OrderPicture.Controls.Add(BlurryOrderPanel); // Thêm panel làm tối hình 
            BlurryOrderPanel.Dock = DockStyle.Fill; // Chèn full vào cha
            BlurryOrderPanel.Controls.Add(OrderGradient);
            BlurryOrderPanel.Controls.Add(OrderLabel);
            OrderGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            OrderLabel.Location = new Point(93, 270);   // Đặt vị trí mới so với cha 
            //
            //TablePicture
            //
            TablePicture.Controls.Add(BlurryTablePanel); // Thêm panel làm tối hình 
            BlurryTablePanel.Dock = DockStyle.Fill; // Chèn full vào cha
            BlurryTablePanel.Controls.Add(TableGradient);
            BlurryTablePanel.Controls.Add(TableLabel);
            TableGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            TableLabel.Location = new Point(93, 270);   // Đặt vị trí mới so với cha 
            //
            //CustomerPicture
            //
            CustomerPicture.Controls.Add(BlurryCustomerPanel); // Thêm panel làm tối hình 
            BlurryCustomerPanel.Dock = DockStyle.Fill; // Chèn full vào cha
            BlurryCustomerPanel.Controls.Add(CustomerGradient);
            BlurryCustomerPanel.Controls.Add(CustomerLabel);
            CustomerGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            CustomerLabel.Location = new Point(85, 270);   // Đặt vị trí mới so với cha 
            //
            //ReportPicture
            //
            ReportPicture.Controls.Add(BlurryReportPanel); // Thêm panel làm tối hình 
            BlurryReportPanel.Dock = DockStyle.Fill; // Chèn full vào cha
            BlurryReportPanel.Controls.Add(ReportGradient);
            BlurryReportPanel.Controls.Add(ReportLabel);
            ReportGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            ReportLabel.Location = new Point(93, 270);   // Đặt vị trí mới so với cha 
            //
            //EmployeePicture
            //
            EmployeePicture.Controls.Add(BlurryEmployeePanel); // Thêm panel làm tối hình 
            BlurryEmployeePanel.Dock = DockStyle.Fill; // Chèn full vào cha
            BlurryEmployeePanel.Controls.Add(EmployeeGradient);
            BlurryEmployeePanel.Controls.Add(EmployeeLabel);
            EmployeeGradient.Location = new Point(150, 150); // Đặt vị trí mới so với cha 
            EmployeeLabel.Location = new Point(90, 270);   // Đặt vị trí mới so với cha 
        }
        //
        //Hiệu ứng bật lên
        //
        private Control FindHoverContainer(Control StarControl)// Tìm container cha chứa control
        {
            Control Current = StarControl;
            // Duyệt vòng lặp ngược để tìm cha
            while (Current != null)
            {
                if (Current.Tag?.ToString() == "HoverContainer") // Nếu vế trước ? là
                {                                                 // Null thì chương trình        
                    return Current;                               // sẽ trả về null và không gây 
                }                                                 // ra lỗi NullReferenException 
                Current = Current.Parent; // tiếp tuc chạy vòng lặp
            }
            ; // trả về null nếu không tìm thấy
            return null;
        }
        private void Control_MouseEnter(object sender, EventArgs e)
        {
            Control MainContainer = FindHoverContainer((Control)sender); // HoverContainer

            if (MainContainer != null)
            {
                if (!DiOriginalLocations.ContainsKey(MainContainer))
                {
                    DiOriginalLocations[MainContainer] = MainContainer.Location; // Lưu vào dictionary địa chỉ của HoverContainer
                }
                Point OriginalLocation = DiOriginalLocations[MainContainer]; //// Lấy ra vị trí gốc ĐÃ LƯU của HoverContainer từ Dictionary

                if (MainContainer.Location == OriginalLocation) // // Chỉ thực hiện hiệu ứng NẾU vị trí hiện tại
                                                                // của HoverContainer vẫn là vị trí gốc (chưa di chuyển)
                {
                    MainContainer.BringToFront();
                    MainContainer.Location = new Point(OriginalLocation.X, OriginalLocation.Y - 10);
                }
            }
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            Control MainContainer = FindHoverContainer((Control)sender); // HoverContainer

            if (MainContainer != null && DiOriginalLocations.ContainsKey(MainContainer)) // Có HoverContainer và
                                                                                         // HoverCointainer của Control có
                                                                                         // trong Dictionary
            {
                Point OriginalLocation = DiOriginalLocations[MainContainer];
                Point MousePosition = MainContainer.PointToClient(Cursor.Position); // Địa chỉ chuột đang trỏ đến

                if (!MainContainer.ClientRectangle.Contains(MousePosition)) // Nếu chuột nằm ngoài vùng MainContainer
                {
                    MainContainer.Location = OriginalLocation; // Trả về vị trí ban đầu
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateLabel.Text = DateTime.Now.ToString("dddd dd/MM/yyyy");
            TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {

        }

        private void MenuPicture_Click(object sender, EventArgs e)
        {
            FormMenu frmMenu = new FormMenu();
            frmMain.instance.MenuButton.PerformClick(); // Chỉnh modifiers của Menubutton thành public rồi gọi 
        }

        private void OrderPicture_Click(object sender, EventArgs e)
        {
            FormOrder frmOrder = new FormOrder();
            frmMain.instance.OrderButton.PerformClick();
        }

        private void TablePicture_Click(object sender, EventArgs e)
        {
            FormTable frmTable = new FormTable();
            frmMain.instance.TableButton.PerformClick();
        }

        private void CustomerPicture_Click(object sender, EventArgs e)
        {
            FormCustomer frmCustomer = new FormCustomer();
            frmMain.instance.CustomerButton.PerformClick();
        }

        private void ReportPicture_Click(object sender, EventArgs e)
        {
            FormReport frmReport = new FormReport();
            frmMain.instance.ReportsButton.PerformClick();
        }

        private void EmployeePicture_Click(object sender, EventArgs e)
        {
            FormEmployee frmEmployee = new FormEmployee();
            frmMain.instance.EmployeesButton.PerformClick();
        }
    }
}
