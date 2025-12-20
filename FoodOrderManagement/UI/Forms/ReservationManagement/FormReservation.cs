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
using Autofac;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.DAL.Models.Entities;
namespace FoodOrderManagement.AdminControl
{
    public partial class FormReservation : Form

    {
        private void FormReservation_Load(object sender, EventArgs e)
        {
            DecorDataGridView(dgvReservations);
        }

        //For every tick of the timer, check the list of reservations,
        //if any reservation time will occur within next 1 hour, update the status of the
        //table to "Reserved", then show a notification to the admin and reload the table forms.
        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                var reservationList = await _reservationsRepository.GetAllReservationsEntityAsync();
                var currentTime = DateTime.Now;
                bool ifChanged = false;
                List<int> upcomingIds = new List<int>();

                foreach (var reservation in reservationList)
                {
                    var timeDiff = (reservation.ReservationTime - currentTime).TotalMinutes;
                    if (reservation.Status == "Pending" && timeDiff <= 60 && timeDiff > 0)
                    {
                        ifChanged = true;
                        upcomingIds.Add(reservation.Id);
                        // Cập nhật trạng thái bàn
                        await _tablesRepository.UpdateTableStatusAndOpenTimeAsync(reservation.TableId, "Reserved", null);
                        // Cập nhật trạng thái đơn đặt
                        reservation.Status = "Upcoming";
                        await _reservationsRepository.UpdateReservationAsync(reservation);
                    }
                }

                if (ifChanged)
                {
                    // 2. Hiện thông báo gom (Tránh hiện nhiều MessageBox cùng lúc)
                    string msg = $"Có {upcomingIds.Count} đơn đặt bàn sắp đến trong 1 tiếng tới (Mã: {string.Join(", ", upcomingIds)})";
                    MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Cập nhật UI
                    await LoadReservationList();
                    await _formTable.LoadTableList();
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi để debug trên Mac/Windows dễ dàng hơn
                Console.WriteLine($"Lỗi Timer: {ex.Message}");
            }
            finally
            {
                timer1.Start();
            }
        }
    }
}
