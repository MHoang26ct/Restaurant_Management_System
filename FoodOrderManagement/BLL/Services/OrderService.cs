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
        private List<Orders> _allOrders = new List<Orders>();
        public async void LoadAllOrders()
        {
            try
            {
                FlowLayoutOrder.Controls.Clear();
                _allOrders = await _ordersRepository.GetAllOrdersAsync();
                RenderOrderList(_allOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách đơn: " + ex.Message);
            }
        }
        private void RenderOrderList(List<Orders> listToRender)
        {
            FlowLayoutOrder.SuspendLayout(); 
            FlowLayoutOrder.Controls.Clear();

            foreach (var order in listToRender)
            {
                AddOrderToUI(order);
            }
            FlowLayoutOrder.ResumeLayout(); 
        }
        private void SetupFilterControls()
        {
            if (StatusCBox.Items.Count == 0)
            {
                StatusCBox.Items.Add("Tất cả");
                StatusCBox.Items.Add("Đã hoàn thành");
                StatusCBox.Items.Add("Chưa thanh toán"); 
                StatusCBox.SelectedIndex = 0; 
            }
        }
        private void ApplyFilters()
        {
            if (_allOrders == null || _allOrders.Count == 0) return;

            var filteredList = _allOrders.AsEnumerable(); 
            string searchText = SearchOrderTBox1.Text.Trim();
            if (!string.IsNullOrEmpty(searchText) && searchText != "Số bàn...")
            {
                if (int.TryParse(searchText, out int tableId))
                {
                    filteredList = filteredList.Where(o => o.TableId == tableId);
                }
            }
            string selectedStatus = StatusCBox.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "Tất cả")
            {
                if (selectedStatus == "Đã hoàn thành")
                    filteredList = filteredList.Where(o => o.CheckoutTime != null);
                else if (selectedStatus == "Chưa thanh toán")
                    filteredList = filteredList.Where(o => o.CheckoutTime == null);
            }
            RenderOrderList(filteredList.ToList());
        }


        private void AddOrderToUI(Orders order)
        {
            UC_OrderItem item = _scope.Resolve<UC_OrderItem>();
            item.SetOrderData(order);
            item.OnViewDetailsClicked += HandleViewDetailsClicked;
            FlowLayoutOrder.Controls.Add(item);
            item.OnAddFoodClicked += HandleAddFoodClicked;
            item.OnStatusChanged += HandleStatusChanged;
            FlowLayoutOrder.Controls.SetChildIndex(item, 0);
        }
        private async void HandleStatusChanged(object sender, Orders updatedOrder)
        {
            try
            {
                await _ordersRepository.UpdateTimeCheckoutAsync(updatedOrder.Id, updatedOrder.CheckoutTime);
                var orderInList = _allOrders.FirstOrDefault(o => o.Id == updatedOrder.Id);
                if (orderInList != null)
                {
                    orderInList.CheckoutTime = updatedOrder.CheckoutTime;
                }
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message);
            }
        }
    }
}



///-----------------------------------------------------------------------------------------------------------------------------------------------





namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_AddFoodOrder : UserControl
    {
        public event EventHandler OnDeleteRequest;
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDeleteRequest?.Invoke(this, EventArgs.Empty);
        }

        private async Task LoadFoodToComboBox()
        {
            try
            {
                foodlist = await _foodsRepository.GetAllFoodsAsync();
                NameFoodCBox.DataSource = null;
                NameFoodCBox.DisplayMember = "Name";
                NameFoodCBox.ValueMember = "Id";    
                NameFoodCBox.DataSource = foodlist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load món: " + ex.Message);
            }
        }
        public async void SetData(int foodId, int quantity)
        {
            if (NameFoodCBox.DataSource == null || NameFoodCBox.Items.Count == 0)
            {
                await LoadFoodToComboBox();
            }
            if (foodlist != null)
            {
                var selectedFood = foodlist.FirstOrDefault(f => f.Id == foodId);

                if (selectedFood != null)
                {
                    NameFoodCBox.SelectedItem = selectedFood; 
                }
                else
                {

                }
            }
            else
            {
                NameFoodCBox.SelectedValue = foodId;
            }
            QuantityFoodNBox.Value = quantity;
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
            TableID_NBox.Value = oldOrder.TableId;
            TableID_NBox.Enabled = false;
            Customers c = await _customersRepository.GetCustomerByIdAsync(oldOrder.CustomerId);
            if (c != null)
            {
                CustomerNameTBox.Text = c.FullName;
                PhoneNumberTBox.Text = c.PhoneNumber;
            }
            CustomerNameTBox.Enabled = false;
            PhoneNumberTBox.Enabled = false;
            label1.Text = "Cập nhật đơn hàng";
            CreateOrderButton.Text = "Cập Nhật Đơn Hàng";
            ListFoodFlowLayout.Controls.Clear();
            var oldDetails = await _orderDetailsRepository.GetOrderDetailsByOrderIdAsync(oldOrder.Id);

            if (oldDetails != null && oldDetails.Count > 0)
            {
                foreach (var item in oldDetails)
                {
                    var row = _scope.Resolve<UC_AddFoodOrder>();
                    row.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    row.Width = ListFoodFlowLayout.Width - 25;
                    row.OnDeleteRequest += (sender, args) =>
                    {
                        ListFoodFlowLayout.Controls.Remove((Control)sender);
                        ((UserControl)sender).Dispose();
                    };
                    ListFoodFlowLayout.Controls.Add(row);
                    row.SetData(item.FoodId, item.Quantity);

                }
            }
        }
        private void ThemDongMonAn()
        {
            var newItem = _scope.Resolve<UC_AddFoodOrder>();
            newItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            newItem.Width = ListFoodFlowLayout.Width - 25;
            newItem.OnDeleteRequest += (sender, args) =>
            {
                ListFoodFlowLayout.Controls.Remove((Control)sender);
                ((UserControl)sender).Dispose(); 
            };
            ListFoodFlowLayout.Controls.Add(newItem);
        }
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            ThemDongMonAn();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }

        private bool KiemTraDauVao()
        {
            if (string.IsNullOrWhiteSpace(CustomerNameTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomerNameTBox.Focus(); 
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
            newCus.Email = phone + name + "@gmail.com";
            int newId = await _customersRepository.AddCustomerAsync(newCus);
            return newId;
        }
        private async void CreateOrderButton_Click(object sender, EventArgs e)
        {
            if (_currentOrderId == null && KiemTraDauVao() == false) return;

            try
            {
                int targetOrderId;

                if (_currentOrderId == null)
                {
                    string phone = string.IsNullOrEmpty(PhoneNumberTBox.Text) ? "Unknown" : PhoneNumberTBox.Text;
                    int customerId = await GetOrCreateCustomerAsync(CustomerNameTBox.Text, phone);
                    if (customerId <= 0)
                    {
                        MessageBox.Show("Lỗi: Không tạo được khách hàng hợp lệ. Vui lòng kiểm tra lại!");
                        return;
                    }
                    Orders order = new Orders();
                    order.CustomerId = customerId;
                    order.OrderTime = DateTime.Now;
                    order.TableId = (int)TableID_NBox.Value;
                    order.ReservationId = 0;
                    order.NumberOfGuests = 1;
                    order.TotalAmount = 0;   
                    targetOrderId = await _ordersRepository.AddOrderAsync(order);
                    _existingOrderData = order;
                    _existingOrderData.Id = targetOrderId;
                }
                else
                {
                    targetOrderId = _currentOrderId.Value;
                    await _orderDetailsRepository.DeleteAllDetailsByOrderIdAsync(targetOrderId);
                }
                List<orderDetail> details = new List<orderDetail>();
                decimal currentTotal = 0;

                foreach (Control ctrl in ListFoodFlowLayout.Controls)
                {
                    if (ctrl is UC_AddFoodOrder row && row.SelectedFoodId > 0 && row.Quantity > 0)
                    {
                        orderDetail item = new orderDetail
                        {
                            OrderId = targetOrderId,
                            FoodId = row.SelectedFoodId,
                            Quantity = row.Quantity,
                            Notes = ""
                        };
                        details.Add(item);
                        currentTotal += (row.Price * row.Quantity);
                    }
                }
                if (details.Count > 0)
                {
                    await _orderDetailsRepository.AddListOrderDetailAsync(details);
                    if (_existingOrderData != null)
                    {
                        _existingOrderData.TotalAmount = currentTotal;
                        await _ordersRepository.UpdateOrderTotalAsync(targetOrderId, currentTotal);
                    }

                    OnOrderCreated?.Invoke(this, _existingOrderData);
                    MessageBox.Show("Cập nhật đơn hàng thành công!");

                    this.Parent?.Controls.Remove(this);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Đơn hàng phải có ít nhất 1 món!");
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
            TimeOrderLabel.Text = order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoneyLabel.Text = order.TotalAmount.ToString("N0") + " VND";
            TotalItemsLabel.Text = "Tổng tiền: ";
            PaymentStatusCBox.SelectedIndexChanged -= PaymentStatusCBox_SelectedIndexChanged;
            int index = (_currentOrderData.CheckoutTime == null) ? 0 : 1;
            PaymentStatusCBox.SelectedIndex = index;
            UpdateUIStyle(index);
            PaymentStatusCBox.SelectedIndexChanged += PaymentStatusCBox_SelectedIndexChanged;
        }
        private void UpdateUIStyle(int index)
        {
            if (index == 0)
            {
                PaymentStatusCBox.ForeColor = Color.Red;
                PaymentStatusCBox.BorderColor = Color.Red;
                StatusBackgroundColor.CustomBorderColor = Color.Red;

                TotalMoneyLabel.ForeColor = Color.Red;
            }
            else if (index == 1) // Đã hoàn thành
            {
                PaymentStatusCBox.ForeColor = Color.LimeGreen;
                PaymentStatusCBox.BorderColor = Color.LimeGreen;

                StatusBackgroundColor.CustomBorderColor = Color.LimeGreen; 
                TotalMoneyLabel.ForeColor = Color.LimeGreen;
            }
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
            public string TenMon { get; set; } 
            public int SoLuong { get; set; }   
            public decimal DonGia { get; set; }
            public decimal ThanhTien => DonGia * SoLuong; 
        }
        public void LoadDetailData(Orders order, List<OrderDetailDisplay> listMonAn)
        {
            OrderIDLabel.Text ="Đơn hàng #" + order.Id.ToString(); 
            NameCustomerLabel.Text = "Khách hàng: " + order.CustomerId.ToString();
            TableIDLabel.Text = "Bàn " + order.TableId.ToString();
            TimeOrderLabel.Text = "Thời gian: " + order.OrderTime.ToString("dd/MM/yyyy HH:mm");
            TotalMoney.Text = "Tổng tiền: " + order.TotalAmount.ToString("N0") + " VND";
            dgvChiTiet.DataSource = null;
            dgvChiTiet.DataSource = listMonAn;

            // 3. Tinh chỉnh giao diện cột (Tùy chọn cho đẹp)
            if (dgvChiTiet.Columns["TenMon"] != null) dgvChiTiet.Columns["TenMon"].HeaderText = "Tên Món";
            if (dgvChiTiet.Columns["SoLuong"] != null) dgvChiTiet.Columns["SoLuong"].HeaderText = "SL";
            if (dgvChiTiet.Columns["DonGia"] != null)
            {
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0"; 
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