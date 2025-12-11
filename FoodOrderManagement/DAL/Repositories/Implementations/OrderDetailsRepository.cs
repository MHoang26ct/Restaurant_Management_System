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
        // Nhớ thêm hàm này vào Interface IOrderDetailsRepository trước nhé!
        public async Task<List<OrderDetailDisplay>> GetDetailsByOrderIdAsync(int orderId)
        {
            List<OrderDetailDisplay> list = new List<OrderDetailDisplay>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Câu lệnh SQL quan trọng: JOIN bảng Foods để lấy Tên và Giá
                string query = @"
            SELECT f.FoodName, od.Quantity, f.Price
            FROM OrderDetails od
            JOIN Foods f ON od.FoodId = f.FoodId
            WHERE od.OrderId = @OrderId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new OrderDetailDisplay
                            {
                                TenMon = reader["FoodName"].ToString(),
                                SoLuong = Convert.ToInt32(reader["Quantity"]),
                                DonGia = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }
            return list;
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

        // Lấy các chi tiết order đã hoàn thành để tính tổng hóa đơn
        public async Task<List<orderDetail>> GetCompletedOrderDetailsByOrderIdAsync(int orderId) {
            var completedOrderDetails = new List<orderDetail>();
            using (var connection = new SqlConnection(_connectionString)) {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM orderDetail WHERE OrderId = @OrderId AND OrderStatus = 'Completed'", connection)) {
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    using (var reader = await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            completedOrderDetails.Add(new orderDetail {
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
            return completedOrderDetails;
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
