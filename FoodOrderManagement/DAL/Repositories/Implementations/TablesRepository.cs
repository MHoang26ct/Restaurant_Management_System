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
    public class TablesRepository : ITablesRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Lấy danh sách các bàn còn trống
        public async Task<List<Tables>> GetAvailableTablesAsync()
        {
            var availableTables = new List<Tables>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetAvailableTables", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var table = new Tables
                {
                    Id = reader.GetInt32(0),
                    Capacity = reader.GetInt32(1),
                    Status = reader.GetString(2)
                };

                if (!reader.IsDBNull(3))
                {
                    table.OpenTime = reader.GetDateTime(3);
                }

                availableTables.Add(table);
            }
            return availableTables;
        }

        // Cập nhật trạng thái và thời gian mở bàn
        public async Task<bool> UpdateTableStatusAndOpenTimeAsync(int tableId, string status, DateTime? openedAt)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateTableStatusAndOpenTime", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TableID", tableId);
            command.Parameters.AddWithValue("@NewStatus", status);
            command.Parameters.AddWithValue("@OpenTime", (object?)openedAt ?? DBNull.Value);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        // Thêm bàn mới
        public async Task<int> AddTableAsync(int capacity, string tableStatus)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("AddTable", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Capacity", capacity);
            command.Parameters.AddWithValue("@TableStatus", tableStatus);
            command.Parameters.AddWithValue("@OpenTime", DBNull.Value);

            SqlParameter outputIdParam = new SqlParameter("@NewTableID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)outputIdParam.Value;
        }

        // Xoá bàn theo ID
        public async Task<bool> DeleteTableByIdAsync(int tableId)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("DeleteTable", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TableID", tableId);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        // Lấy danh sách tất cả bàn
        public async Task<List<Tables>> GetAllTablesAsync()
        {
            var tables = new List<Tables>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetAllTables", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var table = new Tables
                {
                    Id = reader.GetInt32(0),
                    Capacity = reader.GetInt32(1),
                    Status = reader.GetString(2)
                };

                if (!reader.IsDBNull(3))
                {
                    table.OpenTime = reader.GetDateTime(3);
                }

                tables.Add(table);
            }
            return tables;
        }
    }
}
