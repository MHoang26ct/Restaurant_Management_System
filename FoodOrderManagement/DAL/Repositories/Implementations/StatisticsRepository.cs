using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class StatisticsRepository : IStatisticsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thống kết quả kinh doanh theo khoảng thời gian
        public async Task<Statistics?> GetBusinessStatsByDateAsync(DateTime startDate, DateTime endDate) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetBusinessStatsByDate", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@fromDate", startDate);
                    command.Parameters.AddWithValue("@toDate", endDate);
                    using (var reader = await command.ExecuteReaderAsync()) {
                        if (await reader.ReadAsync()) {
                            return new Statistics {
                                Date = reader.GetDateTime(0),
                                TotalRevenue = reader.GetDecimal(1),
                                TotalOrders = reader.GetInt32(2),
                                TotalCustomers = reader.GetInt32(3),
                                TotalReservations = reader.GetInt32(4)
                            };
                        }
                        else {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
