using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FoodOrderManagement.UI.Forms.EmployeManagement.UserControlOfEmployee
{
    public partial class UC_AddEmployee : UserControl
    {
        public delegate void OnAddEmployeeHandler(string ten, string sdt, string email, string vitri, string ngay);
        public event OnAddEmployeeHandler OnAddClicked; // Sự kiện xác nhận
        public UC_AddEmployee()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (OnAddClicked != null)
            {
                OnAddClicked.Invoke(
                    NameTbox.Text,
                    PhoneNumberTBox.Text,
                    EmailTbox.Text,
                    PositionTBox.Text,
                    HireDateDTP.Value.ToString("dd/MM/yyyy")
                );
            }
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void SetModeEdit(string name, string phone, string email, string position, string date)
        {
            TitleLabel.Text = "CẬP NHẬT THÔNG TIN";
            // Điền dữ liệu cũ vào
            NameTbox.Text = name;
            PhoneNumberTBox.Text = phone;
            EmailTbox.Text = email;
            PositionTBox.Text = position;
            // Xử lý ngày tháng (cần try-catch để tránh lỗi format)
            try { HireDateDTP.Value = DateTime.ParseExact(date, "dd/MM/yyyy", null); } catch { }

            // KHÓA các ô không cho sửa
            NameTbox.Enabled = false;      // Khóa tên    
            HireDateDTP.Enabled = false; // Khóa ngày

            // 4. MỞ các ô cho phép sửa
            PhoneNumberTBox.Enabled = true;
            EmailTbox.Enabled = true;
            PositionTBox.Enabled = true;
            PhoneNumberTBox.Focus(); // Đưa con trỏ chuột vào sđt
        }
    }
}
