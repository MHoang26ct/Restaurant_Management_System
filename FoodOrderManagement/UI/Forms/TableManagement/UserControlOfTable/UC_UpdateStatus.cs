    using Autofac;
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
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FoodOrderManagement.UI.Forms.TableManagement.UserControlOfTable
{
    public partial class UC_UpdateStatus : UserControl
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly ICustomersRepository _customersRepository;
        private int _currentTableId;
        public UC_UpdateStatus(ITablesRepository tablesRepo, IOrdersRepository ordersRepo, IReservationsRepository resRepo, ICustomersRepository cusRepo)
        {
            InitializeComponent();
            _tablesRepository = tablesRepo;
            _ordersRepository = ordersRepo;
            _reservationsRepository = resRepo;
            _customersRepository = cusRepo;
        }
            
        //Sự kiện nhấn nút đă đặt bàn
        private void ReservedButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn chuyển trạng thái bàn này sang 'Đã đặt trước' (Reserved)?",
                    "Xác nhận đặt bàn",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                UpdateStatus("Reserved", null);
            }
        }

        //Sự kiện nhấn nút bàn trống
        private void AvailableButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Available", null);
        }

        //Sự kiện nhấn nút đă được dùng
        private void OccupiedButton_Click(object sender, EventArgs e)
        {
            UpdateStatus("Occupied", DateTime.Now);
        }

        //Sự kiện nhấn nút thoát
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
    }
}
