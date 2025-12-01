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
