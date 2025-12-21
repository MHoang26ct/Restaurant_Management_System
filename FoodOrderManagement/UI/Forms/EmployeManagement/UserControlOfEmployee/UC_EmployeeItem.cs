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
    public partial class UC_EmployeeItem : UserControl
    {
        public event EventHandler<Employee> OnEditClicked;
        public event EventHandler<Employee> OnDeleteClicked;
        private Employee _currentEmp;
        public UC_EmployeeItem()
        {
            InitializeComponent();
        }

        private void UC_EmployeeItem_Load(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                // Set chiều rộng = chiều rộng của cha
                this.Width = this.Parent.ClientSize.Width;

                this.Width = this.Parent.ClientSize.Width - 20;
            }
        }
        public void SetData(Employee emp)
        {
            _currentEmp = emp; // Lưu lại để dùng sau

            NameLabel.Text = emp.FullName;
            PhoneNumberLabel.Text = emp.PhoneNumber;
            EmailLabel.Text = emp.Email;
            PositionLabel.Text = emp.Position;
            HireDateLabel.Text = emp.HireDate.ToString("dd/MM/yyyy");

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditClicked?.Invoke(this, _currentEmp);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
                OnDeleteClicked?.Invoke(this, _currentEmp);
        }
    }
}
