using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using System.Text.RegularExpressions;

namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private List<Customers> _originalCustomerList = new List<Customers>();

        //Lấy danh sách các khách hàng trong database
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
        //Hiển thị các khách hàng vào UI
        private void RenderCustomerList(List<Customers> listToShow)
        {
            FlowLayoutCustomer.SuspendLayout();
            FlowLayoutCustomer.Controls.Clear();
            foreach (var cus in listToShow)
            {
                AddCustomerItemToUI(cus); 
            }
            FlowLayoutCustomer.ResumeLayout();
        }

        //Thêm item hiển thị thông tin của khách hàng vào UI
        private void AddCustomerItemToUI(Customers cusData)
        {
            var newItem = _scope.Resolve<UC_CustomerItem>(); 

            newItem.SetCustomerData(cusData);
            newItem.OnEditClicked += HandleEditCustomer;
            newItem.OnDeleteClicked += HandleDeleteCustomer;

            FlowLayoutCustomer.Controls.Add(newItem);
            FlowLayoutCustomer.Controls.SetChildIndex(newItem, 0);
        }

        //Xử lý sự kiện chỉnh sửa thông tin khách hàng
        private void HandleEditCustomer(object sender, Customers cusToEdit)
        {
            _overlayBackground.Show(this);
            var ucEdit = _scope.Resolve<UC_AddCustomer>();
            ucEdit.SetEditMode(cusToEdit);
            ucEdit.OnCustomerUpdated += (s, updatedCus) =>
            {
                LoadCustomerList();
                HandleClosePopup(ucEdit);
            };
            ucEdit.OnCancelClicked += (s, e) =>
            {
                HandleClosePopup(ucEdit);
            };
            ucEdit.Disposed += (s, args) => _overlayBackground.Hide(this);
            this.Controls.Add(ucEdit);
            ucEdit.Location = new Point(
                (this.Width - ucEdit.Width) / 2,
                (this.Height - ucEdit.Height) / 2);

            ucEdit.BringToFront();
        }

        //Xử lý sự kiện xóa khách hàng
        private async void HandleDeleteCustomer(object sender, Customers cusData)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng {cusData.FullName}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await _customersRepository.DeleteCustomerAsync(cusData.Id);
                    UC_CustomerItem itemToRemove = sender as UC_CustomerItem;
                    if (itemToRemove != null)
                    {
                        FlowLayoutCustomer.Controls.Remove(itemToRemove);
                        itemToRemove.Dispose();
                    }
                    MessageBox.Show("Đã xóa khách hàng!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa (có thể khách đang có đơn hàng). Lỗi: " + ex.Message);
                }
            }
        }

        //Xử lý sự kiện đóng pop up
        private void HandleClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }
    }
}

//------------------------------------------------------------------------------------------------------------------------------------------------------------------


namespace FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer
{
    public partial class UC_AddCustomer : UserControl
    {

        //Xử lý sự kiện nhấn nút xác nhận
        private async void ConfirmButton_Click(object sender, EventArgs e)
        {

            // Kiểm tra thông tin có đầy đủ không: 
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

            //Trường hợp chưa tồn tại khách hàng : tạo mới
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

            //Trường hợp đã tồn tại : chỉnh sửa 
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

        //Thay đổi UC tạo mới thành cập nhật
        public void SetEditMode(Customers customer)
        {
            _editingCustomer = customer;

            TitleLable.Text = "CẬP NHẬT THÔNG TIN";
            ConfirmButton.Text = "Lưu Thay Đổi";
            CustomerNameTBox.Text = customer.FullName;
            PhoneNumberTBox.Text = customer.PhoneNumber;
            EmailTBox.Text = customer.Email;
            if (customer.LastVisitDate != DateTime.MinValue)
                DateTimePicker.Value = customer.LastVisitDate;
            DateTimePicker.Enabled = false;
        }

        //Kiểm tra email có hợp lệ không
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        //Sự kiện OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Focus();
        }

        //Thay đổi nút khi được tab vào
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                ExitButton_Click(null, null);
                return true;
            }
            if (keyData == Keys.Tab && ConfirmButton.Focused)
            {
                CustomerNameTBox.Focus();
                CustomerNameTBox.Select();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}

