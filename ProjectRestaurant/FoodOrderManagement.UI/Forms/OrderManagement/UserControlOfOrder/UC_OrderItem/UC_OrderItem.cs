using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectRestaurant.FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_OrderItem : UserControl
    {
        private OrderModel _currentOrderData;
        public event EventHandler<OrderModel> OnViewDetailsClicked;
        public UC_OrderItem()
        {
            InitializeComponent();
        }
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
        public void SetOrderData(OrderModel order)
        {
            _currentOrderData = order;
            OrderIDLabel.Text = order.OrderId;
            NameCustomerLabel.Text = order.CustomerName;
            TableIDLabel.Text = "Bàn " + order.TableNumber;
            TimeOrderLabel.Text = order.OrderDate.ToString("dd/MM/yyyy HH:mm");
            TotalMoneyLabel.Text = "Tổng: " + order.TotalPrice.ToString("N0") + " VND";
            TotalItemsLabel.Text = order.TotalItems + " món";
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
    }
}
