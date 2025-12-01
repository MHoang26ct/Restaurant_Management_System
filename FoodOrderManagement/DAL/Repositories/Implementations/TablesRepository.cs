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
    public class TablesRepository : ITablesRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Lấy danh sách các bàn còn trống
        public async Task<List<Tables>> GetAvailableTablesAsync() {
            var availableTables = new List<Tables>();
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = "SELECT Id, Capacity, Status FROM Tables WHERE Status = 'Available'";
            await using var command = new SqlCommand(query, connection);
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) {
                var table = new Tables {
                    Id = reader.GetInt32(0),
                    Capacity = reader.GetInt32(1),
                    Status = reader.GetString(2)
                };
                availableTables.Add(table);
            }
            return availableTables;
        }

        // Cập nhật trạng thái và thời gian mở bàn (thoi gian mở bàn có thể null nếu bàn được đặt lại thành trống)
        public async Task<bool> UpdateTableStatusAndOpenTimeAsync(int tableId, string status, DateTime? openedAt) {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("UpdateTableStatusAndOpenTime", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TableID", tableId);
            command.Parameters.AddWithValue("@NewStatus", status);
            if (openedAt.HasValue) {
                command.Parameters.AddWithValue("@OpenTime", openedAt.Value);
            } else {
                command.Parameters.AddWithValue("@OpenTime", DBNull.Value);
            }
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        // Thêm bàn mới
        public async Task<int> AddTableAsync(int capacity, string tableStatus) {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new SqlCommand("AddTable", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Capacity", capacity);
            command.Parameters.AddWithValue("@TableStatus", tableStatus);
            command.Parameters.AddWithValue("@OpenTime", DBNull.Value);
            var outputIdParam = new SqlParameter("@NewTableID", System.Data.SqlDbType.Int) {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);
            await command.ExecuteNonQueryAsync();
            return (int)outputIdParam.Value;
        }

        // Xoá bàn theo ID
        public async Task<bool> DeleteTableByIdAsync(int tableId) {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            var query = "DELETE FROM Tables WHERE Id = @TableID";
            await using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TableID", tableId);
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
