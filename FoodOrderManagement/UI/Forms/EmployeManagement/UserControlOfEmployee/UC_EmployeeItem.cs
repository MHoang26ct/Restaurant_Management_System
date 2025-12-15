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
        public event EventHandler<UC_EmployeeItem> OnEditClicked;
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
        public void SetData(string name, string phone, string email, string position, string date)
        {
            NameLabel.Text = name;
            PhoneNumberLabel.Text = phone;
            EmailLabel.Text = email;
            PositionLabel.Text = position;
            HireDateLabel.Text = date;

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditClicked?.Invoke(this, this);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Parent.Controls.Remove(this);
            }
        }
        public string GetTen() => NameLabel.Text;
        public string GetSDT() => PhoneNumberLabel.Text;
        public string GetEmail() => EmailLabel.Text;
        public string GetViTri() => PositionLabel.Text;
        public string GetNgay() => HireDateLabel.Text;
    }
}
