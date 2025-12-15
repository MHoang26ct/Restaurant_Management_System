using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using FoodOrderManagement.UI.Forms.EmployeManagement;
using FoodOrderManagement.UI.Forms.EmployeManagement.UserControlOfEmployee;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FoodOrderManagement.AdminControl
{
    public partial class FormEmployee : Form
    {
        OverlayBackground _overlayBackground;
        public FormEmployee()
        {
            InitializeComponent();
        }

        private void FlowLayoutEmployee_Resize(object sender, EventArgs e)
        {
            foreach (Control item in FlowLayoutEmployee.Controls)
            {
                item.Width = FlowLayoutEmployee.ClientSize.Width;
            }
        }

        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            _overlayBackground = new OverlayBackground(); // thêm làm tối nền
            _overlayBackground.Show(this);
            UC_AddEmployee _ucAddEmployee = new UC_AddEmployee();

            // Canh chỉnh vị trí cho nó nằm CHÍNH GIỮA Form
            _ucAddEmployee.Location = new Point(
                (this.ClientSize.Width - _ucAddEmployee.Width) / 2,
                (this.ClientSize.Height - _ucAddEmployee.Height) / 2
            );
            this.Controls.Add(_ucAddEmployee);
            _ucAddEmployee.BringToFront();
            _ucAddEmployee.Focus();
            // Tắt làm tối nền khi đóng 
            _ucAddEmployee.Disposed += (s, args) =>
            {
                _overlayBackground.Hide(this);
            };
            // Sự kiện thêm nhân viên
            _ucAddEmployee.OnAddClicked += (name, phone, email, position, date) =>
            {
                // Tạo dòng nhân viên mới (UC_EmployeeItem)
                UC_EmployeeItem newItem = new UC_EmployeeItem();
                // Gọi hàm gán dữ liệu
                newItem.SetData(name, phone, email, position, date);
                // Đăng kí sự kiện edit
                newItem.OnEditClicked += Item_OnEditClicked;
                //Thêm vào danh sách
                FlowLayoutEmployee.Controls.Add(newItem);
                //Tắt cái popup nhập liệu
                this.Controls.Remove(_ucAddEmployee);
                _ucAddEmployee.Dispose();
            };
        }
        // Hàm xử lí sửa nhân viên
        private void Item_OnEditClicked(object sender, UC_EmployeeItem itemEdit)
        {
            _overlayBackground = new OverlayBackground(); // thêm làm tối nền
            _overlayBackground.Show(this);

            // Hiện  _ucEditEmployee lên (giống hệt lúc thêm, nhưng logic khác)
            UC_AddEmployee _ucEditEmployee = new UC_AddEmployee();

            _ucEditEmployee.Location = new Point(
                (this.ClientSize.Width - _ucEditEmployee.Width) / 2,
                (this.ClientSize.Height - _ucEditEmployee.Height) / 2
            );

            this.Controls.Add(_ucEditEmployee);
            _ucEditEmployee.BringToFront();
            // Tắt làm tối nền khi đóng 
            _ucEditEmployee.Disposed += (s, args) =>
            {
                _overlayBackground.Hide(this);
            };
            // Đẩy dữ liệu cũ vào itemEdit và Khóa các ô không cần thiết
            _ucEditEmployee.SetModeEdit(
                itemEdit.GetTen(),
                itemEdit.GetSDT(),
                itemEdit.GetEmail(),
                itemEdit.GetViTri(),
                itemEdit.GetNgay()
            );

            // C. Xử lý khi bấm LƯU (Cập nhật lại dòng cũ)
            _ucEditEmployee.OnAddClicked += (newName, newPhone, newEmail, newPossition, newDate) =>
            {
                // Cập nhật lại giao diện của dòng đó
                itemEdit.SetData(newName, newPhone, newEmail, newPossition, newDate);

                // TODO: Nếu có SQL, hãy viết câu lệnh Update Database ở đây

                // Đóng popup
                this.Controls.Remove(_ucEditEmployee);
                _ucEditEmployee.Dispose();
            };
        }
    }
}
