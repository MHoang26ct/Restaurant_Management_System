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
    public partial class UC_ViewDetails : UserControl
    {
        public event EventHandler OnCloseClicked;
        public UC_ViewDetails()
        {
            InitializeComponent();
        }
        public void LoadDetailData(OrderModel order)
        {
            OrderIDLabel.Text = order.OrderId; // Ví dụ: Đơn Hàng #3636
            NameCustomerLabel.Text = "Khách hàng: " + order.CustomerName;
            TableIDLabel.Text = "Bàn " + order.TableNumber;
            TimeOrderLabel.Text = "Thời gian: " + order.OrderDate.ToString("dd/MM/yyyy HH:mm");
            TotalMoney.Text = "Tổng: " + order.TotalPrice.ToString("N0") + " VND";
            // lblDetailCount.Text = ...

            // Nếu có danh sách món ăn chi tiết (List<Food>), bạn cũng đổ vào DataGridView ở đây
        }
        private void ClosedButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
