using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.MenuManagement
{

    public partial class UC_AddFood : UserControl
    {
        private readonly IFoodsRepository _foodsRepository;
        private string TempImageName = "";
        private string CurrentImagePath = "";
        private string OldImagePath = "";
        private int EditingFoodId = 0;
        public event EventHandler FoodAdded;
        public UC_AddFood(IFoodsRepository foodsRepository)
        {
            InitializeComponent();
            _foodsRepository = foodsRepository;
        }

        //
        // TAB INDEX 
        //

        // Sự kiện khi load UC_AddFood sẽ focus vào UC này
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                // Ép Focus vào ô nhập liệu đầu tiên ngay lập tức
                NameFoodTBox.Focus();
                NameFoodTBox.Select();
            }
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
            if (keyData == Keys.Tab && AddFoodButton.Focused)
            {
                // Ép nhảy về ô đầu tiên 
                NameFoodTBox.Focus();
                // Trả về true để chặn Windows không tự nhảy lung tung 
                return true;
            }

            // Nếu không phải các phím trên thì hành xử như bình thường
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //
        // Sự kiện chọn hay rời nút thêm món ăn, textbox tên món, textbox giá món  
        //

        private void AddFoodButton_Enter(object sender, EventArgs e)
        {
            AddFoodButton.FillColor = Color.Chocolate;
            AddFoodButton.FillColor2 = Color.FromArgb(255, 128, 0);
        }

        private void AddFoodButton_Leave(object sender, EventArgs e)
        {
            AddFoodButton.FillColor = Color.FromArgb(255, 128, 0);
            AddFoodButton.FillColor2 = Color.Chocolate;
        }
        private void NameFoodTBox_Enter(object sender, EventArgs e)
        {
            if (NameFoodTBox.Text == "Nhập tên món ăn...")
            {
                NameFoodTBox.Text = "";
            }
        }

        private void NameFoodTBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameFoodTBox.Text))
            {
                NameFoodTBox.PlaceholderText = "Nhập tên món ăn...";
            }
        }

        private void PriceTBox_Enter(object sender, EventArgs e)
        {
            if (PriceTBox.Text == "Nhập giá tiền...")
            {
                PriceTBox.Text = "";
            }
        }

        private void PriceTBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PriceTBox.Text))
            {
                PriceTBox.PlaceholderText = "Nhập giá tiền...";
            }
        }
    }
}
