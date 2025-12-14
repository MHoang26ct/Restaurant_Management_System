using FoodOrderManagement.DAL.Models.Entities;
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

        //Biến để lưu khách hàng đang sửa (nếu có)
        private Customers _editingCustomer = null;

        public UC_AddCustomer()
        {
            InitializeComponent();
        }

        public void SetEditMode(Customers customer)
        {
            // Lưu lại khách hàng đang sửa
            _editingCustomer = customer;

            ConfirmButton.Text = "Lưu Thay Đổi";
            TitleLable.Text = "Cập Nhật Khách Hàng";

            // Điền dữ liệu cũ vào các ô
            CustomerNameTBox.Text = customer.FullName;
            PhoneNumberTBox.Text = customer.PhoneNumber;
            EmailTBox.Text = customer.Email;

            // Xử lý ngày tháng (Chỉ hiển thị, không cho sửa ngày tạo)
            if (customer.LastVisitDate != DateTime.MinValue)
            {
                DateTimePicker.Value = customer.LastVisitDate;
            }
            DateTimePicker.Enabled = false; // Khóa lại không cho sửa ngày này
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

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrEmpty(CustomerNameTBox.Text) || string.IsNullOrEmpty(PhoneNumberTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và số điện thoại!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(EmailTBox.Text) && !IsValidEmail(EmailTBox.Text))
            {
                MessageBox.Show("Email không đúng định dạng (VD: abc@gmail.com). Vui lòng kiểm tra lại!","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailTBox.Focus(); // Đưa con trỏ chuột về ô Email
                return; // QUAN TRỌNG: Dừng hàm tại đây, không cho chạy xuống code lưu bên dưới
            }
            //Kiểm tra xem đang THÊM hay đang SỬA
            if (_editingCustomer == null)
            {
                // === TRƯỜNG HỢP THÊM MỚI ===
                Customers newCus = new Customers
                {
                    FullName = CustomerNameTBox.Text,
                    PhoneNumber = PhoneNumberTBox.Text,
                    Email = EmailTBox.Text,
                    LastVisitDate = DateTimePicker.Value.Date,
                    TotalVisits = 0,
                    TotalSpent = 0
                };

                // Bắn sự kiện Thêm
                OnCustomerAdded?.Invoke(this, newCus);
            }
            else
            {
                // === TRƯỜNG HỢP CẬP NHẬT ===

                // Cập nhật dữ liệu mới vào biến _editingCustomer
                _editingCustomer.FullName = CustomerNameTBox.Text;
                _editingCustomer.PhoneNumber = PhoneNumberTBox.Text;
                _editingCustomer.Email = EmailTBox.Text;

                // Lưu ý: Không sửa LastVisitDate, TotalVisits, TotalSpent ở đây

                // Bắn sự kiện Cập nhật
                OnCustomerUpdated?.Invoke(this, _editingCustomer);
            }
        }
    }
}