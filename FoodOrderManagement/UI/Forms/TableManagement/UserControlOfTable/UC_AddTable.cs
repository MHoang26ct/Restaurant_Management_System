using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_AddTable : UserControl
    {
        public event EventHandler<int> OnSaveTable;
        public UC_AddTable()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            int capacity = (int)CapacityNBox.Value;
            OnSaveTable?.Invoke(this, capacity);
        }
        public void ResetData()
        {
            CapacityNBox.Value = 2; // Mặc định 2 ghế
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
