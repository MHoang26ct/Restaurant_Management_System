using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;

namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private List<Customers> _originalCustomerList = new List<Customers>();
        public async void LoadCustomerList()
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
                AddCustomerItemToUI(cus); 
            }

            // 3. Cho phép vẽ lại
            FlowLayoutCustomer.ResumeLayout();
        }
        private void AddCustomerItemToUI(Customers cusData)
        {
            var newItem = _scope.Resolve<UC_CustomerItem>(); 

            newItem.SetCustomerData(cusData);
            newItem.OnEditClicked += HandleEditCustomer;
            newItem.OnDeleteClicked += HandleDeleteCustomer;

            FlowLayoutCustomer.Controls.Add(newItem);
            FlowLayoutCustomer.Controls.SetChildIndex(newItem, 0);
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
            if (string.IsNullOrEmpty(CustomerNameTBox.Text) || string.IsNullOrEmpty(PhoneNumberTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên và số điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(EmailTBox.Text) && !IsValidEmail(EmailTBox.Text))
            {
                MessageBox.Show("Email không đúng định dạng (VD: abc@gmail.com). Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EmailTBox.Focus(); 
                return;
            }
            //Kiểm tra xem đang THÊM hay đang SỬA
            if (_editingCustomer == null)
            {
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

                OnCustomerAdded?.Invoke(this, newCus); 
                MessageBox.Show("Thêm thành công!");
            }
            else
            {
                _editingCustomer.FullName = CustomerNameTBox.Text;
                _editingCustomer.PhoneNumber = PhoneNumberTBox.Text;
                _editingCustomer.Email = EmailTBox.Text;
                await _customersRepository.UpdateCustomerInfoAsync(_editingCustomer);
                OnCustomerUpdated?.Invoke(this, _editingCustomer); 
                MessageBox.Show("Cập nhật thành công!");
            }
        }

    }
}

