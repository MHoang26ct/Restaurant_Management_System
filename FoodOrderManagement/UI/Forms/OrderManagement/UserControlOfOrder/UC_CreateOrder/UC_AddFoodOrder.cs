using Accessibility;
using Autofac;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder
{
    public partial class UC_AddFoodOrder : UserControl
    {
        private readonly ILifetimeScope _scope;
        private readonly IFoodsRepository _foodsRepository;
        List<Foods> foodlist = new List<Foods>();
        public UC_AddFoodOrder(ILifetimeScope scope, IFoodsRepository foodsRepository)
        {
            InitializeComponent();
            _scope = scope;
            _foodsRepository = foodsRepository;
            LoadFoodToComboBox();

        }
    }
}
