using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer
{
    public partial class UC_AddCustomer : UserControl
    {
        public event EventHandler<Customers> OnCustomerAdded;
        public event EventHandler<Customers> OnCustomerUpdated;
        public event EventHandler OnCancelClicked;
        private readonly ICustomersRepository _customersRepository;
        private Customers _editingCustomer = null;

        public UC_AddCustomer(ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _customersRepository = customersRepository;
        }

        //Xử lý sự kiện nút thoát được nhấn
        private void ExitButton_Click(object sender, EventArgs e)
        {
            OnCancelClicked?.Invoke(this, EventArgs.Empty);
            this.Dispose();
        }

        //Xử lý sự kiện nhập số điện thoại
        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Xử lý sự kiện nhập email
        private void EmailTBox_TextChanged(object sender, EventArgs e)
        {
            if (EmailTBox.Text.Length > 0)
            {
                if (IsValidEmail(EmailTBox.Text))
                {
                    EmailTBox.BorderColor = Color.Green;
                }
                else
                {
                    EmailTBox.BorderColor = Color.Red;
                    EmailTBox.IconRight = null;
                }
            }
            else
            {
                EmailTBox.BorderColor = Color.FromArgb(213, 218, 223);
            }
        }

        //Xử lý sự kiện đưa chuột vào nút xác nhận
        private void ConfirmButton_Enter(object sender, EventArgs e)
        {
            ConfirmButton.FillColor = Color.Chocolate;
            ConfirmButton.FillColor2 = Color.FromArgb(255, 128, 0);
        }

        //Xử lý sự kiện rời chuột khổi nút xác nhận
        private void ConfirmButton_Leave(object sender, EventArgs e)
        {
            ConfirmButton.FillColor = Color.FromArgb(255, 128, 0);
            ConfirmButton.FillColor2 = Color.Chocolate;
        }
    }
}