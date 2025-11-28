using Guna.UI2.WinForms;
using ProjectRestaurant.AdminControl.FormMenu;
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

        public FrmMenu()
        {
            InitializeComponent();
        }

        UC_AddFood _ucAddFood;
        UC_FoodItem _ucFoodItem;
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            // Khởi tạo UC_AddFood
            _ucAddFood = new UC_AddFood();
            _ucAddFood.Visible = false;

            this.Controls.Add(_ucAddFood);
            _ucAddFood.BringToFront();

            //Khời tạo vị trí của AddFood
            _ucAddFood.Location = new Point(
                (this.Width - _ucAddFood.Width) / 2,
                (this.Height - _ucAddFood.Height) / 4
            );

            // Đăng kí sự kiện (lắng nghe) Lưu UC_FoodItem 
            _ucAddFood.OnSaveClicked += (s, data) =>
            {
                UC_FoodItem item = new UC_FoodItem();   
                item.SetData(data.Name, data.Catogories, data.Price, data.Description, data.PictureName);

                // Đăng kí sự kiện (lắng nghe) Xóa UC_FoodItem
                item.OnDeleteClicked += (sender2, e2) =>
                {
                    var result = MessageBox.Show("Bạn có chắc muốn xóa món này không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        FlowLayoutFood.Controls.Remove(item);
                        item.Dispose();
                    }
                };
                // Thêm vào giao diện
                FlowLayoutFood.Controls.Add(item);
                //Ẩn bảng nhập
                _ucAddFood.Visible = false;
            };
        }

        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            _ucAddFood.Visible = true;
            _ucAddFood.BringToFront();
        }

        private void SearchFoodTBox1_Enter(object sender, EventArgs e)
        {
            if (SearchFoodTBox1.PlaceholderText == "Search menu items...")
            {
                SearchFoodTBox1.Text = "";
            }
        }

        private void SearchFoodTBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchFoodTBox1.Text))
            {
                SearchFoodTBox1.PlaceholderText = "Search menu items...";
            }    
        }
    }
}
