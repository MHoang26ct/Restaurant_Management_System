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
using FoodOrderManagement.UI;
using FoodOrderManagement.UI.Forms.MenuManagement;
namespace FoodOrderManagement.AdminControl
{
    public partial class FormReservation : Form
    {
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormReservation_Load(object sender, EventArgs e)
        {
            DecorDataGridView(dgvReservations);
        }
        //UC_CreateReservation uc_CreateReservation;
        //OverlayBackground _overlayBackground;
        //public FormReservation()
        //{
        //    InitializeComponent();
        //    _overlayBackground = new OverlayBackground();
        //}
        //private void CreateReservationButton_Click(object sender, EventArgs e)
        //{
        //    _overlayBackground.Show(this); // hiện panel làm tối
        //    uc_CreateReservation = new UC_CreateReservation();
        //    this.Controls.Add(uc_CreateReservation);
        //    Helper.BoGoc(uc_CreateReservation, 5, true, true, true, true);
        //    uc_CreateReservation.BringToFront();
        //    uc_CreateReservation.Disposed += (s, e) =>
        //    {
        //        _overlayBackground.Hide(this);
        //    };
        //    uc_CreateReservation.Location = new Point(
        //           (this.Width - uc_CreateReservation.Width) / 2,
        //           (this.Height - uc_CreateReservation.Height) / 2
        //      );
        //    uc_CreateReservation.OnExitClicked += (s, e) =>
        //    {
        //        this.Controls.Remove(uc_CreateReservation);

        //    };
        //}
    }
}
