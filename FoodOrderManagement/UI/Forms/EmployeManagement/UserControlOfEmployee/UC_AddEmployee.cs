using FoodOrderManagement.DAL.Models.Entities;
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
        public event EventHandler<Employee> OnSaveClicked;
        public event EventHandler OnCancelClicked;
        // public event OnAddEmployeeHandler OnAddClicked; // Sự kiện xác nhận
        private Employee _editingEmp = null;

        public UC_AddEmployee()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            Employee newEmp = new Employee
            {
                FullName = NameTbox.Text,
                PhoneNumber = PhoneNumberTBox.Text,
                Email = EmailTbox.Text,
                Position = PositionTBox.Text,
                HireDate = HireDateDTP.Value
            };

            OnSaveClicked?.Invoke(_editingEmp, newEmp);
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            OnCancelClicked?.Invoke(this, EventArgs.Empty);
        }
        public void SetModeEdit(Employee emp)
        {
            _editingEmp = emp; 

            TitleLabel.Text = "CẬP NHẬT THÔNG TIN";
            NameTbox.Text = emp.FullName;
            PhoneNumberTBox.Text = emp.PhoneNumber;
            EmailTbox.Text = emp.Email;
            PositionTBox.Text = emp.Position;
            HireDateDTP.Value = emp.HireDate;
            // Tùy bạn có muốn khóa Tên hay Ngày không, thường thì cho sửa hết trừ ID
            // NameTbox.Enabled = false;
            NameTbox.Enabled = false;
            PhoneNumberTBox.Enabled = false;
        }
    }
}
