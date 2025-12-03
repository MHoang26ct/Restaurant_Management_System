using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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


    }
}
