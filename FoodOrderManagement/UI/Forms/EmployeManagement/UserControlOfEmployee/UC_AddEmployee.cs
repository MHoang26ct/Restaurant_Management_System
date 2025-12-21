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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Mở lên thì focus ngay vào dòng đầu
            this.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Kiểm tra nếu phím bấm là ESC
            if (keyData == Keys.Escape)
            {
                // Gọi hàm đóng 
                ExitButton_Click(null, null);
                return true;
            }
            // Tạo vòng lặp tab không cho tab nhảy lung tung ra ngoài form cha
            if (keyData == Keys.Tab && ConfirmButton.Focused)
            {
                // Ép nhảy về ô đầu tiên (TableID)
                NameTbox.Focus();
                NameTbox.Select();

                // Trả về true để chặn Windows không tự nhảy đi lung tung nữa
                return true;
            }

            // Nếu không phải các phím trên thì hành xử như bình thường
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ConfirmButton_Enter(object sender, EventArgs e)
        {
            ConfirmButton.FillColor = Color.Chocolate;
            ConfirmButton.FillColor2 = Color.FromArgb(255, 128, 0);
        }

        private void ConfirmButton_Leave(object sender, EventArgs e)
        {
            ConfirmButton.FillColor = Color.FromArgb(255, 128, 0);
            ConfirmButton.FillColor2 = Color.Chocolate;
        }
    }
}
