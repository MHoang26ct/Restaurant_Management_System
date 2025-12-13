using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation
{
    public partial class UC_CreateReservation : UserControl
    {
        public event EventHandler OnExitClicked;
        public UC_CreateReservation()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            OnExitClicked?.Invoke(this, EventArgs.Empty);
        }

        private void PhoneNumberTBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // chỉ cho nhập số
        }
    }
}
