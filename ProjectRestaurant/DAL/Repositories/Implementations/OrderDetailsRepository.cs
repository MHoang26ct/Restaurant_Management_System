using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRestaurant.DAL.Repositories.Interfaces;
using ProjectRestaurant.DAL.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace ProjectRestaurant.DAL.Repositories.Implementations {
    public class OrderDetailsRepository : IOrderDetailsRepository {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Thêm chi tiết order mới
        public async Task AddOrderDetailAsync(orderDetail orderDetail) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddOrderDetail", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderID", orderDetail.OrderId);
                    command.Parameters.AddWithValue("@FoodID", orderDetail.FoodId);
                    command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                    command.Parameters.AddWithValue("@Notes", orderDetail.Notes);
                    command.Parameters.AddWithValue("@OrderStatus", orderDetail.OrderStatus);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Thêm nhiều chi tiết order cùng lúc
        public async Task AddListOrderDetailAsync(List<orderDetail> orderDetails) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                foreach (var orderDetail in orderDetails) {
                    using (var command = new SqlCommand("AddOrderDetail", connection)) {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrderID", orderDetail.OrderId);
                        command.Parameters.AddWithValue("@FoodID", orderDetail.FoodId);
                        command.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                        command.Parameters.AddWithValue("@Notes", orderDetail.Notes);
                        command.Parameters.AddWithValue("@OrderStatus", orderDetail.OrderStatus);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        // Lấy chi tiết order theo OrderID
        public async Task<List<orderDetail>> GetorderDetailByOrderIdAsync(int orderId) {
            var orderDetailList = new List<orderDetail>();
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM orderDetail WHERE OrderId = @OrderId", connection)) {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    using (var reader = await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            orderDetailList.Add(new orderDetail {
                                Id = reader.GetInt32(0),
                                OrderId = reader.GetInt32(1),
                                FoodId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                Notes = reader.GetString(4),
                                OrderStatus = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return orderDetailList;
        }

        // Cập nhật trạng thái của order
        public async Task UpdateOrderStatusAsync(int orderDetailId, string newStatus) {
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateOrderStatus", connection)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters[0].Value = orderDetailId;
                    command.Parameters[1].Value = newStatus;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
