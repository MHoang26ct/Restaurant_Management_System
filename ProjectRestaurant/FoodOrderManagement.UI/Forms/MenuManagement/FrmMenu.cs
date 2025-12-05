using Autofac;
using Autofac;
using Guna.UI2.WinForms;
using ProjectRestaurant.AdminControl.FormMenu;
using ProjectRestaurant.DAL.Models.Entities;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement
{
    public partial class FrmMenu : Form
    {
        private readonly ILifetimeScope _scope;
        private readonly IFoodsRepository _foodsRepository;
        public UC_AddFood _uc_AddFood;
        private List<Foods> OriginalFoodList = new List<Foods>();
        public static class Helper
        {
            // Hàm bo góc tuỳ chỉnh (Cắt Region - Viền sẽ hơi răng cưa)
            public static void BoGoc(Control control, int radius, bool topLeft, bool topRight, bool bottomRight, bool bottomLeft)
            {
                // Xóa region cũ để tránh rò rỉ bộ nhớ
                if (control.Region != null)
                {
                    control.Region.Dispose();
                    control.Region = null;
                }

                // Tạo đường dẫn mới
                GraphicsPath path = new GraphicsPath();
                path.StartFigure();

                // Hình chữ nhật bao quanh
                Rectangle r = control.ClientRectangle;
                int d = radius * 2;

                // 1. Góc Trên-Trái
                if (topLeft)
                    path.AddArc(r.X, r.Y, d, d, 180, 90);
                else
                    path.AddLine(r.X, r.Y, r.X, r.Y); // Điểm neo góc

                // 2. Góc Trên-Phải
                if (topRight)
                    path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                else
                    path.AddLine(r.Right, r.Y, r.Right, r.Y);

                // 3. Góc Dưới-Phải
                if (bottomRight)
                    path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                else
                    path.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

                // 4. Góc Dưới-Trái
                if (bottomLeft)
                    path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                else
                    path.AddLine(r.X, r.Bottom, r.X, r.Bottom);

                path.CloseFigure();

                // Áp dụng cắt
                control.Region = new Region(path);
            }
        }

        public FrmMenu(ILifetimeScope scope, IFoodsRepository foodsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _foodsRepository = foodsRepository;
        }
        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            _uc_AddFood.ResetForm();
            _uc_AddFood.Location = new Point(
                 (this.Width - _uc_AddFood.Width) / 2,
                 (this.Height - _uc_AddFood.Height) / 2
            );
            _uc_AddFood.Visible = true;
            _uc_AddFood.BringToFront();
        }

        private void SearchFoodTBox1_Enter(object sender, EventArgs e)
        {
            if (SearchFoodTBox1.PlaceholderText == "Tìm kiếm món ăn...")
            {
                SearchFoodTBox1.Text = "";
            }
        }

        private void SearchFoodTBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchFoodTBox1.Text))
            {
                SearchFoodTBox1.PlaceholderText = "Tìm kiếm món ăn...";
            }
        }

        private void CatagorieFoodsCBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
