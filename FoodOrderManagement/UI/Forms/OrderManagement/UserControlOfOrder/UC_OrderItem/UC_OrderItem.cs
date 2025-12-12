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
        private void StatusCBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            string status = StatusCBox.Text;
            switch (status)
            {
                case "Pending":
                    StatusBackgroundColor.CustomBorderColor = Color.Gold;
                    StatusMiniBackGroundColor.BorderColor = Color.DarkGoldenrod;
                    StatusMiniBackGroundColor.FillColor = Color.FromArgb(255, 255, 192);
                    StatusMiniBackGroundColor.FillColor2 = Color.FromArgb(255, 255, 192);
                    StatusLabel.Text = "Pending";
                    StatusLabel.ForeColor = Color.DarkGoldenrod;
                    TotalMoneyLabel.ForeColor = Color.DarkGoldenrod;
                    break;

                case "Completed":
                    StatusBackgroundColor.CustomBorderColor = Color.LimeGreen;
                    StatusMiniBackGroundColor.BorderColor = Color.Green;
                    StatusMiniBackGroundColor.FillColor = Color.FromArgb(192, 255, 192);
                    StatusMiniBackGroundColor.FillColor2 = Color.FromArgb(192, 255, 192);
                    StatusLabel.Text = "Completed";
                    StatusLabel.ForeColor = Color.LimeGreen;
                    TotalMoneyLabel.ForeColor = Color.Green;
                    break;

                case "Cancelled":
                    StatusBackgroundColor.CustomBorderColor = Color.Red;
                    StatusMiniBackGroundColor.BorderColor = Color.Firebrick;
                    StatusMiniBackGroundColor.FillColor = Color.FromArgb(255, 192, 192);
                    StatusMiniBackGroundColor.FillColor2 = Color.FromArgb(255, 192, 192);
                    StatusLabel.Text = "Cancelled";
                    StatusLabel.ForeColor = Color.Firebrick;
                    TotalMoneyLabel.ForeColor = Color.Firebrick;
                    break;
            }
        }

        private void AddFoodInOrderItem_Click(object sender, EventArgs e)
        {
            OnAddFoodClicked?.Invoke(this, _currentOrderData);
        }
    }
}
