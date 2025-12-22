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

namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_ViewDetails : UserControl
    {
        public event EventHandler OnCloseClicked;
        public UC_ViewDetails()
        {
            InitializeComponent();
        }
        public class OrderDetailDisplay
        {
            public string TenMon { get; set; } 
            public int SoLuong { get; set; }   
            public decimal DonGia { get; set; }
            public decimal ThanhTien => DonGia * SoLuong; 
        }
        //Sự kiện đóng xem chi tiết
        private void ClosedButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
