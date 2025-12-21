using FoodOrderManagement.DAL.Models.Entities;
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
    public partial class UC_ViewDetails : UserControl
    {
        public event EventHandler OnCloseClicked;
        public UC_ViewDetails()
        {
            InitializeComponent();
        }
    }
}
