using FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder;
using FoodOrderManagement.UI.Forms.ReservationManagement.UserControlOfReservation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodOrderManagement.AdminControl
{
    public partial class FormReservation : Form
    {
        UC_CreateReservation uc_CreateReservation;
        public FormReservation()
        {
            InitializeComponent();
        }

        private void doubleBufferedtlp4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateReservationButton_Click(object sender, EventArgs e)
        {
          uc_CreateReservation = new UC_CreateReservation();
          this.Controls.Add(uc_CreateReservation);
          uc_CreateReservation.BringToFront();

          uc_CreateReservation.Location = new Point(
                 (this.Width - uc_CreateReservation.Width) / 2,
                 (this.Height - uc_CreateReservation.Height) / 2
            );
            uc_CreateReservation.OnExitClicked += (s, e) =>
            {
                this.Controls.Remove(uc_CreateReservation);

            };
        }
    }
}
