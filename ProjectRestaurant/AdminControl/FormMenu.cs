using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectRestaurant.AdminControl
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void MenuIcon1_Click(object sender, EventArgs e)
        {

        }

        private void BackgroundIcon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            string[] catagories =
            {
                "Tất cả danh mục",
                "Món chính",
                "Khai vị",
                "Tráng miệng",
                "Thức Uống" ,
            };
            FoodCategories.Items.AddRange(catagories);
        }
    }
}
