using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI.Forms.MenuManagement;
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
using TheArtOfDevHtmlRenderer.Adapters;

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
        }

        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // chặn chữ 
        }
        //
        // TAB INDEX 
        //

        // Sự kiện khi load UC_CreateOrder sẽ focus vào UC này
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Mở lên thì focus ngay vào dòng đầu
            this.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Kiểm tra nếu phím bấm là ESC
            if (keyData == Keys.Escape)
            {
                // Gọi hàm đóng 
                ExitButton_Click(null, null);
                return true;
            }
            // Tạo vòng lặp tab không cho tab nhảy lung tung ra ngoài form cha
            if (keyData == Keys.Tab && CreateOrderButton.Focused)
            {
                // Ép nhảy về ô đầu tiên (TableID)
                TableID_NBox.Focus();
                TableID_NBox.Select();

                // Trả về true để chặn Windows không tự nhảy đi lung tung nữa
                return true;
            }

            // Nếu không phải các phím trên thì hành xử như bình thường
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddFoodButton_Enter(object sender, EventArgs e)
        {
            AddFoodButton.BorderColor = Color.DarkOrange;
            AddFoodButton.FillColor = Color.DarkGray;
        }

        private void AddFoodButton_Leave(object sender, EventArgs e)
        {
            AddFoodButton.BorderColor = Color.Silver;
            AddFoodButton.FillColor = Color.Transparent;
        }

        private void CreateOrderButton_Enter(object sender, EventArgs e)
        {
            CreateOrderButton.FillColor = Color.Chocolate;
            CreateOrderButton.FillColor2 = Color.FromArgb(255, 128, 0);
        }
        private void CreateOrderButton_Leave(object sender, EventArgs e)
        {
            CreateOrderButton.FillColor = Color.FromArgb(255, 128, 0);
            CreateOrderButton.FillColor2 = Color.Chocolate;
        }
    }
}
