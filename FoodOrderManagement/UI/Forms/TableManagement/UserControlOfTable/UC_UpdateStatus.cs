using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_UpdateStatus : UserControl
    {
        public UC_UpdateStatus()
        {
            InitializeComponent();
        }
        public void SetData(int TableID, int Capacity, string Status, int OrderId)
        {
            TableIdLabel.Text = "Thông tin bàn " + TableID.ToString();
            CapacityLabel.Text = Capacity.ToString();
            switch (Status)
            {
                case "Còn trống":
                    this.StatusText.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.BorderColor = Color.DarkGreen;
                    this.StatusText.ForeColor = Color.DarkGreen;
                    this.StatusText.Text = Status;
                    break;
                case "Đang sử dụng":
                    this.StatusText.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.BorderColor = Color.DarkRed;
                    this.StatusText.ForeColor = Color.DarkRed;
                    this.StatusText.Text = Status;
                    break;
                case "Đã đặt bàn":
                    this.StatusText.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.BorderColor = Color.SaddleBrown;
                    this.StatusText.ForeColor = Color.SaddleBrown;
                    this.StatusText.Text = Status;
                    break;
            }
        }

        private void doubleBufferedtlp1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
