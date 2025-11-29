using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using ProjectRestaurant.DAL.Models.Entities;
using ProjectRestaurant.DAL.Repositories.Interfaces;
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

namespace ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement
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

        private void ChoosePictureButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Chọn ảnh món ăn";
            op.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";

            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CurrentImagePath = op.FileName;
                    string extension = Path.GetExtension(op.FileName);
                    TempImageName = Guid.NewGuid().ToString() + extension;
                    MessageBox.Show("Đã chọn ảnh " + Path.GetFileName(CurrentImagePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        // Lưu món ăn
        private async void AddFoodButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameFoodTBox.Text) || string.IsNullOrEmpty(PriceTBox.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món và giá tiền!", "Lỗi Thông Tin", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(CatagorieFoodsCBox.SelectedItem.ToString() == "Tất cả" )
            {
                MessageBox.Show("Vui lòng chọn danh mục cụ thể (Ví dụ: Món chính/Món phụ...)" +
                    "\nKhông được chọn 'Tất cả'. ","Lỗi Danh Mục", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CatagorieFoodsCBox.DroppedDown = true;
                return;
            }

            if (EditingFoodId == 0 && (string.IsNullOrEmpty(CurrentImagePath)))
            {
                MessageBox.Show("Vui lòng chọn ảnh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string FinalImagePath = OldImagePath;
            if (!string.IsNullOrEmpty(CurrentImagePath) && !string.IsNullOrEmpty(TempImageName))
            {
                string appPath = Application.StartupPath;
                string folderPath = Path.Combine(appPath, "Images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string destPath = Path.Combine(folderPath, TempImageName);
                try
                {
                    File.Copy(CurrentImagePath, destPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sao chép ảnh: " + ex.Message);
                    return;
                }
                FinalImagePath = Path.Combine("Images", TempImageName); 
            }
            
            Foods NewFood = new Foods();
            {
                NewFood.Name = NameFoodTBox.Text;
                NewFood.Category = CatagorieFoodsCBox.SelectedItem?.ToString() ?? "Tất cả"; // tránh báo lỗi, nếu là null trả về "Tất cả"
                if (!decimal.TryParse(PriceTBox.Text, out decimal price))
                {
                    MessageBox.Show("Giá tiền không hợp lệ.", "Lỗi Thông Tin");
                    return;
                }
                NewFood.Price = price;
                NewFood.Description= DescriptionTBox.Text;
                NewFood.ImagePath = FinalImagePath;
            }
            try
            {
                if (EditingFoodId == 0)
                {
                    int newId = await _foodsRepository.AddFoodAsync(NewFood);
                    MessageBox.Show($"Thêm món ăn thành công! ID: {newId}");
                }
                else
                {
                    NewFood.Id = EditingFoodId;
                    await _foodsRepository.UpdateFoodAsync(NewFood);
                    MessageBox.Show($"Cập nhật món ăn thành công! ID: {EditingFoodId}");
                }
                                    ResetForm();
                    FoodAdded?.Invoke(this, EventArgs.Empty);
                    CurrentImagePath = "";
                    TempImageName = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món ăn vào CSDL: " + ex.Message);
            }
        }
        public void SetEditData(Foods food)
        { 
            EditingFoodId = food.Id;
            OldImagePath = food.ImagePath;

 
            NameFoodTBox.Text = food.Name;
            PriceTBox.Text = food.Price.ToString("F0", CultureInfo.InvariantCulture);
            CatagorieFoodsCBox.SelectedItem = food.Category;
            DescriptionTBox.Text = food.Description;


            AddFoodButton.Text = "Cập Nhật";


            CurrentImagePath = "";
            TempImageName = ""; 
        }
        public void ResetForm()
        {
            EditingFoodId = 0; 
            OldImagePath = "";
            AddFoodButton.Text = "Thêm Món Ăn";
            NameFoodTBox.Clear();
            DescriptionTBox.Clear();
            PriceTBox.Clear();
            TempImageName = "";
            CatagorieFoodsCBox.StartIndex = 0;
        }

        // Sự kiện nút exit
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }
}
