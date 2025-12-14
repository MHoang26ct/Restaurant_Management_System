using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;

namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private async void LoadCustomerList()
        {
            try
            {
                FlowLayoutCustomer.Controls.Clear(); // Xóa sạch cũ
                var customers = await _customersRepository.GetAllCustomersAsync(); // Cần viết hàm này trong Repo

                foreach (var cus in customers)
                {
                    AddCustomerItemToUI(cus);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }
        private void AddCustomerItemToUI(Customers cusData)
        {
            // Sử dụng Resolve để tạo item (để Autofac quản lý nếu sau này item cần Repo)
            var newItem = _scope.Resolve<UC_CustomerItem>(); // Hoặc new UC_CustomerItem() nếu item không cần DI

            newItem.SetCustomerData(cusData);
            newItem.OnEditClicked += HandleEditCustomer;
            newItem.OnDeleteClicked += HandleDeleteCustomer;

            FlowLayoutCustomer.Controls.Add(newItem);
            FlowLayoutCustomer.Controls.SetChildIndex(newItem, 0); // Đưa lên đầu
        }
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------------------------------


namespace FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer
{
    public partial class UC_AddCustomer : UserControl
    {
        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrEmpty(CustomerNameTBox.Text) || string.IsNullOrEmpty(PhoneNumberTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và số điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(EmailTBox.Text) && !IsValidEmail(EmailTBox.Text))
            {
                MessageBox.Show("Email không đúng định dạng (VD: abc@gmail.com). Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailTBox.Focus(); // Đưa con trỏ chuột về ô Email
                return; // QUAN TRỌNG: Dừng hàm tại đây, không cho chạy xuống code lưu bên dưới
            }
            //Kiểm tra xem đang THÊM hay đang SỬA
            if (_editingCustomer == null)
            {
                // === THÊM MỚI ===
                Customers newCus = new Customers
                {
                    FullName = CustomerNameTBox.Text,
                    PhoneNumber = PhoneNumberTBox.Text,
                    Email = EmailTBox.Text,
                    LastVisitDate = DateTimePicker.Value.Date,
                    TotalVisits = 0,
                    TotalSpent = 0
                };

                // 1. Lưu vào Database trước
                int newId = await _customersRepository.AddCustomerAsync(newCus);
                newCus.Id = newId; // Cập nhật ID mới sinh ra từ DB

                // 2. Bắn sự kiện ra ngoài (kèm ID mới)
                OnCustomerAdded?.Invoke(this, newCus);
                MessageBox.Show("Thêm khách hàng thành công!");
            }
            else
            {
                // === TRƯỜNG HỢP CẬP NHẬT ===

                _editingCustomer.FullName = CustomerNameTBox.Text;
                _editingCustomer.PhoneNumber = PhoneNumberTBox.Text;
                _editingCustomer.Email = EmailTBox.Text;

                // 1. Cập nhật xuống Database
                await _customersRepository.UpdateCustomerInfoAsync(_editingCustomer);

                // 2. Bắn sự kiện ra ngoài
                OnCustomerUpdated?.Invoke(this, _editingCustomer);
                MessageBox.Show("Cập nhật thành công!");
            }
        }
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer
{
    public partial class UC_CustomerItem : UserControl
    {

    }
}