using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_CreateOrder : UserControl
    {
        private readonly ILifetimeScope _scope;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly ICustomersRepository _customersRepository;
        public event EventHandler<Orders> OnOrderCreated; // Thông báo cho cha khi có order được tạo
        public UC_CreateOrder(ILifetimeScope scope, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, ICustomersRepository customersRepository)
        {
            InitializeComponent();
            _scope = scope;
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _customersRepository = customersRepository;
            ThemDongMonAn();
        }

        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // chặn chữ 
        }
    }
}
