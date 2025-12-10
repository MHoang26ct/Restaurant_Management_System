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
        private readonly ILifetimeScope _scope;
        private readonly IOrdersRepository _ordersRepository;
        UC_CreateOrder _ucCreateOrder;
        UC_ViewDetails _ucViewDetails;
        Guna2Panel _overlayPanel; // Làm tối nền
        public UC_OrderItem _uc_OrderItem;
        public FormOrder(ILifetimeScope scope, IOrdersRepository ordersRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            LoadAllOrders();
        }
        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            _ucCreateOrder = _scope.Resolve<UC_CreateOrder>();
            _ucCreateOrder.OnOrderCreated += HandleOrderCreated;
            _ucCreateOrder.OnOrderCreated += (s, newOrder) =>
            {
                LoadAllOrders() ;
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
            UC_OrderItem orderItem = new UC_OrderItem();

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
        private void HandleViewDetailsClicked(object sender, Orders orderData)
        {
            //Khởi tạo UserControl xem chi tiết
            _ucViewDetails = new UC_ViewDetails();

            //Truyền dữ liệu vào 
            _ucViewDetails.LoadDetailData(orderData);

            //Thêm vào Form cha
            this.Controls.Add(_ucViewDetails);

            //Canh giữa màn hình
            _ucViewDetails.Location = new Point(
                 (this.Width - _ucViewDetails.Width) / 2,
                 (this.Height - _ucViewDetails.Height) / 2
            );
            _ucViewDetails.BringToFront();
        }

        private void SearchOrderTBox1_Enter(object sender, EventArgs e)
        {
            if (SearchOrderTBox1.PlaceholderText == "Số hóa đơn hoặc tên khác hàng...")
            {
                SearchOrderTBox1.Text = "";
            }
        }

        private void SearchOrderTBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchOrderTBox1.Text))
            {
                SearchOrderTBox1.PlaceholderText = "Số hóa đơn hoặc tên khác hàng...";
            }
        }
    }
}
