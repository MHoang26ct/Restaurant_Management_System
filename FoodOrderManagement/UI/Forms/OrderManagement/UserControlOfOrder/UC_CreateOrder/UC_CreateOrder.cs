using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_CreateOrder : UserControl
    {
        public event EventHandler<OrderModel> OnOrderCreated; // Thông báo cho cha khi có order được tạo
        public UC_CreateOrder()
        {
            InitializeComponent();
        }

        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            UC_AddFoodOrder NewItem = new UC_AddFoodOrder();
            NewItem.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            ListFoodFlowLayout.Controls.Add(NewItem);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private bool KiemTraDauVao()
        {
            // Check 1: Chưa nhập tên khách hàng
            if (string.IsNullOrWhiteSpace(CustomerNameTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CustomerNameTBox.Focus(); // Đưa con trỏ chuột về ô này để nhập luôn
                return false;
            }
            return true;
        }
        private void CreateOrderButton_Click(object sender, EventArgs e)
        {
            if (KiemTraDauVao() == false) return;
            MessageBox.Show("Tạo đơn hàng thành công!", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);

            OrderModel newOrder = new OrderModel();
            newOrder.OrderId = "Đơn Hàng #" + new Random().Next(1000, 9999);

            // Nối dữ liệu nhập vào database 
            newOrder.CustomerName = CustomerNameTBox.Text;
            newOrder.TableNumber = TableID_NBox.Value.ToString();
            newOrder.OrderDate = DateTime.Now;

            // xài logic tính cái này rồi thêm vào 
            newOrder.TotalItems = 5; 
            newOrder.TotalPrice = 500000;

            OnOrderCreated?.Invoke(this, newOrder);
            
        }
    }
}
