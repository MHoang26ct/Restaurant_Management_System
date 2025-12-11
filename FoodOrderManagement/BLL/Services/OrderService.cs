using Guna.UI2.WinForms;
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
using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;

namespace FoodOrderManagement.AdminControl
{
    public partial class FormOrder : Form
    {

        public async void LoadAllOrders()
        {
            try
            {
                // 1. Xóa hết các control cũ để tránh trùng lặp
                FlowLayoutOrder.Controls.Clear();

                // 2. Lấy danh sách từ Repo (Bạn cần có hàm GetAllOrdersAsync trong Repo)
                var listOrders = await _ordersRepository.GetAllUnpaidOrdersAsync();

                // 3. Duyệt và vẽ từng đơn hàng
                foreach (var order in listOrders)
                {
                    AddOrderToUI(order);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đơn: " + ex.Message);
            }
        }
        private void AddOrderToUI(Orders order)
        {
            // Tạo UserControl hiển thị đơn hàng
            UC_OrderItem item = _scope.Resolve<UC_OrderItem>();

            // Đổ dữ liệu vào
            item.SetOrderData(order);

            // Đăng ký sự kiện xem chi tiết (nếu cần)
            item.OnViewDetailsClicked += HandleViewDetailsClicked;

            // Thêm vào Panel (FlowLayoutPanel)
            FlowLayoutOrder.Controls.Add(item);

            // Đưa đơn mới nhất lên đầu (nếu muốn)
            FlowLayoutOrder.Controls.SetChildIndex(item, 0);
        }
    }
}



///-----------------------------------------------------------------------------------------------------------------------------------------------





namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_AddFoodOrder : UserControl
    {
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private async void LoadFoodToComboBox()
        {
            try
            {
                foodlist = await _foodsRepository.GetAllFoodsAsync();

                NameFoodCBox.DataSource = foodlist;
                NameFoodCBox.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách món ăn: " + ex.Message);
            }
        }
        public int SelectedFoodId
        {
            get
            {
                if (NameFoodCBox.SelectedItem is Foods food)
                {
                    return food.Id;
                }
                return 0;
            }
        }
        public int Quantity
        {
            get
            {
                return (int)QuantityFoodNBox.Value;
            }
        }
        public decimal Price
        {
            get
            {
                if (NameFoodCBox.SelectedItem is Foods selectedFood)
                {
                    return selectedFood.Price;
                }
                return 0;
            }
        }
    }
}

///-----------------------------------------------------------------------------------------------------------------------------------------------


namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_CreateOrder : UserControl
    {
        private void ThemDongMonAn()
        {
            // Resolve UC con từ Scope để nó có đủ Repository
            var newItem = _scope.Resolve<UC_AddFoodOrder>();

            // Căn chỉnh chiều ngang cho đẹp
            newItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            newItem.Width = ListFoodFlowLayout.Width - 25;

            // Thêm vào Layout
            ListFoodFlowLayout.Controls.Add(newItem);
        }
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            ThemDongMonAn();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private bool KiemTraDauVao()
        {
            // Check 1: Chưa nhập tên khách hàng
            if (string.IsNullOrWhiteSpace(CustomerNameTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomerNameTBox.Focus(); // Đưa con trỏ chuột về ô này để nhập luôn
                return false;
            }
            return true;
        }
        private async Task<int> GetOrCreateCustomerAsync(string name, string phone)
        {
            var existingCustomer = await _customersRepository.GetCustomerByNameAndPhoneAsync(name, phone);

            if (existingCustomer != null)
            {
                return existingCustomer.Id;
            }
            Customers newCus = new Customers();
            newCus.PhoneNumber = phone;
            newCus.FullName = name;
            newCus.Email = "123@gmail.com";
            // Hàm AddCustomerAsync phải trả về ID khách mới
            int newId = await _customersRepository.AddCustomerAsync(newCus);
            return newId;
        }
        private async void CreateOrderButton_Click(object sender, EventArgs e)
        {
            if (KiemTraDauVao() == false) return;
            /////////////////////////
            Orders order = new Orders();
            order.ReservationId = 1;
            order.TableId = (int)TableID_NBox.Value;
            order.OrderTime = DateTime.Now;
            //order.CustomerId = 1;
            string phone = "0123456789";
            order.CustomerId = await GetOrCreateCustomerAsync(CustomerNameTBox.Text, phone);
            order.NumberOfGuests = 1;
           // int newOrderId = await _ordersRepository.AddOrderAsync(order);
            decimal totalAmount = 0;
            List<orderDetail> details = new List<orderDetail>();

            foreach (Control ctrl in ListFoodFlowLayout.Controls)
            {
                if (ctrl is UC_AddFoodOrder row && row.SelectedFoodId > 0 && row.Quantity > 0)
                {
                    orderDetail item = new orderDetail
                    {
                        FoodId = row.SelectedFoodId,
                        Quantity = row.Quantity,
                        OrderStatus = "Pending",
                        Notes = string.Empty
                    };
                    details.Add(item);
                    totalAmount += row.Price * row.Quantity;
                }
            }

            if (details.Count > 0)
            {
                order.TotalAmount = totalAmount;
                int newOrderId = await _ordersRepository.AddOrderAsync(order);

                // gán OrderId cho chi tiết
                foreach (var d in details) d.OrderId = newOrderId;
                await _orderDetailsRepository.AddListOrderDetailAsync(details);

                OnOrderCreated?.Invoke(this, order);
                MessageBox.Show($"Tạo đơn thành công! Mã đơn: {newOrderId}\nTổng tiền: {totalAmount:N0} VNĐ");
                
                this.Parent?.Controls.Remove(this);
            }

        }
    }
}



