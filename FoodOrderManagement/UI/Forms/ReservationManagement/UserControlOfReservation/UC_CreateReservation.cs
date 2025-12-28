using Autofac;
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

namespace FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation
{
    public partial class UC_CreateReservation : UserControl

    {
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
                ExitButton_Click(null, null);
                return true;
            }
            // Tạo vòng lặp tab không cho tab nhảy lung tung ra ngoài form cha
            if (keyData == Keys.Tab && CreateReservationButton.Focused)
            {
                TableID_NBox.Focus();
                TableID_NBox.Select();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Sự kiện thêm dòng món ăn vào giao diện
        private void AddFoodRowToUI(int foodId = 0, int quantity = 1)
        {
            if (ListFoodFlowLayout == null) return;

            var row = _scope.Resolve<UC_AddFoodOrder>();
            row.Width = ListFoodFlowLayout.Width - 25;
            row.OnDeleteRequest += (sender, e) =>
            {
                ListFoodFlowLayout.Controls.Remove(row);
                row.Dispose();
            };
            if (foodId > 0)
            {
                row.SetData(foodId, quantity);
            }

            ListFoodFlowLayout.Controls.Add(row);
        }

        //Sự kiện nhán nút thêm nón ăn
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (ListFoodFlowLayout == null) { MessageBox.Show("Thiếu FlowLayoutPanel 'pnlFoodList' trên giao diện!"); return; }
            AddFoodRowToUI();
        }

        //Sự kiện chọn hoặc rời nút thêm món ăn, đặt bàn
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
        private void CreateReservationButton_Enter(object sender, EventArgs e)
        {
            CreateReservationButton.FillColor = Color.DarkOrange;
            CreateReservationButton.FillColor2 = Color.FromArgb(255, 128, 0);
        }
        private void CreateReservationButton_Leave(object sender, EventArgs e)
        {
            CreateReservationButton.FillColor = Color.FromArgb(255, 128, 0);
            CreateReservationButton.FillColor2 = Color.Chocolate;
        }

        //Sự kiện nhấn nút thoát
        private void ExitButton_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke(this, EventArgs.Empty);
            this.Dispose();
        }

        //Chỉ cho nhập số ở SĐT
        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
