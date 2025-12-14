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
            _currentCustomer = customer;// gán giá trị biến toàn cục
            // Gán dữ liệu vào các Label giao diện
            // Giả sử tên các label của bạn là như này
            CustomerNameLabel.Text = customer.FullName;
            PhoneNumber.Text = customer.PhoneNumber;
            Email.Text = customer.Email;
            // Format tiền tệ
            TotalSpent.Text = customer.TotalSpent.ToString("#,##0") + " VNĐ";
            TotalVisit.Text = customer.TotalVisits.ToString();
            //Ngày ghé gần nhất
            LastVisitDate.Text = customer.LastVisitDate.ToString("dd/MM/yyyy");
            // Lưu trữ ID để sau này sửa/xóa nếu cần
            this.Tag = customer.Id;
            //switch (RankCustomer.ToLower()) // Chuyển về chữ thường để so sánh
            //{
            //    case "silver": // Hạng Bạc
            //        RankBackground.CustomBorderColor = Color.Silver;
            //        RankCirclePanel.FillColor = Color.Silver;
            //        RankCirclePanel.FillColor2 = Color.Gray;
            //        CustomerRank.Text = RankCustomer.ToUpper();
            //        CustomerRank.BorderColor = Color.Black;
            //        CustomerRank.ForeColor = Color.Black;
            //        CustomerRank.FillColor = Color.Gainsboro;
            //        CustomerRank.FillColor2 = Color.Gainsboro;
            //        PhoneNumber.BorderColor = Color.Gray;
            //        Email.BorderColor = Color.Gray;
            //        break;

            //    case "gold": // Hạng Vàng
            //        RankBackground.CustomBorderColor = Color.Gold;
            //        RankCirclePanel.FillColor = Color.Gold;
            //        RankCirclePanel.FillColor2 = Color.FromArgb(255, 128, 0);
            //        CustomerRank.Text = RankCustomer.ToUpper();
            //        CustomerRank.BorderColor = Color.Gold;
            //        CustomerRank.ForeColor = Color.Chocolate;
            //        CustomerRank.FillColor = Color.FromArgb(255, 255, 192);
            //        CustomerRank.FillColor2 = Color.FromArgb(255, 255, 192);
            //        PhoneNumber.BorderColor = Color.Gold;
            //        Email.BorderColor = Color.Gold;
            //        break;

            //    case "platinum": // Hạng Bạch Kim
            //        RankBackground.CustomBorderColor = Color.Firebrick
            //        RankCirclePanel.FillColor = Color.DarkOrange;
            //        RankCirclePanel.FillColor2 = Color.Firebrick;
            //        CustomerRank.Text = RankCustomer.ToUpper();
            //        CustomerRank.BorderColor = Color.Firebrick;
            //        CustomerRank.ForeColor = Color.Firebrick;
            //        CustomerRank.FillColor = Color.255, 192, 192;
            //        CustomerRank.FillColor2 = Color.Firebrick;
            //        PhoneNumber.BorderColor = Color.Firebrick;
            //        Email.BorderColor = Color.Firebrick;
            //        break;

            //    default: // Mặc định (New/Regular)
            //        RankBackground.CustomBorderColor = Color.Black;
            //        RankCirclePanel.FillColor = Color.Black;
            //        RankCirclePanel.FillColor2 = Color.Black;
            //        CustomerRank.Text = RankCustomer.ToUpper();
            //        CustomerRank.BorderColor = Color.Black;
            //        CustomerRank.ForeColor = Color.Black;
            //        CustomerRank.FillColor = Color.White;
            //        CustomerRank.FillColor2 = Color.White;
            //        PhoneNumber.BorderColor = Color.Black;
            //        Email.BorderColor = Color.Black;
            //        break;
            //}
        }

        private void CustomerRank_Click(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditClicked?.Invoke(this, _currentCustomer);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDeleteClicked?.Invoke(this, _currentCustomer);
        }
    }
}
