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
using FoodOrderManagement.UI.Forms.MenuManagement;

namespace FoodOrderManagement.UI.Forms.MenuManagement
{
    public partial class UC_FoodItem : UserControl
    {
        public int FoodId { get; set; }
        public UC_FoodItem()
        {
            InitializeComponent();

        }
        private void UC_FoodItem_Load(object sender, EventArgs e)
        {
            Helper.BoGoc(PictureFood, 15, true, true, false, false); // Bo 2 góc trên
            ShadowPanel.Radius = 10;
        }
    }
}
