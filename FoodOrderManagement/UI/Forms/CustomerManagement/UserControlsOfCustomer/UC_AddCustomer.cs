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
        //Khai báo 2 sự kiện: Thêm mới và Cập nhật
        public event EventHandler<Customers> OnCustomerAdded;
        public event EventHandler<Customers> OnCustomerUpdated;
        public event EventHandler OnCancelClicked;
        private readonly ICustomersRepository _customersRepository;
        //Biến để lưu khách hàng đang sửa (nếu có)
        private Customers _editingCustomer = null;

        public UC_AddCustomer(ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _customersRepository = customersRepository;
        }

        public void SetEditMode(Customers customer)
        {
            _editingCustomer = customer; // Lưu lại khách đang sửa

            TitleLable.Text = "CẬP NHẬT THÔNG TIN"; // Đổi tiêu đề
            ConfirmButton.Text = "Lưu Thay Đổi";      // Đổi tên nút

            // Đổ dữ liệu cũ vào ô nhập
            CustomerNameTBox.Text = customer.FullName;
            PhoneNumberTBox.Text = customer.PhoneNumber;
            EmailTBox.Text = customer.Email;

            // Ngày tháng chỉ hiển thị, không cho sửa (tùy nghiệp vụ)
            if (customer.LastVisitDate != DateTime.MinValue)
                DateTimePicker.Value = customer.LastVisitDate;
            DateTimePicker.Enabled = false;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            // Bắn sự kiện ra ngoài để Form cha tắt Popup đi
            OnCancelClicked?.Invoke(this, EventArgs.Empty);

            // Hoặc tự hủy chính mình (nếu Form cha không handle sự kiện Cancel)
            this.Dispose();
        }

        // Đã sửa lại logic: Tên thì không chặn chữ
        // (Nếu bạn muốn chặn số ở ô Tên thì dùng !char.IsLetter)

        // Sự kiện chặn chữ cho Số điện thoại
        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và phím điều khiển (xóa, enter...)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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

    }
}