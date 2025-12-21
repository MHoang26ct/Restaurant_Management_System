using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_OrderItem : UserControl
    {
        private Orders _currentOrderData;
        private readonly ICustomersRepository _customersRepository;
        public event EventHandler<Orders> OnViewDetailsClicked;
        public event EventHandler<Orders> OnAddFoodClicked;
        public event EventHandler<Orders> OnStatusChanged;

        public UC_OrderItem(ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _customersRepository = customersRepository;
        }

        //Sự kiện thêm món ăn vào orderItem
        private void AddFoodInOrderItem_Click(object sender, EventArgs e)
        {
            OnAddFoodClicked?.Invoke(this, _currentOrderData);
        }

       //Sự kiện thay đổi trạng thái thanh toán
        private void PaymentStatusCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PaymentStatusCBox.SelectedIndex == 0)
            {
                _currentOrderData.CheckoutTime = null;
            }
            // Nếu chọn "Đã hoàn thành" (Index 1) -> Gán thời gian hiện tại (nếu chưa có)
            else if (PaymentStatusCBox.SelectedIndex == 1)
            {
                if (_currentOrderData.CheckoutTime == null)
                {
                    _currentOrderData.CheckoutTime = DateTime.Now;
                }
            }
            UpdateUIStyle(PaymentStatusCBox.SelectedIndex);
            OnStatusChanged?.Invoke(this, _currentOrderData);
        }

        //Sự kiện nhấn vào nút thêm món ăn
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            if (_currentOrderData != null)
            {
                OnAddFoodClicked?.Invoke(this, _currentOrderData);
            }
        }
        //Sự kiện nhấn nút xem chi tiết
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
    }
}
