using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{

    public partial class UC_AddTableCard : UserControl
    {
        public event EventHandler OnCardClicked;

        public UC_AddTableCard()
        {
            InitializeComponent();
           
        }

        private void AddTableButton_Click(object sender, EventArgs e)
        {
            OnCardClicked?.Invoke(this, EventArgs.Empty);
            OnCardClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
