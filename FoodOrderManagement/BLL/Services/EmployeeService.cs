using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.EmployeManagement.UserControlOfEmployee;

namespace FoodOrderManagement.AdminControl
{
    public partial class FormEmployee : Form
    {
        private List<Employee> _originalList = new List<Employee>();

        //Lấy thông tin nhân viên tự Database
        public async Task LoadListEmployee()
        {
            try
            {
                _originalList = await _employeesRepository.GetAllEmployeesAsync();
                RenderList(_originalList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        //Hiển thị thông tin các nhân viên lên UI
        private void RenderList(List<Employee> list)
        {
            FlowLayoutEmployee.SuspendLayout();
            FlowLayoutEmployee.Controls.Clear();

            foreach (var emp in list)
            {
                var item = new UC_EmployeeItem(); 
                item.SetData(emp);
                item.OnEditClicked += Item_OnEditClicked;
                item.OnDeleteClicked += Item_OnDeleteClicked;
                item.Width = FlowLayoutEmployee.ClientSize.Width - 25;
                FlowLayoutEmployee.Controls.Add(item);
            }
            FlowLayoutEmployee.ResumeLayout();
        }

        //Hiển thị pop up để cập nhập, thêm nhân viên
        private void ShowPopup(Employee empToEdit)
        {
            _overlayBackground.Show(this);
            var popup = new UC_AddEmployee(); 
            if (empToEdit != null)
            {
                popup.SetModeEdit(empToEdit);
            }

            this.Controls.Add(popup);
            popup.Location = new Point((this.Width - popup.Width) / 2, (this.Height - popup.Height) / 2);
            popup.BringToFront();
            popup.OnSaveClicked += async (sender, infoFromForm) =>
            {
                try
                {
                    if (empToEdit == null)
                    {
                        await _employeesRepository.AddEmployeeAsync(infoFromForm);
                        MessageBox.Show("Thêm nhân viên thành công!");
                    }
                    else
                    {
                        await _employeesRepository.UpdateEmployeeAsync(infoFromForm);
                        MessageBox.Show("Cập nhật thành công!");
                    }

                    LoadListEmployee(); 
                    ClosePopup(popup);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            };

            popup.OnCancelClicked += (s, args) => ClosePopup(popup);
        }

        //Sự kiện nhấn nút thêm nhân viên
        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            ShowPopup(null);
        }

        //Sự kiện nhấn nút sửa thông tin nhân viên
        private void Item_OnEditClicked(object sender, Employee empToEdit)
        {
            ShowPopup(empToEdit);
        }

        //Đóng pop up
        private void ClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }

        //Sự kiện xóa nhân viên
        private async void Item_OnDeleteClicked(object sender, Employee emp)
        {
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa nhân viên {emp.FullName}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _employeesRepository.DeleteEmployeeByNameAndPhoneAsync(emp.FullName, emp.PhoneNumber);
                    LoadListEmployee();
                    MessageBox.Show("Đã xóa nhân viên!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa: " + ex.Message);
                }
            }
        }

        //Sự kiện thay đổi ở thanh tìm kiếm
        private void SearchEmployeeTBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = SearchEmployeeTBox1.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                RenderList(_originalList);
            }
            else
            {
                var filteredList = _originalList.Where(emp =>
                    emp.FullName.ToLower().Contains(keyword) || 
                    emp.PhoneNumber.Contains(keyword)            
                ).ToList();
                RenderList(filteredList);
            }
        }
    }
}