using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectRestaurant.FoodOrderManagement.UI.Forms.MenuManagement
{
    public partial class UC_AddFood : UserControl
    {
        public event EventHandler<FoodData> OnSaveClicked;

        private string TempImageName = ""; // biến lưu tạm tên ảnh
        public UC_AddFood()
        {
            InitializeComponent();
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
                    string AppPath = Application.StartupPath;
                    string FolderPath = Path.Combine(AppPath, "Images");
                    if (!Directory.Exists(FolderPath))
                    {
                        Directory.CreateDirectory(FolderPath);
                    }

                    string NameFile = Path.GetFileName(op.FileName);
                    string destPath = Path.Combine(FolderPath, NameFile);

                    if (op.FileName != destPath)
                    {
                        File.Copy(op.FileName, destPath, true);
                    }

                    TempImageName = NameFile;

                    MessageBox.Show("Đã chọn ảnh " + NameFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        // Lưu món ăn
        private void AddFoodButton_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(TempImageName))
            {
                MessageBox.Show("Vui lòng thêm hình ảnh món ăn cụ thể!", "Lỗi Hình Ảnh",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = new FoodData();
            {
                data.Name = NameFoodTBox.Text;
                data.Catogories = CatagorieFoodsCBox.SelectedItem?.ToString() ?? "Tất cả"; // tránh báo lỗi, nếu là null trả về "Tất cả"
                data.Price = PriceTBox.Text;
                data.Description = DescriptionTBox.Text;
                data.PictureName = string.IsNullOrEmpty(TempImageName) ? "default.png" : TempImageName;
            }
            ;

            OnSaveClicked?.Invoke(this, data);
            ResetForm();
        }
        public void ResetForm()
        {
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
