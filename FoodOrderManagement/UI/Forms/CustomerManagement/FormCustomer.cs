using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer;
using FoodOrderManagement.UI.Forms.MenuManagement;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
namespace FoodOrderManagement.AdminControl
{

    public partial class FormCustomer : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly ICustomersRepository _customersRepository;
        UC_AddCustomer _ucAddCustomer;
        OverlayBackground _overlayBackground;
        public FormCustomer(ILifetimeScope scope, ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _scope = scope;
            _customersRepository = customersRepository;
            _overlayBackground = new OverlayBackground(); // Khởi tạo overlay
            LoadCustomerList();
        }

        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            _overlayBackground.Show(this);
            _ucAddCustomer = _scope.Resolve<UC_AddCustomer>();
            //ĐĂNG KÝ SỰ KIỆN: Khi bên kia bấm "Xác nhận"
            _ucAddCustomer.OnCustomerAdded += (s, newCustomerData) =>
            {
                //LoadCustomerList();
                // a. Tạo thẻ item mới để hiển thị
                UC_CustomerItem newItem = new UC_CustomerItem();

                // b. Đổ dữ liệu vừa nhập vào thẻ đó
                newItem.SetCustomerData(newCustomerData);
                // Đăng ký: Nếu bấm Sửa ở thẻ này -> Gọi hàm HandleEditCustomer
                newItem.OnEditClicked += HandleEditCustomer;

                // Đăng ký: Nếu bấm Xóa ở thẻ này -> Gọi hàm HandleDeleteCustomer
                newItem.OnDeleteClicked += HandleDeleteCustomer;

                // c. Thêm thẻ vào FlowLayout
                FlowLayoutCustomer.Controls.Add(newItem);

                // d. Đưa thẻ mới lên đầu danh sách 
                FlowLayoutCustomer.Controls.SetChildIndex(newItem, 0);

                // e. Dọn dẹp popup (Tắt popup, tắt nền tối)
                HandleClosePopup(_ucAddCustomer);
            };
            _ucAddCustomer.Disposed += (s, args) =>
            {
                _overlayBackground.Hide(this);
            };
            this.Controls.Add(_ucAddCustomer);
            _ucAddCustomer.Location = new Point(
                (this.Width - _ucAddCustomer.Width) / 2,
                (this.Height - _ucAddCustomer.Height) / 2);

            _ucAddCustomer.BringToFront();
        }
        private void HandleEditCustomer(object sender, Customers cusToEdit)
        {
            _overlayBackground.Show(this);

            // 2. Tạo UC Edit
            var ucEdit = _scope.Resolve<UC_AddCustomer>();

            // 3. QUAN TRỌNG: Kích hoạt chế độ Sửa và nạp dữ liệu cũ
            ucEdit.SetEditMode(cusToEdit);

            // 4. Xử lý sự kiện: Khi Sửa Xong
            ucEdit.OnCustomerUpdated += (s, updatedCus) =>
            {
                // Cách đơn giản nhất: Load lại toàn bộ danh sách để cập nhật giao diện
                LoadCustomerList();

                // Đóng Popup
                HandleClosePopup(ucEdit);
            };

            // 5. Xử lý sự kiện: Khi bấm nút X hoặc Hủy
            ucEdit.OnCancelClicked += (s, e) =>
            {
                HandleClosePopup(ucEdit);
            };
            ucEdit.Disposed += (s, args) => _overlayBackground.Hide(this); // Phòng hờ

            // 6. Tính toán vị trí hiển thị (giữa màn hình)
            this.Controls.Add(ucEdit);
            ucEdit.Location = new Point(
                (this.Width - ucEdit.Width) / 2,
                (this.Height - ucEdit.Height) / 2);

            ucEdit.BringToFront();
        }


        // --- HÀM XỬ LÝ SỰ KIỆN XÓA ---
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
                    // 1. Gọi Repository xóa trong Database
                    await _customersRepository.DeleteCustomerAsync(cusData.Id); // Cần viết hàm này trong Repo

                    // 2. Xóa trên giao diện
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
        private void HandleClosePopup(Control popup)
        {
            this.Controls.Remove(popup);
            popup.Dispose();
            _overlayBackground.Hide(this);
        }

        private void SearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string keyword = SearchCustomer.Text.Trim().ToLower(); // Chuyển về chữ thường để tìm không phân biệt hoa thường

            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu ô tìm kiếm trống -> Hiển thị lại toàn bộ danh sách gốc
                RenderCustomerList(_originalCustomerList);
            }
            else
            {
                // Dùng LINQ để lọc: Tìm theo Tên HOẶC Số điện thoại
                var filteredList = _originalCustomerList
                    .Where(c => c.FullName.ToLower().Contains(keyword) ||
                                c.PhoneNumber.Contains(keyword))
                    .ToList();

                // Hiển thị danh sách đã lọc
                RenderCustomerList(filteredList);
            }
        }
    }
}
