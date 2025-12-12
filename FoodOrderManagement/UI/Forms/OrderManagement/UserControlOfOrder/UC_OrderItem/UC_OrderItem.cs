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
            if (PaymentStatusCBox.SelectedIndex == 0)
            {
                PaymentStatusCBox.ForeColor = Color.Red;
                PaymentStatusCBox.BorderColor = Color.Red;
                StatusBackgroundColor.CustomBorderColor = Color.Red;
                TotalMoneyLabel.ForeColor = Color.Red;
            }
            else if (PaymentStatusCBox.SelectedIndex == 1)
            {
                PaymentStatusCBox.ForeColor = Color.LimeGreen;
                PaymentStatusCBox.BorderColor = Color.LimeGreen;
                StatusBackgroundColor.CustomBorderColor = Color.LimeGreen;
                TotalMoneyLabel.ForeColor = Color.LimeGreen;
            }
        }

        private void TotalItemsLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
