using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Implementations;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.AdminControl
{
    public partial class FormOrder : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        UC_CreateOrder _ucCreateOrder;
        UC_ViewDetails _ucViewDetails;
        Guna2Panel _overlayPanel; // Làm tối nền
        public UC_OrderItem _uc_OrderItem;
        public FormOrder(ILifetimeScope scope, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            LoadAllOrders();
            _orderDetailsRepository = orderDetailsRepository;
        }
        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            _ucCreateOrder = _scope.Resolve<UC_CreateOrder>();
            _ucCreateOrder.OnOrderCreated += HandleOrderCreated;
            _ucCreateOrder.OnOrderCreated += (s, newOrder) =>
            {
                LoadAllOrders();
            };
            this.Controls.Add(_ucCreateOrder);
            _ucCreateOrder.Location = new Point(
                 (this.Width - _ucCreateOrder.Width) / 2,
                 (this.Height - _ucCreateOrder.Height) / 2
            );
            _ucCreateOrder.BringToFront();
        }
        private void HandleOrderCreated(object sender, Orders orderData)
        {
            UC_OrderItem orderItem = _scope.Resolve<UC_OrderItem>();
            orderItem.SetOrderData(orderData); // thêm dữ liệu vào 
            orderItem.OnViewDetailsClicked += HandleViewDetailsClicked;// Đăng kí sự kiện xem chi tiết

            FlowLayoutOrder.Controls.Add(orderItem);

            Control ctrl = sender as Control; // Đặt tính hiệu 

            if (ctrl != null)
            {
                this.Controls.Remove(ctrl);   // Gỡ nó ra khỏi Form cha
                ctrl.Dispose(); // Hủy nó đi cho nhẹ bộ nhớ
            }
        }
        private async void HandleViewDetailsClicked(object sender, Orders orderData)
        {
            //Khởi tạo UserControl xem chi tiết
            _ucViewDetails = new UC_ViewDetails();
            var listMonAn = await _orderDetailsRepository.GetDetailsByOrderIdAsync(orderData.Id);
            //Truyền dữ liệu vào 
            _ucViewDetails.LoadDetailData(orderData, listMonAn);

            //Thêm vào Form cha
            this.Controls.Add(_ucViewDetails);

            //Canh giữa màn hình
            _ucViewDetails.Location = new Point(
                 (this.Width - _ucViewDetails.Width) / 2,
                 (this.Height - _ucViewDetails.Height) / 2
            );
            _ucViewDetails.BringToFront();
        }
        private async void HandleAddFoodClicked(object sender, Orders orderData)
        {
            // 1. Tạo UC CreateOrder (nhưng dùng để thêm món)
            var ucAddMore = _scope.Resolve<UC_CreateOrder>();

            // 2. Chuyển sang chế độ "Thêm món" (Truyền đơn hàng cũ vào)
            // Hàm SetModeAddFood này bạn phải viết trong UC_CreateOrder như hướng dẫn trước
            ucAddMore.SetModeAddFood(orderData);

            // 3. Đăng ký sự kiện: Khi lưu xong -> Load lại danh sách
            ucAddMore.OnOrderCreated += (s, updatedOrder) =>
            {
                LoadAllOrders(); // Tải lại toàn bộ để cập nhật tổng tiền mới
            };

            // 4. Hiển thị form lên
            this.Controls.Add(ucAddMore);
            ucAddMore.BringToFront();

            // Căn giữa màn hình
            ucAddMore.Location = new Point(
                (this.Width - ucAddMore.Width) / 2,
                (this.Height - ucAddMore.Height) / 2
            );
        }

        private void SearchOrderTBox1_Enter(object sender, EventArgs e)
        {
            if (SearchOrderTBox1.PlaceholderText == "Số bàn...")
            {
                SearchOrderTBox1.Text = "";
            }
        }

        private void SearchOrderTBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchOrderTBox1.Text))
            {
                SearchOrderTBox1.PlaceholderText = "Số bàn...";
            }
        }
    }
}
