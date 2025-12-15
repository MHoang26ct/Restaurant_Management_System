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
        private async void LoadListEmployee()
        {
            try
            {
                // Gọi Repo lấy dữ liệu
                _originalList = await _employeesRepository.GetAllEmployeesAsync();

                // Vẽ lên giao diện
                RenderList(_originalList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        // --- 2. HÀM VẼ GIAO DIỆN (Tách riêng để dùng cho cả Tìm kiếm) ---
        private void RenderList(List<Employee> list)
        {
            FlowLayoutEmployee.SuspendLayout();
            FlowLayoutEmployee.Controls.Clear();

            foreach (var emp in list)
            {
                // Tạo Item mới
                var item = new UC_EmployeeItem(); // Hoặc _scope.Resolve<UC_EmployeeItem>() nếu cần

                // Đổ dữ liệu vào item
                item.SetData(emp);

                // Đăng ký sự kiện SỬA và XÓA
                item.OnEditClicked += Item_OnEditClicked;
                item.OnDeleteClicked += Item_OnDeleteClicked;

                // Chỉnh độ rộng
                item.Width = FlowLayoutEmployee.ClientSize.Width - 25;

                FlowLayoutEmployee.Controls.Add(item);
            }

            FlowLayoutEmployee.ResumeLayout();
        }

        // --- 3. XỬ LÝ NÚT THÊM ---
        private void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            ShowPopup(null); // Truyền null tức là đang THÊM
        }

        // --- 4. XỬ LÝ SỰ KIỆN SỬA (Từ Item bắn ra) ---
        private void Item_OnEditClicked(object sender, Employee empToEdit)
        {
            ShowPopup(empToEdit); // Truyền có dữ liệu tức là đang SỬA
        }

        // --- 5. HÀM HIỂN THỊ POPUP (Dùng chung cho cả Thêm và Sửa) ---
        private void ShowPopup(Employee empToEdit)
        {
            _overlayBackground.Show(this);
            var popup = new UC_AddEmployee(); // Hoặc _scope.Resolve<UC_AddEmployee>();

            // Nếu empToEdit != null nghĩa là đang SỬA
            if (empToEdit != null)
            {
                popup.SetModeEdit(empToEdit);
            }

            this.Controls.Add(popup);
            popup.Location = new Point((this.Width - popup.Width) / 2, (this.Height - popup.Height) / 2);
            popup.BringToFront();

            // --- XỬ LÝ KHI BẤM LƯU ---
            popup.OnSaveClicked += async (sender, infoFromForm) =>
            {
                try
                {
                    if (empToEdit == null)
                    {
                        // === THÊM MỚI (Vì lúc gọi hàm ShowPopup mình truyền null) ===
                        await _employeesRepository.AddEmployeeAsync(infoFromForm);
                        MessageBox.Show("Thêm nhân viên thành công!");
                    }
                    else
                    {
                        // === CẬP NHẬT (Vì empToEdit có dữ liệu) ===
                        // Gọi hàm Update chỉ nhận 1 tham số như bạn yêu cầu
                        // (Lưu ý: Tên và SĐT trong infoFromForm phải giống hệt DB)
                        await _employeesRepository.UpdateEmployeeAsync(infoFromForm);
                        MessageBox.Show("Cập nhật thành công!");
                    }

                    LoadListEmployee(); // Tải lại danh sách
                    ClosePopup(popup);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            };

            popup.OnCancelClicked += (s, args) => ClosePopup(popup);
        }

        private void ClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }

        // --- 6. XỬ LÝ SỰ KIỆN XÓA ---
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
                    // Gọi Repository xóa (theo Tên và SĐT vì không có ID)
                    await _employeesRepository.DeleteEmployeeByNameAndPhoneAsync(emp.FullName, emp.PhoneNumber);

                    // Tải lại danh sách
                    LoadListEmployee();
                    MessageBox.Show("Đã xóa nhân viên!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa: " + ex.Message);
                }
            }
        }

        private void SearchEmployeeTBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = SearchEmployeeTBox1.Text.Trim().ToLower();

            // 2. Kiểm tra
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu ô tìm kiếm trống -> Hiển thị lại toàn bộ danh sách gốc
                RenderList(_originalList);
            }
            else
            {
                // 3. Lọc danh sách: Tìm theo Tên HOẶC Số điện thoại
                // (Dùng LINQ Where để lọc)
                var filteredList = _originalList.Where(emp =>
                    emp.FullName.ToLower().Contains(keyword) ||  // Tên chứa từ khóa
                    emp.PhoneNumber.Contains(keyword)            // Hoặc SĐT chứa từ khóa
                ).ToList();

                // 4. Vẽ danh sách đã lọc lên màn hình
                RenderList(filteredList);
            }
        }
    }
}