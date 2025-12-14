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


        private void AddFoodInOrderItem_Click(object sender, EventArgs e)
        {
            OnAddFoodClicked?.Invoke(this, _currentOrderData);
        }

        private void PaymentStatusCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentOrderData == null) return;

            int index = PaymentStatusCBox.SelectedIndex;
            UpdateUIStyle(index);
            if (index == 0)
            {
                _currentOrderData.CheckoutTime = null;
            }
            else if (index == 1) 
            {
                if (_currentOrderData.CheckoutTime == null)
                    _currentOrderData.CheckoutTime = DateTime.Now;
            }
            OnStatusChanged?.Invoke(this, _currentOrderData);
        }

        private void TotalItemsLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
