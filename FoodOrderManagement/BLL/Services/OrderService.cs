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

            item.OnAddFoodClicked += HandleAddFoodClicked;

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
        private int? _currentOrderId = null;
        private Orders _existingOrderData = null;

        public async void SetModeAddFood(Orders oldOrder)
        {
            _currentOrderId = oldOrder.Id;
            _existingOrderData = oldOrder;

            // Điền lại thông tin cũ lên giao diện
            TableID_NBox.Value = oldOrder.TableId;

            // Khóa các ô không được sửa
            TableID_NBox.Value = oldOrder.TableId;
            TableID_NBox.Enabled = false;
            Customers c = await _customersRepository.GetCustomerByIdAsync(oldOrder.CustomerId);
            CustomerNameTBox.Text = c.FullName;
            CustomerNameTBox.Enabled = false;
            PhoneNumberTBox.Text = c.PhoneNumber;
            PhoneNumberTBox.Enabled = false;

            // Đổi tên nút bấm cho hợp lý
            // Giả sử nút tạo đơn tên là CreateOrderButton
            CreateOrderButton.Text = "Lưu Món Mới";
        }
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
            this.Dispose(); //Hủy
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
            newCus.Email = "";
            // Hàm AddCustomerAsync phải trả về ID khách mới
            int newId = await _customersRepository.AddCustomerAsync(newCus);
            return newId;
        }

        private async void CreateOrderButton_Click(object sender, EventArgs e)
        {
            // Validate: Nếu là tạo mới thì phải check input, còn thêm món thì không cần check tên khách
            if (_currentOrderId == null && KiemTraDauVao() == false) return;

            try
            {
                int targetOrderId;

                // --- BƯỚC 1: XÁC ĐỊNH ORDER ID ---
                if (_currentOrderId == null)
                {
                    // TẠO ORDER MỚI
                    Orders order = new Orders();
                    // Nếu không có tính năng đặt bàn, hãy để null (nếu int? trong DB) hoặc 0
                    order.ReservationId = 0;
                    order.TableId = (int)TableID_NBox.Value;
                    order.OrderTime = DateTime.Now;
                    // Xử lý khách hàng
                    string phone = string.IsNullOrEmpty(PhoneNumberTBox.Text) ? "Unknown" : PhoneNumberTBox.Text;
                    order.CustomerId = await GetOrCreateCustomerAsync(CustomerNameTBox.Text, phone);

                    // Nên lấy từ UI, tạm thời để 1
                    order.NumberOfGuests = 1;
                    order.TotalAmount = 0; // Mới tạo thì tiền là 0, tính sau

                    targetOrderId = await _ordersRepository.AddOrderAsync(order);

                    // Gán lại ID và dữ liệu để dùng cho việc hiển thị
                    order.Id = targetOrderId;
                    _existingOrderData = order;
                }
                else
                {
                    // DÙNG ORDER CŨ
                    targetOrderId = _currentOrderId.Value;
                }

                // --- BƯỚC 2: TẠO DANH SÁCH CHI TIẾT ---
                List<orderDetail> details = new List<orderDetail>();
                decimal currentBatchTotal = 0; // Tổng tiền của đợt thêm món này

                foreach (Control ctrl in ListFoodFlowLayout.Controls)
                {
                    if (ctrl is UC_AddFoodOrder row && row.SelectedFoodId > 0 && row.Quantity > 0)
                    {
                        orderDetail item = new orderDetail
                        {
                            OrderId = targetOrderId, // ✅ Gán ID ngay tại đây luôn
                            FoodId = row.SelectedFoodId,
                            Quantity = row.Quantity,
                            OrderStatus = "Pending",
                            Notes = string.Empty
                        };
                        details.Add(item);
                        currentBatchTotal += (row.Price * row.Quantity);
                    }
                }

                // --- BƯỚC 3: LƯU VÀO DB ---
                if (details.Count > 0)
                {
                    // 1. Lưu chi tiết món ăn
                    await _orderDetailsRepository.AddListOrderDetailAsync(details);

                    // 2. Cập nhật lại tổng tiền cho Order chính (Quan trọng)
                    if (_existingOrderData != null)
                    {
                        // Cộng thêm tiền mới vào tiền cũ
                        _existingOrderData.TotalAmount += currentBatchTotal;

                        // GỌI REPO ĐỂ UPDATE TIỀN VÀO DB (Bạn cần viết hàm này bên Repo nếu chưa có)
                        // await _ordersRepository.UpdateOrderTotalAsync(targetOrderId, _existingOrderData.TotalAmount);
                    }

                    // 3. Thông báo ra ngoài
                    OnOrderCreated?.Invoke(this, _existingOrderData);

                    MessageBox.Show($"Lưu thành công!\nMã đơn: {targetOrderId}\nTiền đợt này: {currentBatchTotal:N0} đ\nTổng cộng: {_existingOrderData?.TotalAmount:N0} đ");

                    this.Parent?.Controls.Remove(this);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất 1 món ăn!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
        public async void SetOrderData(Orders order)
        {
            _currentOrderData = order;
            OrderIDLabel.Text = "Đơn Hàng #" + order.Id.ToString();
            Customers cus = await _customersRepository.GetCustomerByIdAsync(order.CustomerId);
            string namecus = cus.FullName;
            InfomationLabel.Text = "Bàn " + order.TableId.ToString() + " - Khách hàng: " + namecus;
            //NameCustomerLabel.Text = namecus;
            //TableIDLabel.Text = "Bàn " + order.TableId.ToString();
            TimeOrderLabel.Text = order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoneyLabel.Text = order.TotalAmount.ToString("N0") + " VND";
            TotalItemsLabel.Text = "Tổng tiền: ";
        }
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            if (_currentOrderData != null)
            {
                OnAddFoodClicked?.Invoke(this, _currentOrderData);
            }
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
            OrderIDLabel.Text ="Đơn hàng #" + order.Id.ToString(); // Ví dụ: Đơn Hàng #3636
            NameCustomerLabel.Text = "Khách hàng: " + order.CustomerId.ToString();
            TableIDLabel.Text = "Bàn " + order.TableId.ToString();
            TimeOrderLabel.Text = "Thời gian: " + order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoney.Text = "Tổng tiền: " + order.TotalAmount.ToString("N0") + " VND";
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
        }
        private void ClosedButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}

///-----------------------------------------------------------------------------------------------------------------------------------------------