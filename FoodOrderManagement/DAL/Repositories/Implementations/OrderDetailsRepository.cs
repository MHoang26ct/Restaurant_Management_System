using FoodOrderManagement.DAL.Helper;
using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using static FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder.UC_ViewDetails;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class OrderDetailsRepository : IOrderDetailsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private readonly DatabaseHelper _db = new DatabaseHelper();

        //
        private orderDetail Mapper(SqlDataReader reader) {
            return new orderDetail {
                Id = reader.GetInt32(0),
                OrderId = reader.GetInt32(1),
                FoodId = reader.GetInt32(2),
                Quantity = reader.GetInt32(3),
                Notes = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                OrderStatus = reader.GetString(5)
            };
        }

        // Thêm chi tiết order mới
        public async Task AddOrderDetailAsync(orderDetail orderDetail) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderDetail.OrderId),
                new SqlParameter("@FoodID", orderDetail.FoodId),
                new SqlParameter("@Quantity", orderDetail.Quantity),
                new SqlParameter("@Notes", orderDetail.Notes),
                new SqlParameter("@OrderStatus", orderDetail.OrderStatus)
            };
            await _db.ExecuteNonQueryAsync("AddOrderDetail", parameters);
        }

        // Lấy chi tiết order theo OrderID (kèm thông tin món ăn)
        public async Task<List<OrderDetailDisplay>> GetDetailsByOrderIdAsync(int orderId) {
            var orderDetails = new List<OrderDetailDisplay>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetOrderDetailsWithFoodInfoByOrderID", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@OrderID", orderId);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                orderDetails.Add(new OrderDetailDisplay {
                    TenMon = reader.GetString(0),
                    SoLuong = reader.GetInt32(1),
                    DonGia = reader.GetDecimal(2)
                });
            }
            
            return orderDetails;
        }

        // Thêm nhiều chi tiết order cùng lúc
        public async Task AddListOrderDetailAsync(List<orderDetail> orderDetails) {
            foreach (var orderDetail in orderDetails) {
                await AddOrderDetailAsync(orderDetail);
            }
        }

        // Lấy các chi tiết order đã hoàn thành để tính tổng hóa đơn
        public async Task<List<orderDetail>> GetCompletedOrderDetailsByOrderIdAsync(int orderId) {
            var param = new SqlParameter("@OrderID", orderId);
            return await _db.QueryAsync("GetCompletedOrderDetailsByOrderID", Mapper, param);
        }

        // Cập nhật trạng thái của order
        public async Task UpdateOrderStatusAsync(int orderDetailId, string newStatus) {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrderDetailID", orderDetailId),
                new SqlParameter("@Status", newStatus)
            };
            await _db.ExecuteNonQueryAsync("UpdateOrderStatus", parameters);
        }
    }
}