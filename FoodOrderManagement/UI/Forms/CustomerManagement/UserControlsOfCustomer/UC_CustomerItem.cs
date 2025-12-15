using FoodOrderManagement.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.CustomerManagement.UserControlsOfCustomer
{
    public partial class UC_CustomerItem : UserControl
    {
        private Customers _currentCustomer; // khai báo lưu biến toàn cục
        public event EventHandler<Customers> OnEditClicked;
        public event EventHandler<Customers> OnDeleteClicked;
        public UC_CustomerItem()
        {
            InitializeComponent();
        }
        public void SetCustomerData(Customers customer)
        {
            _currentCustomer = customer;

            // 1. Gán dữ liệu cơ bản
            CustomerNameLabel.Text = customer.FullName;
            PhoneNumber.Text = customer.PhoneNumber;
            Email.Text = customer.Email;

            // Format tiền tệ
            TotalSpent.Text = customer.TotalSpent.ToString("#,##0") + " VNĐ";
            TotalVisit.Text = customer.TotalVisits.ToString();

            // Xử lý ngày ghé gần nhất
            if (customer.LastVisitDate == DateTime.MinValue)
            {
                LastVisitDate.Text = "Chưa đến";
            }
            else
            {
                LastVisitDate.Text = customer.LastVisitDate.ToString("dd/MM/yyyy");
            }

            this.Tag = customer.Id; // Lưu ID để dùng sau này

            // ==========================================================
            // 2. LOGIC TÍNH RANK (Dựa trên số tiền chi tiêu)
            // ==========================================================
            string rank = "Regular"; // Mặc định
            double spent = customer.TotalSpent;

            if (spent >= 50000000) // 50 triệu
            {
                rank = "Platinum";
            }
            else if (spent >= 10000000) // 10 triệu
            {
                rank = "Gold";
            }
            else if (spent >= 2000000) // 2 triệu
            {
                rank = "Silver";
            }

            // Gán text hiển thị rank
            CustomerRank.Text = rank.ToUpper();

            // ==========================================================
            // 3. LOGIC ĐỔI MÀU GIAO DIỆN THEO RANK
            // ==========================================================
            switch (rank.ToLower())
            {
                case "silver": // Hạng Bạc (>= 2tr)
                    RankBackground.CustomBorderColor = Color.Silver;
                    RankCirclePanel.FillColor = Color.Silver;
                    RankCirclePanel.FillColor2 = Color.Gray;

                    CustomerRank.BorderColor = Color.Black;
                    CustomerRank.ForeColor = Color.Black;
                    CustomerRank.FillColor = Color.Gainsboro;
                    CustomerRank.FillColor2 = Color.Gainsboro;

                    PhoneNumber.BorderColor = Color.Gray;
                    Email.BorderColor = Color.Gray;
                    break;

                case "gold": // Hạng Vàng (>= 10tr)
                    RankBackground.CustomBorderColor = Color.Gold;
                    RankCirclePanel.FillColor = Color.Gold;
                    RankCirclePanel.FillColor2 = Color.FromArgb(255, 128, 0); // Cam đậm

                    CustomerRank.BorderColor = Color.Gold;
                    CustomerRank.ForeColor = Color.Chocolate; // Màu chữ nâu vàng
                    CustomerRank.FillColor = Color.FromArgb(255, 255, 192); // Vàng nhạt
                    CustomerRank.FillColor2 = Color.FromArgb(255, 255, 192);

                    PhoneNumber.BorderColor = Color.Gold;
                    Email.BorderColor = Color.Gold;
                    break;

                case "platinum": // Hạng Bạch Kim (>= 50tr)
                    RankBackground.CustomBorderColor = Color.Firebrick;
                    RankCirclePanel.FillColor = Color.DarkOrange;
                    RankCirclePanel.FillColor2 = Color.Firebrick;

                    CustomerRank.BorderColor = Color.Firebrick;
                    CustomerRank.ForeColor = Color.Firebrick;
                    // Sửa cú pháp màu lỗi của bạn: Color.255, 192, 192 -> Color.FromArgb(...)
                    CustomerRank.FillColor = Color.FromArgb(255, 192, 192);
                    CustomerRank.FillColor2 = Color.FromArgb(255, 192, 192);

                    PhoneNumber.BorderColor = Color.Firebrick;
                    Email.BorderColor = Color.Firebrick;
                    break;

                default: // Regular (Dưới 2tr)
                    RankBackground.CustomBorderColor = Color.Black;
                    RankCirclePanel.FillColor = Color.Black;
                    RankCirclePanel.FillColor2 = Color.Black;

                    CustomerRank.BorderColor = Color.Black;
                    CustomerRank.ForeColor = Color.Black;
                    CustomerRank.FillColor = Color.White;
                    CustomerRank.FillColor2 = Color.White;

                    PhoneNumber.BorderColor = Color.Black;
                    Email.BorderColor = Color.Black;
                    break;
            }
        }

        private void CustomerRank_Click(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (_currentCustomer != null)
            {
                OnEditClicked?.Invoke(this, _currentCustomer);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDeleteClicked?.Invoke(this, _currentCustomer);
        }
    }
}
