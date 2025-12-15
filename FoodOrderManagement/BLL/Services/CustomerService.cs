using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;

namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private List<Customers> _originalCustomerList = new List<Customers>();
        private async void LoadCustomerList()
        {
            try
            {
                _originalCustomerList = await _customersRepository.GetAllCustomersAsync();
                if (SumCustomer != null)
                {
                    SumCustomer.Text = _originalCustomerList.Count.ToString();
                }
                RenderCustomerList(_originalCustomerList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }
        private void RenderCustomerList(List<Customers> listToShow)
        {
            // 1. Tạm dừng vẽ để đỡ giật màn hình
            FlowLayoutCustomer.SuspendLayout();
            FlowLayoutCustomer.Controls.Clear();

            // 2. Duyệt danh sách cần hiện
            foreach (var cus in listToShow)
            {
                AddCustomerItemToUI(cus); // Hàm này bạn đã có sẵn, giữ nguyên
            }

            // 3. Cho phép vẽ lại
            FlowLayoutCustomer.ResumeLayout();
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
                // === TRƯỜNG HỢP THÊM MỚI ===
                Customers newCus = new Customers
                {
                    FullName = CustomerNameTBox.Text,
                    PhoneNumber = PhoneNumberTBox.Text,
                    Email = EmailTBox.Text,
                    LastVisitDate = DateTimePicker.Value,
                    TotalVisits = 0,
                    TotalSpent = 0,
                    CustomerRank = "Regular"
                };

                int newId = await _customersRepository.AddCustomerAsync(newCus);
                newCus.Id = newId;

                OnCustomerAdded?.Invoke(this, newCus); // Báo Form cha: Đã thêm xong
                MessageBox.Show("Thêm thành công!");
            }
            else
            {
                // === TRƯỜNG HỢP CẬP NHẬT ===
                // Cập nhật giá trị mới vào biến tạm
                _editingCustomer.FullName = CustomerNameTBox.Text;
                _editingCustomer.PhoneNumber = PhoneNumberTBox.Text;
                _editingCustomer.Email = EmailTBox.Text;
                // Không cập nhật TotalSpent và Rank ở đây (giữ nguyên cái cũ)

                await _customersRepository.UpdateCustomerInfoAsync(_editingCustomer);

                OnCustomerUpdated?.Invoke(this, _editingCustomer); // Báo Form cha: Đã sửa xong
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