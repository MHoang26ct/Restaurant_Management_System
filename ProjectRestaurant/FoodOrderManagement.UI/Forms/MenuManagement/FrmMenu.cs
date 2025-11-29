using Autofac;
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
using Autofac;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.DAL.Models.Entities;
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
                        //FlowLayoutFood.Controls.Remove(foodItem);
                        //foodItem.Dispose();

                        MessageBox.Show("Xóa món ăn thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa khỏi Database: " + ex.Message, "Lỗi Xóa Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private async void FrmMenu_Load(object sender, EventArgs e)
        {
            LoadFoodAsync();
            _uc_AddFood = _scope.Resolve<UC_AddFood>();
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

        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            _uc_AddFood.ResetForm();
            _uc_AddFood.Visible = true;
            _uc_AddFood.BringToFront();
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
