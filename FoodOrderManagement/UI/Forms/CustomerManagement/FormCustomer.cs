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
        private void HandleEditCustomer(object sender, Customers cusData)
        {
            _overlayBackground.Show(this);

            // Tạo form nhập liệu (dùng lại UC_AddCustomer)
            UC_AddCustomer _ucEditCustomer = _scope.Resolve<UC_AddCustomer>();

            // Chuyển sang chế độ Sửa (Điền dữ liệu cũ vào)
            // (Bạn cần chắc chắn bên UC_AddCustomer đã có hàm SetEditMode như bài trước)
            _ucEditCustomer.SetEditMode(cusData);

            // Đăng ký sự kiện: Khi update xong
            _ucEditCustomer.OnCustomerUpdated += (s, updatedCus) =>
            {
                //Load lại toàn bộ danh sách (Dễ nhất)
                // LoadData(); 
                // Khi nào nối database thì làm nhé
                // Cập nhật lại giao diện của cái thẻ đang sửa (Tối ưu hơn)
                UC_CustomerItem itemBeingEdited = sender as UC_CustomerItem;
                if (itemBeingEdited != null)
                {
                    itemBeingEdited.SetCustomerData(updatedCus);
                }

                HandleClosePopup(_ucEditCustomer);
            };

            // Xử lý đóng form
            _ucEditCustomer.Disposed += (s, args) => _overlayBackground.Hide(this);

            this.Controls.Add(_ucEditCustomer);
            _ucEditCustomer.Location = new Point((this.Width - _ucEditCustomer.Width) / 2, (this.Height - _ucEditCustomer.Height) / 2);
            _ucEditCustomer.BringToFront();
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
                    await _customersRepository.DeleteCustomerAsync  (cusData.Id); // Cần viết hàm này trong Repo

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
    }
}
