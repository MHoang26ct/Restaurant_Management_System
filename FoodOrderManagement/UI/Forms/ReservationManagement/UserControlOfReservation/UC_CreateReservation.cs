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
                // Gọi hàm đóng 
                ExitButton_Click(null, null);
                return true;
            }
            // Tạo vòng lặp tab không cho tab nhảy lung tung ra ngoài form cha
            if (keyData == Keys.Tab && CreateReservationButton.Focused)
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

        //public event EventHandler OnExitClicked;
        //public UC_CreateReservation()
        //{
        //    InitializeComponent();
        //    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        //}

        //private void ExitButton_Click(object sender, EventArgs e)
        //{
        //    OnExitClicked?.Invoke(this, EventArgs.Empty);
        //    this.Dispose();
        //}

        //private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // chỉ cho nhập số
        //}

        //private void CreateOrderButton_Click(object sender, EventArgs e)
        //{
        //    if (TimeReservationCBox.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Vui lòng chọn khung giờ đặt bàn",
        //                        "Thông báo",
        //                        MessageBoxButtons.OK,
        //                        MessageBoxIcon.Warning);
        //        return;
        //    }
        //}
    }
}
