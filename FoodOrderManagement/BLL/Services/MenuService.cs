using Autofac;
using Guna.UI2.WinForms;
using FoodOrderManagement.UI.Forms;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FoodOrderManagement.UI.Forms.MenuManagement.FrmMenu;
namespace FoodOrderManagement.UI.Forms.MenuManagement
{
    public partial class FrmMenu : Form
    {

        // Hàm này chỉ có nhiệm vụ: Nhận 1 danh sách -> Vẽ lên màn hình
        private void RenderFoodList(List<Foods> listToRender)
        {
            FlowLayoutFood.Controls.Clear();
            FlowLayoutFood.SuspendLayout();
            foreach (Foods food in listToRender)
            {
                UC_FoodItem item = _scope.Resolve<UC_FoodItem>();
                item.SetData(food.Id, food.Name, food.Price.ToString(), food.Category, food.ImagePath, food.Description);
                item.OnDeleteClicked += FoodItem_OnDeleteClicked;
                item.OnEditClicked += async (s, e) =>
                {
                    Foods foodToEdit = await _foodsRepository.GetFoodByIdAsync(item.FoodId);

                    if (foodToEdit != null)
                    {
                        _uc_AddFood.SetEditData(foodToEdit);
                        _uc_AddFood.Visible = true;
                        _uc_AddFood.BringToFront();
                    }
                };
                FlowLayoutFood.Controls.Add(item);
            }
            FlowLayoutFood.ResumeLayout();
        }
        public void ApplyFilter()
        {
            string keyword = SearchFoodTBox1.Text.ToLower().Trim();
            string selectedCategory = CatagorieFoodsCBox.SelectedItem?.ToString() ?? "Tất cả";
            var filteredList = OriginalFoodList.Where(food =>
            {

                bool matchName = string.IsNullOrEmpty(keyword) || food.Name.ToLower().Contains(keyword);
                bool matchCategory = false;

                if (selectedCategory == "Tất cả")
                {
                    matchCategory = true;
                }
                else
                {
                    if (food.Category != null)
                    {
                        matchCategory = food.Category.Trim().ToLower() == selectedCategory.Trim().ToLower();
                    }
                }

                return matchName && matchCategory;
            }).ToList();

            RenderFoodList(filteredList);
        }
        public async Task LoadFoodAsync()
        {
            try
            {

                OriginalFoodList = await _foodsRepository.GetAllFoodsAsync();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải món ăn từ Database: " + ex.Message);
            }

        }
        private async void FoodItem_OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is UC_FoodItem foodItem)
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa món này không?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _foodsRepository.DeleteFoodAsync(foodItem.FoodId);
                        var itemToRemove = OriginalFoodList.FirstOrDefault(f => f.Id == foodItem.FoodId);
                        if (itemToRemove != null)
                        {
                            OriginalFoodList.Remove(itemToRemove);
                        }
                        ApplyFilter();
                        MessageBox.Show("Xóa món ăn thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa khỏi Database: " + ex.Message, "Lỗi Xóa Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private async void FormMenu_Load(object sender, EventArgs e)
        {
            await LoadFoodAsync();
            _uc_AddFood.Visible = false;
            this.Controls.Add(_uc_AddFood);
            _uc_AddFood.BringToFront();
            //Khời tạo vị trí của AddFood
            _uc_AddFood.Location = new Point(
                (this.Width - _uc_AddFood.Width) / 2,
                (this.Height - _uc_AddFood.Height) / 4
            );
            SearchFoodTBox1.TextChanged += (s, args) => ApplyFilter();

            CatagorieFoodsCBox.SelectedIndexChanged += (s, args) => ApplyFilter();
            CatagorieFoodsCBox.Items.Clear();
            CatagorieFoodsCBox.Items.AddRange(new object[] { "Tất cả", "Món chính", "Khai vị", "Tráng miệng", "Đồ uống" });
            CatagorieFoodsCBox.SelectedIndex = 0; // Chọn mặc định là Tất cả
            _uc_AddFood.FoodAdded += (s, args) =>
            {
                _uc_AddFood.Visible = false;
                LoadFoodAsync();
            };
        }
    }
}
///-----------------------------------------------------------------------------------------------------------------------------------------------

namespace FoodOrderManagement.UI.Forms.MenuManagement
{

    public partial class UC_AddFood : UserControl
    {
       
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

            if ( CatagorieFoodsCBox.SelectedItem.ToString() == "Tất cả")
            {
                MessageBox.Show("Vui lòng chọn danh mục cụ thể (Ví dụ: Món chính/Món phụ...)" +
                    "\nKhông được chọn 'Tất cả'. ", "Lỗi Danh Mục",
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
                NewFood.Description = DescriptionTBox.Text;
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
            ResetForm();
            this.Visible = false;
        }

    }
}
///-----------------------------------------------------------------------------------------------------------------------------------------------
namespace FoodOrderManagement.UI.Forms.MenuManagement
{
    public partial class UC_FoodItem : UserControl
    {
        public void SetData(int Id, string name, string price, string Catagories, string PicturePath, string Description)
        {
            FoodNameLabel.Text = name;
            CatagoriesButton.Text = Catagories;
            FoodPriceLabel.Text = price;
            FoodDesciptionLabel.Text = Description;
            FoodId = Id;

            string path = Path.Combine(Application.StartupPath, PicturePath);
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
        public event EventHandler OnDeleteClicked; // Tạo sự kiện xóa
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            OnDeleteClicked?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler OnEditClicked;
        private void EditButton_Click(object sender, EventArgs e)
        {
            OnEditClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
