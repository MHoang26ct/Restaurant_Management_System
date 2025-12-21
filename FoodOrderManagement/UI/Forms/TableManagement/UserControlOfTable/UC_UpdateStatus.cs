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
        public async void LoadTableData(TableData data)
        {
            _currentTableId = data.TableId;
            TableIdLabel.Text = data.TableName; 
            CustomerNameLabel.Text = "Khách hàng: Chưa có";
            OrderIdLabel.Text = "Đơn hàng: Chưa có";
            TimeReservedLabel.Text = "Thời gian: --";
            try
            {
                if (data.Status == "Occupied") 
                {
                    // Get the active order for this table (not paid yet)
                    var order = await _ordersRepository.GetOrdersByTableIdAsync(_currentTableId);

                    if (order != null)
                    {
                        var cus = await _customersRepository.GetCustomerByIdAsync(order.CustomerId);
                        CustomerNameLabel.Text = "Khách hàng: " + (cus != null ? cus.FullName : "Khách vãng lai");
                        OrderIdLabel.Text = "Đơn hàng: #" + order.Id;
                        TimeReservedLabel.Text = "Giờ vào: " + order.OrderTime.ToString("HH:mm");
                    }
                }
                else if (data.Status == "Reserved")
                {
                    // Get the upcoming reservation (Pending status)
                    var booking = await _reservationsRepository.GetUpcomingReservationByTableIdAsync(_currentTableId);

                    if (booking != null)
                    {
                        var cus = await _customersRepository.GetCustomerByIdAsync(booking.customerId);
                        CustomerNameLabel.Text = "Khách hàng: " + (cus != null ? cus.FullName : "Unknown");
                        OrderIdLabel.Text = "Mã đặt: #" + booking.Id;
                        TimeReservedLabel.Text = "Giờ hẹn: " + booking.ReservationTime.ToString("HH:mm dd/MM");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin chi tiết: " + ex.Message);
            }

            UpdateStatusUI(data.Status);
        }
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

            private void AvailableButton_Click(object sender, EventArgs e)
            {
                UpdateStatus("Available", null);
            }

            private void OccupiedButton_Click(object sender, EventArgs e)
            {
                UpdateStatus("Occupied", DateTime.Now);
            }

            private void ExitButton_Click(object sender, EventArgs e)
            {
                this.Parent.Controls.Remove(this);
                this.Dispose();
            }
        }
    }