///-----------------------------------------------------------------------------------------------------------------------------------------------




namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_OrderItem : UserControl
    {

        private void ViewDetailsButton_Click(object sender, EventArgs e)
        {
            if (_currentOrderData != null)
            {
                OnViewDetailsClicked?.Invoke(this, _currentOrderData);
            }
            else
            {
                MessageBox.Show("Lỗi: Không tìm thấy dữ liệu đơn hàng!");
            }
        }
        public void SetOrderData(Orders order)
        {
            _currentOrderData = order;
            OrderIDLabel.Text = "#" + order.Id.ToString();
            NameCustomerLabel.Text = order.CustomerId.ToString();
            TableIDLabel.Text = "Bàn " + order.TableId.ToString();
            TimeOrderLabel.Text = order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoneyLabel.Text = order.TotalAmount.ToString("N0") + " VND";
            TotalItemsLabel.Text = "Tổng tiền: ";
        }
    }
}

///-----------------------------------------------------------------------------------------------------------------------------------------------


namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_ViewDetails : UserControl
    {
        public class OrderDetailDisplay
        {
            // Các tên này sẽ trở thành Tiêu đề cột trong DataGridView
            public string TenMon { get; set; }  // Tên món ăn
            public int SoLuong { get; set; }    // Số lượng
            public decimal DonGia { get; set; } // Giá gốc
            public decimal ThanhTien => DonGia * SoLuong; // Tự động tính tổng
        }
        public void LoadDetailData(Orders order, List<OrderDetailDisplay> listMonAn)
        {
            OrderIDLabel.Text ="#" + order.Id.ToString(); // Ví dụ: Đơn Hàng #3636
            NameCustomerLabel.Text = "Khách hàng: " + order.CustomerId.ToString();
            TableIDLabel.Text = "Bàn " + order.TableId.ToString();
            TimeOrderLabel.Text = "Thời gian: " + order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoney.Text = order.TotalAmount.ToString("N0") + " VND";
            dgvChiTiet.DataSource = null; // Reset trước để tránh lỗi
            dgvChiTiet.DataSource = listMonAn;

            // 3. Tinh chỉnh giao diện cột (Tùy chọn cho đẹp)
            if (dgvChiTiet.Columns["TenMon"] != null) dgvChiTiet.Columns["TenMon"].HeaderText = "Tên Món";
            if (dgvChiTiet.Columns["SoLuong"] != null) dgvChiTiet.Columns["SoLuong"].HeaderText = "SL";
            if (dgvChiTiet.Columns["DonGia"] != null)
            {
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ
            }
            if (dgvChiTiet.Columns["ThanhTien"] != null)
            {
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
            // lblDetailCount.Text = ...

            // Nếu có danh sách món ăn chi tiết (List<Food>), bạn cũng đổ vào DataGridView ở đây
        }
        private void ClosedButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
