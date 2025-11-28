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
using System.IO; // Thêm ảnh món ăn
using static ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement.FrmMenu;


namespace ProjectRestaurant.AdminControl.FormMenu
{
    public partial class UC_FoodItem : UserControl
    {

        public UC_FoodItem()
        {
            InitializeComponent();

        }
        //
        // Hàm Truyền dữ liệu 
        //
        public void SetData(string name, string Catagories, string price, string Description, string PictureName)
        {
            FoodNameLabel.Text = name;
            CatagoriesButton.Text = Catagories;
            FoodPriceLabel.Text = price;
            FoodDesciptionLabel.Text = Description;

            string path = Path.Combine(Application.StartupPath, "Images", PictureName);
            if (File.Exists(path)) // Kiểm tra xem người dùng có truyền ảnh vào không
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    PictureFood.BackgroundImage = Image.FromStream(fs); // Đặt BackgroundImage là 1 ảnh từ máy
                }
            }
            else
            {
                PictureFood.BackgroundImage = null;
            }
        }
        private void UC_FoodItem_Load(object sender, EventArgs e)
        {
            Helper.BoGoc(PictureFood, 15, true, true, false, false); // Bo 2 góc trên
            ShadowPanel.Radius = 10;
        }

        public event EventHandler OnDeleteClicked; // Tạo sự kiện xóa
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
