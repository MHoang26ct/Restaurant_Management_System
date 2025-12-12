using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Configuration;
using FoodOrderManagement.DAL.Helper;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class TablesRepository : ITablesRepository
    {
        private readonly DatabaseHelper _db = new DatabaseHelper();

        // Hàm chung để chuyển dữ liệu từ Reader sang Object Tables
        private Tables Mapper(SqlDataReader reader)
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
            return table;
        }

        // Lấy danh sách các bàn còn trống
        public async Task<List<Tables>> GetAvailableTablesAsync()
        {
            return await _db.GetListAsync("GetAvailableTables", Mapper);
        }

        // Cập nhật trạng thái và thời gian mở bàn
        public async Task<bool> UpdateTableStatusAndOpenTimeAsync(int tableId, string status, DateTime? openedAt)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@TableID", tableId),
                new SqlParameter("@NewStatus", status),
                new SqlParameter("@OpenTime", (object?)openedAt ?? DBNull.Value)
            };

            int rowsAffected = await _db.ExecuteNonQueryAsync("UpdateTableStatusAndOpenTime", parameters);
            return rowsAffected > 0;
        }

        // Thêm bàn mới
        public async Task<int> AddTableAsync(int capacity, string tableStatus)
        {
            var outputIdParam = new SqlParameter("@NewTableID", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Capacity", capacity),
                new SqlParameter("@TableStatus", tableStatus),
                new SqlParameter("@OpenTime", DBNull.Value),
                outputIdParam
            };
            await _db.ExecuteNonQueryAsync("AddTable", parameters);
            return (int)outputIdParam.Value;
        }

        // Xoá bàn theo ID
        public async Task<bool> DeleteTableByIdAsync(int tableId)
        {
            var param = new SqlParameter("@TableID", tableId);
            int rowsAffected = await _db.ExecuteNonQueryAsync("DeleteTable", param);
            return rowsAffected > 0;
        }

        // Lấy danh sách tất cả bàn
        public async Task<List<Tables>> GetAllTablesAsync()
        {
            return await _db.GetListAsync("GetAllTables", Mapper);
        }
    }
}
