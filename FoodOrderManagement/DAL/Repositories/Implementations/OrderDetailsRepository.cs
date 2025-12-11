using FoodOrderManagement.DAL.Models.Entities;
using FoodOrderManagement.DAL.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodOrderManagement.UI.Forms.OrderManagement.UserControlOfOrder.UC_ViewDetails;

namespace FoodOrderManagement.DAL.Repositories.Implementations {
    public class OrderDetailsRepository : IOrderDetailsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thêm chi tiết order mới
        public async Task AddOrderDetailAsync(orderDetail orderDetail) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("AddOrderDetail", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@OrderID", orderDetail.OrderId);
            command.Parameters.AddWithValue("@FoodID", orderDetail.FoodId);
            command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
            command.Parameters.AddWithValue("@Notes", orderDetail.Notes);
            command.Parameters.AddWithValue("@OrderStatus", orderDetail.OrderStatus);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
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
                    FoodName = reader.GetString(0),
                    Quantity = reader.GetInt32(1),
                    Price = reader.GetDecimal(2)
                });
            }
            
            return orderDetails;
        }

        // Thêm nhiều chi tiết order cùng lúc
        public async Task AddListOrderDetailAsync(List<orderDetail> orderDetails) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Lưu ý: Với danh sách lớn, nên cân nhắc dùng Transaction để đảm bảo toàn vẹn dữ liệu
            foreach (var orderDetail in orderDetails) {
                using SqlCommand command = new SqlCommand("AddOrderDetail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@OrderID", orderDetail.OrderId);
                command.Parameters.AddWithValue("@FoodID", orderDetail.FoodId);
                command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                command.Parameters.AddWithValue("@Notes", orderDetail.Notes);
                command.Parameters.AddWithValue("@OrderStatus", orderDetail.OrderStatus);

                await command.ExecuteNonQueryAsync();
            }
        }

        // Lấy các chi tiết order đã hoàn thành để tính tổng hóa đơn
        public async Task<List<orderDetail>> GetCompletedOrderDetailsByOrderIdAsync(int orderId) {
            var completedOrderDetails = new List<orderDetail>();
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("GetCompletedOrderDetailsByOrderID", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@OrderID", orderId);

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync()) {
                completedOrderDetails.Add(new orderDetail {
                    OrderDetailId = reader.GetInt32(0),
                    OrderId = reader.GetInt32(1),
                    FoodId = reader.GetInt32(2),
                    Quantity = reader.GetInt32(3),
                    Notes = reader.IsDBNull(4) ? null : reader.GetString(4),
                    OrderStatus = reader.GetString(5)
                });
            }
            
            return completedOrderDetails;
        }

        // Cập nhật trạng thái của order
        public async Task UpdateOrderStatusAsync(int orderDetailId, string newStatus) {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand("UpdateOrderStatus", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            // Đã sửa lại cách add tham số cho rõ ràng hơn so với code cũ dùng index
            command.Parameters.AddWithValue("@OrderDetailID", orderDetailId);
            command.Parameters.AddWithValue("@Status", newStatus);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}