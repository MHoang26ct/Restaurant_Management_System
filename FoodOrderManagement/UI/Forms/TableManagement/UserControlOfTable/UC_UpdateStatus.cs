using Autofac;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_UpdateStatus : UserControl
    {
        private int _currentTableId;
        public void LoadTableData(TableData data)
        {
            _currentTableId = data.TableId;

            // Gán dữ liệu lên giao diện 
            TableIdLabel.Text = data.TableName; // Ví dụ: Thông tin bàn 5
            CustomerNameLabel.Text = "Khách hàng: " + (string.IsNullOrEmpty(data.CustomerName) ? "Chưa có" : data.CustomerName);
            OrderIdLabel.Text = "Đơn hàng: " + (string.IsNullOrEmpty(data.OrderId) ? "Chưa có" : "#" + data.OrderId);
            TimeReservedLabel.Text = "Thời gian đặt: " + (data.ReservationTime.HasValue ? data.ReservationTime.Value.ToString("HH:mm") : "Không có");

            switch (data.Status)
            {
                case "Available":
                    this.StatusText.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(220, 255, 220);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(220, 255, 220);
                    this.StatusText.BorderColor = Color.DarkGreen;
                    this.StatusText.ForeColor = Color.DarkGreen;
                    this.StatusText.Text = data.Status;
                    break;
                case "Occupied":
                    this.StatusText.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 192, 192);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 192, 192);
                    this.StatusText.BorderColor = Color.DarkRed;
                    this.StatusText.ForeColor = Color.DarkRed;
                    this.StatusText.Text = data.Status;
                    break;
                case "Reserved":
                    this.StatusText.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor = Color.FromArgb(255, 255, 128);
                    this.StatusText.HoverState.FillColor2 = Color.FromArgb(255, 255, 128);
                    this.StatusText.BorderColor = Color.SaddleBrown;
                    this.StatusText.ForeColor = Color.SaddleBrown;
                    this.StatusText.Text = data.Status;
                    break;
            }
        }
        public UC_UpdateStatus(ILifetimeScope scope, ITablesRepository tablesRepository)
        {
            InitializeComponent();
            _tablesRepository = tablesRepository;
            _scope = scope;
        }

        private void ReservedButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn chuyển trạng thái bàn này sang 'Đã đặt trước' (Reserved)?",
                "Xác nhận đặt bàn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                UpdateStatus("Reserved", null);
            }
        }

        private void AvailableButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Available", null);
        }

        private void OccupiedButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Occupied", DateTime.Now);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
