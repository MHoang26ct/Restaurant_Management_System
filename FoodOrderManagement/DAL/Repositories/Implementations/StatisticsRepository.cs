using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using FoodOrderManagement.UI.Forms.FormDashboard;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class StatisticsRepository : IStatisticsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thống kết quả kinh doanh theo khoảng thời gian
        public async Task<List<Statistics>> GetBusinessStatsByDateAsync(DateTime startDate, DateTime endDate) {
            var statsList = new List<Statistics>();
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetBusinessStatsByDate", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fromDate", startDate);
                    command.Parameters.AddWithValue("@toDate", endDate);
                    using (var reader = await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            statsList.Add(new Statistics {
                                Date = reader.GetDateTime(0),
                                TotalRevenue = reader.GetDecimal(1),
                                TotalOrders = reader.GetInt32(2),
                                TotalGuests = reader.GetInt32(3),
                                TotalReservations = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return statsList;
        }
        public async Task<DashboardStatModel> GetDashboardStatsByDateAsync(DateTime date)
        {
            var stats = new DashboardStatModel();

            // Tạo khoảng thời gian từ 00:00:00 đến 23:59:59 của ngày đó
            DateTime fromDate = date.Date;
            DateTime toDate = date.Date.AddDays(1).AddTicks(-1);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetDashboardStats", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            stats.TotalRevenue = reader["TotalRevenue"] != DBNull.Value ? Convert.ToDecimal(reader["TotalRevenue"]) : 0;
                            stats.TotalOrders = reader["TotalOrders"] != DBNull.Value ? Convert.ToInt32(reader["TotalOrders"]) : 0;
                            stats.TotalReservations = reader["TotalReservations"] != DBNull.Value ? Convert.ToInt32(reader["TotalReservations"]) : 0;
                            stats.TotalCustomers = reader["TotalCustomers"] != DBNull.Value ? Convert.ToInt32(reader["TotalCustomers"]) : 0;
                        }
                    }
                }
            }
            return stats;
        }
    }
}
