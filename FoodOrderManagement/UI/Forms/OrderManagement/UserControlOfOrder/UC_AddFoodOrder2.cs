using Autofac;
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
    public partial class UC_AddFoodOrder2 : UserControl
    {
        private readonly ILifetimeScope _scope;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly ICustomersRepository _customersRepository;
        public event EventHandler<Orders> OnOrderCreated; // Thông báo cho cha khi có order được tạo
        private int _currentOrderId; // Biến lưu mã đơn hàng đang chỉnh sửa
        public UC_AddFoodOrder2(ILifetimeScope scope, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _customersRepository = customersRepository;
            ThemDongMonAn();
        }
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            ThemDongMonAn();
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
        private async void CreateOrderButton_Click(object sender, EventArgs e)
        {

        }
    }
}
